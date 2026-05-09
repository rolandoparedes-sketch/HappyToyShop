
using Sirenix.OdinInspector;
using UnityEngine;

public class ParanormalSuccess : MonoBehaviour
{
    [FoldoutGroup("References")]
    public GameObject Shadow;

    [FoldoutGroup("References")]
    public Transform Player;

    [FoldoutGroup("Settings"), Range(0,100)]
    public float PercentageProbablityToApper = 15;


    [FoldoutGroup("Settings")]
    public float TimeToTrySpawn = 10f;
    private float timer;
    [FoldoutGroup("Settings")]
    public bool CanSpawn = true;

   
    void Start()
    {
        PercentageProbablityToApper = Mathf.Min (PercentageProbablityToApper * (GameManager.instance.Day.Peek()), 100f);

        Debug.Log("Initial probability to spawn shadow: " + PercentageProbablityToApper);

    }

    void Update()
    {
        if(!GameManager.instance.TurnDay)
        {
            if (CanSpawn)
            {
                timer += Time.deltaTime;
                if (timer >= TimeToTrySpawn)
                {
                    timer = 0f;
                    TrySpawnShadow();
                }
            }
        }
    }

    private void TrySpawnShadow()
    {
        int random = Random.Range(0, 100);
        Debug.Log("Random number for spawning shadow: " + random);
        if (random <= PercentageProbablityToApper)
        {
            {
                Vector3 SpawnShadow =  Player.position - Player.forward * 5f;

                GameObject shadowInstance = Instantiate(Shadow, SpawnShadow, Quaternion.identity);

                Shadow enemy = shadowInstance.GetComponent<Shadow>();

                enemy.target = Player;


                CanSpawn = false;
            }
        }
    }
}
