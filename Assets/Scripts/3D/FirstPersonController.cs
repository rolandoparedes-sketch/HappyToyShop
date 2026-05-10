using Sirenix.OdinInspector;
using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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
      

        inputs.Player.Sprint.performed += ctx => moveSpeed *= 2;

        inputs.Player.Sprint.canceled += ctx => moveSpeed /= 2;


        inputs.Player.FlashLight.performed += LightOn;
      

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
      

        inputs.Player.FlashLight.performed -= LightOn;


        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode2D();

        OnStateFearChange -= ChangefearEffect;

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
   
        OnSimpleMove();
        Rays();
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
    #endregion
}