
using Sirenix.OdinInspector;
using UnityEngine;

public class ParanormalSuccess3D : MonoBehaviour
{
   

    [FoldoutGroup("References")]
    public Collider ColliderDetectedA;
    [FoldoutGroup("References")]
    public Collider ColliderDetectedB;

    [FoldoutGroup("References/Shadows")]
    public ShadowFollower ShadowFollower;

    [FoldoutGroup("References/Shadows")]
    public ShadowPassageway ShadowsPassageway;

    [FoldoutGroup("References/Shadows")]
    public ShadowPassageway ShadowsPassageway2;

    [FoldoutGroup("References/Shadows")]
    public ShadowScream ShadowsScream;


    [FoldoutGroup("Settings"), Range(0,100)]
    public float PercentageProbablityToApper = 15;


    [FoldoutGroup("Settings")]
    public float TimeToTrySpawnFollower = 10f;

    [FoldoutGroup("Settings")]
    public float TimeToActivatePassageway = 20f;


    private float timerFollower;
    private float timerPassageway;  
    [FoldoutGroup("Settings/Bool")]
    public bool CanSpawnFollower = true;
    [FoldoutGroup("Settings/Bool")]
    public bool CanSpawnPassageway = true;
    [FoldoutGroup("Settings/Bool")]
    public bool CanSpawnScream = true;

    void Start()
    {

        Debug.Log("Initial probability to spawn shadow: " + PercentageProbablityToApper);

    }

    void Update()
    {
        if (CanSpawnFollower)
        {
            timerFollower += Time.deltaTime;
            if (timerFollower >= TimeToTrySpawnFollower)
            {
                timerFollower = 0f;
                TrySpawnShadowFollower();
            }
        }
        if (!CanSpawnPassageway)
        {
            timerPassageway += Time.deltaTime;

            if(timerPassageway >= TimeToActivatePassageway)
            {
                timerPassageway = 0f;
                CanSpawnPassageway = true;

            }
        }

    }

    private void TrySpawnShadowFollower()
    {
        int random = Random.Range(0, 100);
        Debug.Log("Random number for spawning shadow follower: " + random);
        if (random <= PercentageProbablityToApper)
        {
            {
                GameManager3D.instance.ShadowSpawner.SpawnFollower();
                int number= Random.Range(0, 2);

                switch (number)
                {   
                    case 0:
                        GameManager3D.instance.SoundManager.CheckTypeAudio(SoundType.Voice, 0);
                        break;
                    case 1:
                        GameManager3D.instance.SoundManager.CheckTypeAudio(SoundType.Voice, 1);
                        break;
                }
                CanSpawnFollower = false;
            }
        }
    }

    
    private void TryActivateShadowPassageway()
    {
        float n = GameManager3D.instance.ShadowSpawner.Target.GetComponent<Player3DState>().currentCordure;


        int random = Random.Range(0, 100);
        Debug.Log("Random number for activating shadow passageway: " + random);
        if(random >= n)
        {
            ShadowsPassageway.gameObject.SetActive(true);
            ShadowsPassageway2.gameObject.SetActive(true);
            CanSpawnPassageway = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == GameManager3D.instance.ShadowSpawner.Target)
        {
            if (CanSpawnPassageway)
            {
                if (gameObject == ColliderDetectedA.gameObject || gameObject == ColliderDetectedB.gameObject)
                {
                    TryActivateShadowPassageway();


                }
            }
        }
    }

}
