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

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        if (windows != null)
        {
            foreach (Transform window in windows)
            {
                if (window != null)
                    Gizmos.DrawWireSphere(window.position, 0.4f);
            }
        }

        
        Gizmos.color = Color.blue;
        if (VentPoints != null)
        {
            foreach (Transform vent in VentPoints)
            {
                if (vent != null)
                    Gizmos.DrawWireSphere(vent.position, 0.4f);
            }
        }

      
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, 0.5f);
        }

       
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

       
        if (currentWindow != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, currentWindow.position);
        }
    }
}