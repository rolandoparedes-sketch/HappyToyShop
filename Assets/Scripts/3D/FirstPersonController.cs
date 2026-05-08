using Sirenix.OdinInspector;
using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonController : MonoBehaviour
{
    #region Properties
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;
    [FoldoutGroup("References")]
    private CharacterController controller;
    [FoldoutGroup("References")]
    public CinemachineCamera characterCamera;
   

    [FoldoutGroup("ControllerSettings")]
    public float moveSpeed = 5f;

    [FoldoutGroup("ControllerSettings")]

    public GameObject flashlight;

    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private float timeDontMove = 2.5f;

    private bool flashlightOn = true;
    [SerializeField] private Vector2 moveInput;
  
    private bool CanMove = false; 
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

        inputs.Player.FlashLight.performed += ctx =>
        {
            if (flashlightOn)
            {
                flashlight.SetActive(false);
                flashlightOn = false;
            }
            else
            {
                flashlight.SetActive(true);
                flashlightOn = true;
            }
        };


        inputs.Player.Jump.performed += ctx => ScenesManager.instance.ChangeMode2D();





    }
    private void OnDisable()
    {
        inputs.Disable();

        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;


        inputs.Player.FlashLight.performed -= ctx =>
        {
            if (flashlightOn)
            {
                flashlight.SetActive(false);
                flashlightOn = false;
            }
            else
            {
                flashlight.SetActive(true);
                flashlightOn = true;
            }
        };



        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode2D();


    }
    #endregion
    void Start()
    {
        StartCoroutine(WaitForPlay());

    }
    void Update()
    {

        //OnMove();
   
        OnSimpleMove();
       
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

    #endregion
    public IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(timeDontMove);
        CanMove = true;
    }
}