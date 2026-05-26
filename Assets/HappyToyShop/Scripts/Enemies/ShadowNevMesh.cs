using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Sirenix.OdinInspector;
public class EnemyShadow : MonoBehaviour
{
    public Transform[] windows;

    public float breakTime = 5f;

    private NavMeshAgent agent;
    public bool waiting = false;

    private int LastWindow;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(BreakWindow());
    }

    



    IEnumerator BreakWindow()
    {
        /* while (!waiting && !agent.pathPending && agent.remainingDistance <= 0.5f)
         {
             waiting = true;

             Debug.Log("Intentando Romper la ventana");

             yield return new WaitForSeconds(breakTime);
             Debug.Log("Cambiando Ventana");
             GoToNewWindow();

             waiting = false;
         }*/
        GoToNewWindow();
        yield return new WaitUntil(() => agent.remainingDistance<= 0.001f);
        Debug.Log(agent.remainingDistance);


        yield return new WaitForSeconds(breakTime);

        StartCoroutine(BreakWindow());
    }
    [Button]
    void GoToNewWindow()
    {
        Debug.Log("SelectWindow");
        int randomIndex = Random.Range(0, windows.Length);
        if(randomIndex == LastWindow)
        {
            GoToNewWindow();
            return;
        }
        agent.SetDestination(windows[randomIndex].position);
        LastWindow = randomIndex;
        
    }
}
