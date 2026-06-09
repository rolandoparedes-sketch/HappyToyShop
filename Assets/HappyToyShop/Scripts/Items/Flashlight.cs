using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : ItemBase
{
    [SerializeField]private Light pointlight;
    [SerializeField]private bool enoughBattery = true;
    [SerializeField]private float currentBattery = 0;
    [SerializeField]private float maxBattery = 100;
    [SerializeField]private float batteryDrainRate = 0.25f;
    private Coroutine currentCoroutine;

    public static event Action OnDeadBattery;

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
    //public float batteryRechargeRate = 0.05f;

    


    [FoldoutGroup("ControllerSettings/Flashlight")]
    public bool flashlightOn = true;

    private void Awake()
    {
        pointlight = GetComponent<Light>();
    }
    private void OnEnable()
    {
        currentCoroutine = StartCoroutine(BatteryCoroutine());

     
    }
    private void OnDisable()
    {
       StopCoroutine(currentCoroutine);
    }

    private IEnumerator BatteryCoroutine()
    {
        while (enoughBattery) 
        {
            currentBattery -= batteryDrainRate * Time.deltaTime;
            if(currentBattery <= 0)
            {
                currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
                enoughBattery = false;
                yield break;

            }

            yield return null;
        }
    }

    void Start()
    {
        ApplyBaseBattery();
    }

    void Update()
    {
        Rays();
    }

    public void ApplyBaseBattery()
    {
        currentBattery = maxBattery;
        enoughBattery = true;
    }

    public void Rays()
    {
        Vector3 origin = transform.position;

        Vector3 forward = transform.forward;

        Vector3 upRay = Quaternion.AngleAxis(-inclinacionVertical, transform.right) * forward;

        Vector3 downRay = Quaternion.AngleAxis(inclinacionVertical, transform.right) * forward;

        Vector3 leftRay = Quaternion.AngleAxis(-inclinacionHorizontal, transform.up) * forward;

        Vector3 rightRay = Quaternion.AngleAxis(inclinacionHorizontal, transform.up) * forward;

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

        Vector3 origin = transform.position;

        Vector3 forward = transform.forward;

        Vector3 upRay =
            Quaternion.AngleAxis(-inclinacionVertical, transform.right)
            * forward;

        Vector3 downRay =
            Quaternion.AngleAxis(inclinacionVertical, transform.right)
            * forward;

        Vector3 leftRay =
            Quaternion.AngleAxis(-inclinacionHorizontal, transform.up)
            * forward;

        Vector3 rightRay =
            Quaternion.AngleAxis(inclinacionHorizontal, transform.up)
            * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, forward * DistanceRay);

        Gizmos.DrawRay(origin, upRay * DistanceRay);

        Gizmos.DrawRay(origin, downRay * DistanceRay);

        Gizmos.DrawRay(origin, leftRay * DistanceRay);

        Gizmos.DrawRay(origin, rightRay * DistanceRay);
    }
    #region Getters
    public Light Light => pointlight;
    public bool EnoughBattery => enoughBattery;
    public float CurrentBattery => currentBattery;
    public float MaxBattery => maxBattery;
    public float BatteryDrainRate => batteryDrainRate;


    //proyecto => assets=> nombre del proyect => scripts, scriptableObjects, Resources

   // scripts =>
   // interface
   // enums

    #endregion
}
