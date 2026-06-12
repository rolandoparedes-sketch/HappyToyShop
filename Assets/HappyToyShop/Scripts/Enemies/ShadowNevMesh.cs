using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.XR;
using UnityEditor.PackageManager.UI;
public class EnemyShadow : MonoBehaviour
{
    [Header("BreakWindow")]
    public Transform[] windows;
    public stateMachine stateMachine;
    public NavMeshAgent agent;
    public Transform player;

    [HideInInspector]
    public Transform currentWindow;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 7.5f;
        stateMachine = new stateMachine();
        currentWindow = windows[Random.Range(0, windows.Length)];
      
        stateMachine.Initialize(new GoToWindowState(this));

    }
    void Update()
    {
        stateMachine.Update();
    }
    
    
}
