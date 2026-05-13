using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemyShadow : MonoBehaviour
{
    public Transform[] windows;

    public float breakTime = 5f;

    private NavMeshAgent agent;
    private bool waiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GoToNewWindow();
    }

    void Update()
    {
        if (!waiting && !agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            StartCoroutine(BreakWindow());
        }
    }

    IEnumerator BreakWindow()
    {
        waiting = true;

        Debug.Log("Intentando Romper la ventana");

        yield return new WaitForSeconds(breakTime);

        GoToNewWindow();

        waiting = false;
    }

    void GoToNewWindow()
    {
        int randomIndex = Random.Range(0, windows.Length);

        agent.SetDestination(windows[randomIndex].position);
    }
}
