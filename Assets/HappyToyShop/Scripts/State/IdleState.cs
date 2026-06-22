using UnityEngine;

public class IdleState : IState
{
    private EnemyShadow enemy;
    private float timer;

    public IdleState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        timer = 10f;
        Debug.Log("Enemy en estado de Reposo");
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            int r = Random.Range(0, 2);

            if (r == 0)
            {
                enemy.isVent = false;
                enemy.currentWindow = enemy.windows[Random.Range(0, enemy.windows.Length)];
            }
            else
            {
                enemy.isVent = true;
                enemy.currentWindow = enemy.VentPoints[Random.Range(0, enemy.VentPoints.Length)];
            }

            enemy.stateMachine.ChangeState(new GoToWindowState(enemy));
        }
    }
    public void Exit()
    {

    }
}