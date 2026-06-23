using UnityEngine;
using UnityEngine.AI;

public class EnemyShadow : MonoBehaviour
{
    [Header("BreakWindow")]
    public Transform[] windows;
    public Transform[] VentPoints;

    [Header("Attack Chances")]
    [Range(0, 100)]
    public float windowChance = 50f;

    [Range(0, 100)]
    public float ventChance = 50f;

    public stateMachine stateMachine;
    public NavMeshAgent agent;
    public Transform player;

    [Header("Movement")]
    public float enemySpeed = 6f;

    [Header("UI")]
    public GameObject warningText;
    public GameObject AlertText;

    public bool isVent;

    [HideInInspector]
    public Transform currentWindow;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;

        stateMachine = new stateMachine();
        stateMachine.Initialize(new IdleState(this));
    }

    void Update()
    {
        stateMachine.Update();
    }
    public void GoIdle()
    {
        stateMachine.ChangeState(new IdleState(this));
    }

    
}