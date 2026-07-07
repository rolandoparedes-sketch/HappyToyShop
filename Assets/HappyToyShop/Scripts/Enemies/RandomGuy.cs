using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
public class RandomGuy : MonoBehaviour
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

    [Header("Electroshock")]
    public GameObject electroshockParticles;
    [HideInInspector]
public Transform currentVent;

    [Header("Movement")]
    public float enemySpeed = 6f;
    public float chaseSpeed = 20f;

    public float sphereRadius = 1.5f;
    public float sphereDistance = 8f;

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
        CheckPlayerSphereCast();
    }
    public void GoIdle()
    {
        stateMachine.ChangeState(new IdleState(this));
    }
    private void CheckPlayerSphereCast()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, sphereDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Detectando Spherecast");
            }
        }
    }
    public void ActivateElectroshock()
    {
        if (!isVent)
        {
            Debug.Log("El enemigo no está usando la ventilación.");
            return;
        }

        if (electroshockParticles != null && currentVent != null)
        {
            Instantiate(
                electroshockParticles,
                currentVent.position,
                currentVent.rotation
            );
        }

        Debug.Log("¡Electroshock activado!");

        agent.ResetPath();

        isVent = false;

        GoIdle();
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + transform.forward * sphereDistance, sphereRadius);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * sphereDistance);
    }
}