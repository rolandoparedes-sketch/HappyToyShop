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

                OnDeadBattery?.Invoke();
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
        
    }

    public void ApplyBaseBattery()
    {
        currentBattery = maxBattery;
        enoughBattery = true;

        if (GameManager.instance.Day.Peek() == 7)
        {
            pointlight.GetComponent<Light>().color = Color.red;
        }
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
