
using Sirenix.OdinInspector;
using UnityEngine;

public class ParanormalSuccess3D : MonoBehaviour
{
   

    [FoldoutGroup("References")]
    public Transform Player;

    [FoldoutGroup("References")]
    public Collider ColliderDetectedA;
    [FoldoutGroup("References")]
    public Collider ColliderDetectedB;

    [FoldoutGroup("References/Shadows")]
    public GameObject ShadowFollower;

    [FoldoutGroup("References/Shadows")]
    public GameObject ShadowsPassageway;

    [FoldoutGroup("References/Shadows")]
    public GameObject ShadowsPassageway2;

    [FoldoutGroup("References/Shadows")]
    public GameObject ShadowsScream;


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
        PercentageProbablityToApper = Mathf.Min (PercentageProbablityToApper * (GameManager.instance.Day.Peek()), 100f);

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
                Vector3 SpawnShadowFollower =  Player.position - Player.forward * 5f;

                GameObject shadowFollowerInstance = Instantiate(ShadowFollower, SpawnShadowFollower, Quaternion.identity);

                ShadowFollower enemy = shadowFollowerInstance.GetComponent<ShadowFollower>();

                enemy.target = Player;


                CanSpawnFollower = false;
            }
        }
    }


    private void TryActivateShadowPassageway()
    {
        float n = GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>().currentCordure;


        int random = Random.Range(0, 100);
        Debug.Log("Random number for activating shadow passageway: " + random);
        if(random >= n)
        {
            ShadowsPassageway.SetActive(true);
            ShadowsPassageway2.SetActive(true);
            CanSpawnPassageway = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == Player)
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
