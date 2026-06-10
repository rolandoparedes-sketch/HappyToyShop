using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Player3DMovement : MonoBehaviour
{
    #region Properties
    public float timeToActiveMonitor = 1.5f;
    public float velocityToAccessCamera = 4;
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;
    [FoldoutGroup("References")]
    private CharacterController controller;
    [FoldoutGroup("References")]
    public CinemachineCamera characterCamera;

    [FoldoutGroup("ControllerSettings")]
    public FearState currentFearState;

    [FoldoutGroup("ControllerSettings/Windows")]
    //public GameObject woodText;
    public float interactDistance = 3f;

    [FoldoutGroup("ControllerSettings/Monitor")]
    public Transform monitorViewPoint;
    private Vector3 originalTargetPosition;

    [FoldoutGroup("ControllerSettings/Monitor")]
    public GameObject woodText;
    public float interactDistanceMonitor = 3f;
    public GameObject woodPlanks;
    private float repairCounter;
    public Slider repairBar;
    public TextMeshPro repairText;

    [FoldoutGroup("ControllerSettings/Hold")]
    public Transform holdPoint;
    private Rigidbody grabbedObject;

    [FoldoutGroup("ControllerSettings/Inventory")]
    private bool inventoryOpen;
    public GameObject inventoryUI;

    [FoldoutGroup("ControllerSettings")]
    public float moveSpeed = 5f;

    [FoldoutGroup("ControllerSettings/Cordure")]
    public float cordureDrainRate = 0.25f;

    [FoldoutGroup("ControllerSettings/Cordure")]
    public float maxCordure = 100f;

    [FoldoutGroup("ControllerSettings/Cordure")]
    public float currentCordure;

    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private float timeDontMove = 2.5f;

    public Vector2 moveInput;

  
    private bool CanMove = false;

    private Coroutine currentCoroutine;
    public Action OnStateFearChange;

    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 1f)]
    [SerializeField]private float amplitudeGainCalm = 0.5f;
    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 5f)]
    [SerializeField] private float frequencyGainCalm = 0.5f;

    [Space(10)]

    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 1f)]
    [SerializeField] private float amplitudeGainNervous = 0.5f;
    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 10f)]
    [SerializeField] private float frequencyGainNervous= 0.5f;

    [Space(10)]

    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 1f)]
    [SerializeField] private float amplitudeGainScared = 0.5f;
    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 100f)]
    [SerializeField] private float frequencyGainScared = 0.5f;

    [Space(10)]

    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 1f)]
    [SerializeField] private float amplitudeGainTerrified = 0.5f;
    [FoldoutGroup("ControllerSettings/FearIntensityLeveles"), Range(0f, 300f)]
    [SerializeField] private float frequencyGainTerrified = 0.5f;

    #endregion
    #region Inicialization
    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region InputSystem
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
      
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputs.Player.Grab.performed += GrabObject;
        inputs.Player.Grab.canceled += ReleaseObject;

        inputs.Player.Sprint.performed += ctx => moveSpeed *= 2;

        inputs.Player.Sprint.canceled += ctx => moveSpeed /= 2;

        inputs.Player.Repair.performed += RepairWindow;

        inputs.Player.FlashLight.performed += LightOn;
        inputs.Player.Inventory.performed += OpenInventory;

        inputs.Player.Jump.performed += ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange += ChangefearEffect;

        
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();

        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        inputs.Player.Sprint.performed -= ctx => moveSpeed *= 2;
      
        inputs.Player.Sprint.canceled -= ctx => moveSpeed /= 2;

        inputs.Player.Inventory.performed -= OpenInventory;

        inputs.Player.FlashLight.performed -= LightOn;

        inputs.Player.Repair.performed -= RepairWindow;

        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange -= ChangefearEffect;

        inputs.Player.Grab.performed -= GrabObject;
        inputs.Player.Grab.canceled -= ReleaseObject;
    }
    #endregion
    void Start()
    {
        StartCoroutine(WaitForPlay());
   
        ChangefearEffect();

    }
    void Update()
    {

        //OnMove();
       // CheckWindow();
        OnSimpleMove();
       

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = holdPoint.position;
        }
    
    }
    #region Methods
    public void OnSimpleMove()
    {
        if(!CanMove)
        {  
            return; 
        }
        Vector3 cameraForwardDir = characterCamera.transform.forward;
        cameraForwardDir.y = 0;
        cameraForwardDir.Normalize();


        Quaternion targetQuaternion = Quaternion.LookRotation(cameraForwardDir);
        transform.rotation = targetQuaternion;

        Vector3 moveDir = (cameraForwardDir * moveInput.y + transform.right * moveInput.x) * moveSpeed;
       
        controller.SimpleMove(moveDir);
    }
    private void LightOn(InputAction.CallbackContext context)
    {
        if(Player3DController.instance.inventory3D.itemInHandRight.gameObject.activeSelf)
        {
            Player3DController.instance.inventory3D.itemInHandRight.gameObject.SetActive(false);
        }
        else 
            Player3DController.instance.inventory3D.itemInHandRight.gameObject.SetActive(true);
    }
    public void UpdateFearState()
    {
        if (currentCordure <= 10)
        {
            currentFearState = FearState.Terrified;
            OnStateFearChange?.Invoke();

        }
        else if (currentCordure <= 30)
        {
            currentFearState = FearState.Scared;
            OnStateFearChange?.Invoke();
        }
        else if (currentCordure <= 60)
        {
            currentFearState = FearState.Nervous;
            OnStateFearChange?.Invoke();
        }
        else
        {
            currentFearState = FearState.Calm;
            OnStateFearChange?.Invoke();
        }
    }
    private void ChangefearEffect()
    {
        switch (currentFearState)
        {
            case FearState.Calm:
                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitudeGainCalm;

                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequencyGainCalm;
                break;
            case FearState.Nervous:
                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitudeGainNervous;

                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequencyGainNervous;
                break;
            case FearState.Scared:
                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitudeGainScared;

                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequencyGainScared;
                break;
            case FearState.Terrified:
                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitudeGainTerrified;

                characterCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequencyGainTerrified;
                break;
        }

    }
    private void CheckWindow()
    {
        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.CompareTag("Window"))
            {
                woodText.SetActive(true);
            }
            else
            {
                woodText.SetActive(false);
            }
        }
        else
        {
            woodText.SetActive(false);
        }
    }
    private void RepairWindow(InputAction.CallbackContext ctx)
    {

        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Window"))
            {
                StartCoroutine(RepairCoroutine(hit.collider));
            }
        }
    }



    #endregion
    #region Coroutines

    private IEnumerator CordureCoroutine()
    {
        while (currentCordure > 0)
        {
            currentCordure -= cordureDrainRate * Time.deltaTime;

            if (currentCordure < 0)
                currentCordure = 0;
            UpdateFearState();

            yield return null;
        }
    }
    public IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(timeDontMove);
        CanMove = true;
    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        inventoryOpen = !inventoryOpen;

        inventoryUI.SetActive(inventoryOpen);

       
    }
    
    private void GrabObject(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Box"))
            {
                grabbedObject = hit.collider.GetComponent<Rigidbody>();

                if (grabbedObject != null)
                {
                    grabbedObject.useGravity = false;
                }
            }
        }
    }
    private void ReleaseObject(InputAction.CallbackContext ctx)
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;

            grabbedObject = null;
        }
    }
    private IEnumerator RepairCoroutine(Collider window)
    {
        repairCounter = 0;
        repairBar.gameObject.SetActive(true);
        repairBar.value = 0;

        while (inputs.Player.Repair.IsPressed())
        {
            repairBar.value = repairCounter / 10f;
            repairCounter += Time.deltaTime * 2;

            Debug.Log(repairCounter);

            if (repairCounter >= 10)
            {
                GameObject wood = window.transform.Find("WoodPlanks").gameObject;

                wood.SetActive(true);
                repairBar.gameObject.SetActive(false);
                repairText.text = "Tablas de Maderas Puestas";

                StartCoroutine(HideMessage());

                repairCounter = 0;

                yield break;
            }

            yield return null;
        }

        repairCounter = 0;

        repairBar.value = 0;
        repairBar.gameObject.SetActive(false);
    }
    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);

        repairText.text = "";
    }
    #endregion
}