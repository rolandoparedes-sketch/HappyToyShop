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

    private bool usingMonitor;
    public float timeToActiveMonitor = 1.5f;
    public float velocityToAccessCamera = 4;
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;
    [FoldoutGroup("References")]
    private CharacterController controller;
    [FoldoutGroup("References")]
    public CinemachineCamera characterCamera;


    [FoldoutGroup("ControllerSettings/Windows")]
    //public GameObject woodText;
    public float interactDistance = 3f;

    [FoldoutGroup("ControllerSettings/Monitor")]
    public Transform monitorViewPoint;
    private Vector3 originalTargetPosition;
    public Transform cameraTarget;
    public Camera[] securityCameras;
    public GameObject monitorUI;
    private CameraNode currentCamera;


   
    [FoldoutGroup("ControllerSettings/Monitor")]
    public GameObject woodText;
    public float interactDistanceMonitor = 3f;
    public GameObject woodPlanks;
    private float repairCounter;
    public Slider repairBar;
    public TMP_Text repairText;
    

    [FoldoutGroup("ControllerSettings/Hold")]
    public Transform holdPoint;
    private Rigidbody grabbedObject;

    [FoldoutGroup("ControllerSettings/Inventory")]
    private bool inventoryOpen;
    public GameObject inventoryUI;

    [FoldoutGroup("ControllerSettings")]
    public float moveSpeed = 5f;

    

    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private float timeDontMove = 2.5f;

    public Vector2 moveInput;

  
    private bool CanMove = false;

    private Coroutine currentCoroutine;

    [SerializeField] private LayerMask interactuableObjects;
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

        inputs.Player.Interact.performed += ctx => Interact();

        inputs.Player.Sprint.performed += ctx => moveSpeed *= 2;

        inputs.Player.Sprint.canceled += ctx => moveSpeed /= 2;

        inputs.Player.Repair.performed += RepairWindow;

        inputs.Player.FlashLight.performed += LightOn;
        inputs.Player.Inventory.performed += OpenInventory;

       
        Monitor.OnWatchingCameras += EnterMonitor;
        Monitor.OnExitCameras += ExitMonitor;


    }

    private void Interact()
    {
        Debug.Log("Interactuar");

        Ray ray = new Ray(characterCamera.transform.position, characterCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactuableObjects))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.TryGetComponent<IInteractuable>(out var interactuable))
            {
                Debug.Log(interactuable);

                interactuable.Interact();
            }
        }
    }

    private void EnterMonitor()
    {
        currentCoroutine = StartCoroutine(WaitForPlay(9999));
    }

    private void ExitMonitor()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
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



        inputs.Player.Grab.performed -= GrabObject;
        inputs.Player.Grab.canceled -= ReleaseObject;



        Monitor.OnWatchingCameras -= EnterMonitor;
        Monitor.OnExitCameras -= ExitMonitor;
    }
    #endregion
    void Start()
    {

        StartCoroutine(WaitForPlay(timeDontMove));
   

    }
    void Update()
    {

        //OnMove();
        //CheckWindow();
        OnSimpleMove();
       

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = holdPoint.position;
        }
    
    }
    #region Methods
    public void OnSimpleMove()
    {
        if(!CanMove || usingMonitor)
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
        var inventory = Player3DController.instance.inventory3D;
        var state3D = Player3DController.instance.state3D;

        bool flashlightOn = inventory.itemInHandRight.gameObject.activeSelf;

        if (flashlightOn)
        {
            inventory.itemInHandRight.gameObject.SetActive(false);

            if (state3D.currentCoroutine == null)
            {
                state3D.currentCoroutine = state3D.StartCoroutine(state3D.CordureCoroutine());
            }
        }
        else
        {
            inventory.itemInHandRight.gameObject.SetActive(true);

            if (state3D.currentCoroutine != null)
            {
                state3D.StopCoroutine(state3D.currentCoroutine);
                state3D.currentCoroutine = null;
            }
        }

    }
   /* private void CheckWindow()
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
    }*/
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

    public IEnumerator WaitForPlay(float time)
    {
        yield return new WaitForSeconds(time);
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
                WoodPlanks planks = wood.GetComponent<WoodPlanks>();
                planks.health = 5;
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