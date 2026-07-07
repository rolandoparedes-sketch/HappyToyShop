using UnityEngine;

public class GoToWindowState : IState
{
    private EnemyShadow enemy;

    public GoToWindowState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Ventana elegida: " + enemy.currentWindow.name);

        Transform outsidePoint =
            enemy.currentWindow;

        if (outsidePoint == null)
        {
            Debug.LogError("No existe OutsidePoint en " + enemy.currentWindow.name);
            return;
        }

        enemy.agent.SetDestination(outsidePoint.position);
    }

    public void Update()
    {
        Transform outsidePoint =
            enemy.currentWindow;

        float distance = Vector3.Distance(
            enemy.transform.position,
            outsidePoint.position);

        if (distance < 2f)
        {
            enemy.stateMachine.ChangeState(
                new WaitWindowState(enemy));
        }
    }

    public void Exit()
    {

    }
}
