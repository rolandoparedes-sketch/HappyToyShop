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
    public FearState currentFearState;


    [FoldoutGroup("ControllerSettings")]
    public float moveSpeed = 5f;

    [FoldoutGroup("ControllerSettings/Flashlight")]

    public GameObject flashlight;

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
    public float cordureDrainRate = 0.1f;

    [FoldoutGroup("ControllerSettings/Cordure")]
    public float maxCordure = 100f;

    [FoldoutGroup("ControllerSettings/Cordure")]
    public float currentCordure;

    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private float timeDontMove = 2.5f;

    [SerializeField] private Vector2 moveInput;
  
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

        inputs.Player.FlashLight.performed += LightOn;
      

        inputs.Player.Jump.performed += ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange += ChangefearEffect;



    }

    private void OnDisable()
    {
        inputs.Disable();

        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;


        inputs.Player.FlashLight.performed -= LightOn;


        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange -= ChangefearEffect;

    }
    #endregion
    void Start()
    {
        StartCoroutine(WaitForPlay());

        currentBattery = maxBattery;
        currentCordure = maxCordure;

        StartCoroutine(BatteryCoroutine());

    }
    void Update()
    {

        //OnMove();
   
        OnSimpleMove();
        ChangefearEffect(); 

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

    private void UpdateFearState()
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
    public void ChangefearEffect()
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
    #endregion
}