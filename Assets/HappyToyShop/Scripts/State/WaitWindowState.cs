using UnityEngine;

public class WaitWindowState : IState
{
    private RandomGuy enemy;

    private float timer;

    public WaitWindowState(RandomGuy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        timer = 5f;
        Debug.Log("Esperando ala ventana");
    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            enemy.stateMachine.ChangeState(new BreakWindowState(enemy));
        }
    }

    public void Exit()
    {
        
    }
}
