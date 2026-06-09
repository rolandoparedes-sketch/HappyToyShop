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
        enemy.agent.SetDestination(enemy.currentWindow.position);
        Debug.Log("Yendo a Ventana");
    }
   
    public void Update()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.currentWindow.position);

        if (distance < 2f)
        {
            enemy.stateMachine.ChangeState(new WaitWindowState(enemy));
        }
    }
    public void Exit()
    {

    }
}

