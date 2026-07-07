using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParanormalSuccess2D : MonoBehaviour
{
    public static bool paranormalSuccessActive;

    public float TimeDelaySounDoors = 4;
    public float durationSoundDoors = 6;
    public float TimeToActiveSound;
   
    public float timer;

    public bool DayEnd;




    public Shadow shadow;
    public float timerShadow;
    public int probability;
    public float TimeToActiveShadow;
    public bool CanSpawnShadows;

    public List<Transform> initialPos;



    public Light globalLight;

    public float powerDownDuration = 21;

    void Start()
    {

    }
    public void OnEnable()
    {
        GameManager2D.instance.DayManager.OnDayComplete += () => DayEnd = true;
    }
    public void OnDisable()
    {

        GameManager2D.instance.DayManager.OnDayComplete -= () => DayEnd = true;
    }
    private void Update()
    {

        if(DayEnd)
        {
            return;
        }
        timer += Time.deltaTime;

        if (timer > TimeToActiveSound)
        {
            timer = 0;

            int n = Random.Range(0, probability);


            int n1 = Random.Range(0, 2);


            switch (n1)
            { 
                case 1:
                    KnockKnock();

                    break;
                case 2:

                    PowerDown();
                    break;
            }


                /*DayEvents dayEvents = GameManager2D.instance.DayManager.SpecialDays.Peek().DayEvents;


                switch (dayEvents)
                {
                    case DayEvents.None:
                        break;

                    case DayEvents.PayDay:

                        break;
                    case DayEvents.HorrorDay:

                        PowerDown();

                        break;
                    case DayEvents.Shadows:
                        CanSpawnShadows = true;

                        break;
                    case DayEvents.MysteryVisitor:
                        break;

                    case DayEvents.MysterySounds:

                        KnockKnock();
                        break;
                }*/



            
        } 
    }
    [Button]
    public void StartPowerDown()
    {
        StartCoroutine(PowerDown());    
    }
    public IEnumerator PowerDown()
    {
        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 7);

        float originalIntensity = globalLight.intensity;
        globalLight.intensity = 0.1f;

        yield return new WaitForSeconds(powerDownDuration);

            globalLight.intensity = originalIntensity;
    }

    [Button]
    public void KnockKnock()
    {
        GameManager2D.instance.SoundManager.StopMusicBackground(TimeDelaySounDoors);


        StartCoroutine(PlayStrangeSound());
    }
    public IEnumerator PlayStrangeSound()
    {
        StartCoroutine(TimeToFreeze());
        yield return new WaitForSeconds(TimeDelaySounDoors);

        int n = Random.Range(0, 2);

        switch (n)
        {

                case 0:
                GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Ambient, 0);
                break;
            case 1:
                GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Ambient, 1);
                break;
        }


        yield return new WaitForSeconds(durationSoundDoors);


        GameManager2D.instance.SoundManager.PlayMusicBackground();
    }

    public void FreezeALL()
    {
       // StartCoroutine(TimeToFreeze());
    }
    public IEnumerator TimeToFreeze()
    {

        paranormalSuccessActive = true;
        GameManager2D.instance.UIManager.ChangeDialoguePlayer(".......");

        yield return new WaitForSeconds(durationSoundDoors);


        GameManager2D.instance.UIManager.ChangeDialoguePlayer("New days, new smiles");

        paranormalSuccessActive = false;
    }
    [Button]
    public void TryToSpawnShadows()
    {
        if (!CanSpawnShadows)
            return;


        timerShadow += Time.deltaTime;

        if (timer > TimeToActiveShadow)
        {
            timer = 0;
            int n = Random.Range(0, 101);
            if(n> probability)
            {

            }
            


        }
    }
}

