using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class FirstPersonController : MonoBehaviour
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

    [FoldoutGroup("ControllerSettings/Monitor")]
    public Camera[] securityCameras;

  

    [FoldoutGroup("ControllerSettings/Monitor")]
    public GameObject monitorUI;

    private int currentCameraIndex;

    private bool usingMonitor;

    [FoldoutGroup("ControllerSettings/Monitor")]
    public Camera securityCamera1;

    [FoldoutGroup("ControllerSettings/Windows")]
    public GameObject woodText;
    public float interactDistance = 3f;
    public GameObject woodPlanks;

    [FoldoutGroup("ControllerSettings/Hold")]
    public Transform holdPoint;
    private Rigidbody grabbedObject;

    [FoldoutGroup("ControllerSettings/Inventory")]
    private bool inventoryOpen;
    public GameObject inventoryUI;

    [FoldoutGroup("ControllerSettings")]
    public float moveSpeed = 5f;

    [FoldoutGroup("ControllerSettings/Flashlight")]

    public GameObject flashlight;

    [FoldoutGroup("ControllerSettings/Flashlight")]

    public LayerMask Shadows;
    [FoldoutGroup("ControllerSettings/Flashlight")]
    public float DistanceRay = 10f;
    [FoldoutGroup("ControllerSettings/Flashlight")]
    [SerializeField] private float inclinacionVertical = 10f;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    [SerializeField] private float inclinacionHorizontal = 10f;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    public float batteryDrainRate = 0.1f;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    //public float batteryRechargeRate = 0.05f;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    public float maxBattery = 100f;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    public float currentBattery;

    [FoldoutGroup("ControllerSettings/Flashlight")]
    public bool flashlightOn = true;
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

        inputs.Player.Interact.started += Interact;

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

        inputs.Player.Interact.started -= Interact;

        inputs.Player.Repair.performed -= RepairWindow;

        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange -= ChangefearEffect;

        inputs.Player.Grab.performed -= GrabObject;
        inputs.Player.Grab.canceled -= ReleaseObject;
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (usingMonitor)
        {
            ExitMonitor();
            return;
        }

        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 10f))
        {
            if (hit.collider.CompareTag("Monitor"))
            {
                StartCoroutine(TimeToActiveMonitor());
            }
        }
    }
    IEnumerator TimeToActiveMonitor()
    {
        StartCoroutine(AcercarCamera());
        yield return new WaitForSeconds(timeToActiveMonitor);
        characterCamera.Lens.FieldOfView = 60;
        EnterMonitor();
    }
    
    public IEnumerator AcercarCamera()
    {

        float timer = 0;
        while (timer < timeToActiveMonitor)
        {
            timer += Time.deltaTime;

            characterCamera.Lens.FieldOfView -= velocityToAccessCamera/timer * Time.deltaTime;

            yield return null;
        }
    }
    

    #endregion
    void Start()
    {
        StartCoroutine(WaitForPlay());
        if (GameManager.instance.Day.Peek() == 7)
        {
            flashlight.GetComponent<Light>().color = Color.red;
        }
        currentBattery = maxBattery;
        currentCordure = maxCordure;
        if(flashlightOn)
        {
            flashlight.SetActive(flashlightOn);
            currentCoroutine = StartCoroutine(BatteryCoroutine());

        }
        else
        {
            flashlight.SetActive(flashlightOn);
            currentCoroutine = StartCoroutine(CordureCoroutine());
        }
   
        ChangefearEffect();

    }
    void Update()
    {

        //OnMove();
        CheckWindow();
        OnSimpleMove();
        Rays();

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = holdPoint.position;
        }
    
    }
    public void Rays()
    {
        Vector3 origin = characterCamera.transform.position;

        Vector3 forward = characterCamera.transform.forward;

        Vector3 upRay = Quaternion.AngleAxis(-inclinacionVertical, characterCamera.transform.right) * forward;

        Vector3 downRay = Quaternion.AngleAxis(inclinacionVertical, characterCamera.transform.right) * forward;

        Vector3 leftRay = Quaternion.AngleAxis(-inclinacionHorizontal, characterCamera.transform.up) * forward;

        Vector3 rightRay = Quaternion.AngleAxis(inclinacionHorizontal, characterCamera.transform.up) * forward;

        DetectShadow(origin, forward);

        DetectShadow(origin, upRay);

        DetectShadow(origin, downRay);

        DetectShadow(origin, leftRay);

        DetectShadow(origin, rightRay);
    }

    private void DetectShadow(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, DistanceRay, Shadows))
        {
            if (hit.collider != null)
            {
                ShadowFollower shadowFollower = hit.collider.GetComponent<ShadowFollower>();

                if (shadowFollower != null)
                {
                    shadowFollower.ShadowDetected();
                }

                ShadowPassageway shadowPassageway = hit.collider.GetComponent<ShadowPassageway>();
                if (shadowPassageway != null)
                {
                    shadowPassageway.ShadowDetected();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (characterCamera == null)
            return;

        Vector3 origin = characterCamera.transform.position;

        Vector3 forward = characterCamera.transform.forward;

        Vector3 upRay =
            Quaternion.AngleAxis(-inclinacionVertical, characterCamera.transform.right)
            * forward;

        Vector3 downRay =
            Quaternion.AngleAxis(inclinacionVertical, characterCamera.transform.right)
            * forward;

        Vector3 leftRay =
            Quaternion.AngleAxis(-inclinacionHorizontal, characterCamera.transform.up)
            * forward;

        Vector3 rightRay =
            Quaternion.AngleAxis(inclinacionHorizontal, characterCamera.transform.up)
            * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, forward * DistanceRay);

        Gizmos.DrawRay(origin, upRay * DistanceRay);

        Gizmos.DrawRay(origin, downRay * DistanceRay);

        Gizmos.DrawRay(origin, leftRay * DistanceRay);

        Gizmos.DrawRay(origin, rightRay * DistanceRay);
    }
    private void EnterMonitor()
    {
        usingMonitor = true;

        characterCamera.gameObject.SetActive(false);

        securityCameras[currentCameraIndex].gameObject.SetActive(true);

        monitorUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    private void ExitMonitor()
    {
        usingMonitor = false;

        securityCameras[currentCameraIndex].gameObject.SetActive(false);

        characterCamera.gameObject.SetActive(true);

        monitorUI.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void NextCamera()
    {
        securityCameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex++;

        if (currentCameraIndex >= securityCameras.Length)
        {
            currentCameraIndex = 0;
        }

        securityCameras[currentCameraIndex].gameObject.SetActive(true);
    }
    #region Methods
    public void OnSimpleMove()
    {
        if (!CanMove)
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
        if(flashlightOn)
        {

            flashlightOn = false;
            flashlight.SetActive(flashlightOn);


            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(
                flashlightOn ? BatteryCoroutine() : CordureCoroutine()
            );
        }
        else
        {
            flashlightOn = true;
            flashlight.SetActive(flashlightOn);

            if(currentCoroutine != null)
                StopCoroutine (currentCoroutine);
            
            currentCoroutine = StartCoroutine(
                flashlightOn ? BatteryCoroutine() : CordureCoroutine()
            );
        }

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
   

    #endregion
    #region Coroutines
    private IEnumerator BatteryCoroutine()
    {
        while (flashlightOn)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime;

            if (currentBattery <= 0)
            {
                currentBattery = 0;

                flashlightOn = false;
                flashlight.SetActive(false);

                currentCoroutine = StartCoroutine(CordureCoroutine());

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator CordureCoroutine()
    {
        while (!flashlightOn && currentCordure > 0)
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
                GameObject wood = hit.collider.transform.Find("WoodPlanks").gameObject;

                wood.SetActive(true);
            }
        }
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
    #endregion
}