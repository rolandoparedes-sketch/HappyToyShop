using UnityEngine;

public class GoToWindowState : IState
{
    private EnemyShadow enemy;
    private Transform outsidePoint;

    public GoToWindowState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Destino: " + enemy.currentWindow.name);

        outsidePoint = enemy.currentWindow.Find("OutsidePoint");

        if (outsidePoint == null)
        {
            Debug.LogError("No existe OutsidePoint en " + enemy.currentWindow.name);
            return;
        }

        enemy.agent.SetDestination(outsidePoint.position);
    }

    public void Update()
    {
        if (outsidePoint == null) return;

        float distance = Vector3.Distance(enemy.transform.position, outsidePoint.position);

        if (distance < 2f)
        {
            enemy.stateMachine.ChangeState(new WaitWindowState(enemy));
        }
    }

    public void Exit()
    {
    }
}
