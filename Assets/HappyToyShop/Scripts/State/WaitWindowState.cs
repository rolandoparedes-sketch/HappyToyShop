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
        timer = 15f;
        Debug.Log("Esperando ala ventana");
    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (enemy.isVent)
            {
                enemy.stateMachine.ChangeState(new ChasePlayerState(enemy));
            }
            else
            {
                enemy.stateMachine.ChangeState(new BreakWindowState(enemy));
            }
        }
    }

    public void Exit()
    {
        
    }
}
