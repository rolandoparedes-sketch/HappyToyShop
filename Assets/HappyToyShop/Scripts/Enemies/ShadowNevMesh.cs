using UnityEngine;
using UnityEngine.AI;
public class EnemyShadow : MonoBehaviour
{
    [Header("BreakWindow")]
    public Transform[] windows;
    public stateMachine stateMachine;
    public NavMeshAgent agent;
    public Transform player;
    [Header("Movement")]
    public float enemySpeed = 6f;

    [HideInInspector]
    public Transform currentWindow;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
        stateMachine = new stateMachine();
        currentWindow = windows[Random.Range(0, windows.Length)];
      
        stateMachine.Initialize(new GoToWindowState(this));

    }
    void Update()
    {
        stateMachine.Update();
    }
    
    
}
