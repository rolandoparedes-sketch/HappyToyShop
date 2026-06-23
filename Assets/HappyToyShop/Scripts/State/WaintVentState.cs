using UnityEngine;

public class WaintVentState : IState
{
    private EnemyShadow enemy;
    private float timer;

    public WaintVentState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        timer = 10f;

        Debug.Log("Esperando en la Ventilacion");
    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Transform insidePoint = enemy.currentWindow.Find("InsidePoint");

            enemy.agent.Warp(insidePoint.position);

            enemy.stateMachine.ChangeState(new ChasePlayerState(enemy));
        }
    }

    public void Exit()
    {

    }
}
