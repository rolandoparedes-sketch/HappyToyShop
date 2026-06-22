using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.XR;
using UnityEditor.PackageManager.UI;
public class EnemyShadow : MonoBehaviour
{
    [Header("BreakWindow")]
    public Transform[] windows;
    public Transform[] VentPoints;
    public stateMachine stateMachine;
    public NavMeshAgent agent;
    public Transform player;
    [Header("Movement")]
    public float enemySpeed = 6f;

  
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
}
