using UnityEngine;

public class WaitWindowState : IState
{
    private EnemyShadow enemy;

    private float timer;

    public WaitWindowState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        timer = 5f;

        if (enemy.isVent)
            Debug.Log("Esperando en ventilación");
        else
            Debug.Log("Esperando en ventana");
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
