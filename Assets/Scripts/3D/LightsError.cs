using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class LightsError : MonoBehaviour
{
    [FoldoutGroup("References")]
    public Light lightSource;

    [FoldoutGroup("Settings") ,Header("IdleTime")]
   
    public float minWaitTime = 3f;

    [FoldoutGroup("Settings")]
    public float maxWaitTime = 10f;

    [FoldoutGroup("Settings") ,Header("Flickers")]
    public int minFlickers = 1;
    [FoldoutGroup("Settings")]
    public int maxFlickers = 4;

    [FoldoutGroup("Settings") ,Header("Flicker Speed")]
    public float minFlickerSpeed = 0.03f;
    [FoldoutGroup("Settings")]
    public float maxFlickerSpeed = 0.08f;

    private void Awake()
    {

    }
    private void Start()
    {

        //timer = Random.Range(MinTime, MaxTime);
        StartCoroutine(FlickerRoutine());
        GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>().OnStateFearChange += lightsEffects;
        if(GameManager.instance.Day.Peek() == 7)
        {
            lightSource.color = Color.red;
        }
    }
   
 
    public void lightsEffects()
    {

        var Enum = GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>().currentFearState;

        switch (Enum)
        {
            case FearState.Calm:
                lightSource.range = 7;
                lightSource.intensity = 0.5f;
                break;
            case FearState.Nervous:

                lightSource.range = 6;
                lightSource.intensity = 0.25f;
                break;
            case FearState.Scared:
                lightSource.range = 5;

                lightSource.intensity = 0.05f;
                break;
            case FearState.Terrified:
                lightSource.gameObject.SetActive(false);
                break;
        }
    }
    

    
    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            int flickers = Random.Range(minFlickers, maxFlickers + 1);

            for (int i = 0; i < flickers; i++)
            {
                lightSource.enabled = false;

                yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));

                lightSource.enabled = true;

                yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            }
        }
    }
    void Update()
    {
        /*if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Light.enabled = !Light.enabled;
            timer = Random.Range(MinTime, MaxTime);
        }*/
    }
}