using UnityEngine;

public class ChasePlayerState : IState
{
    private EnemyShadow enemy;

    public ChasePlayerState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Persiguiendo jugador");
    }

    public void Update()
    {
        if (enemy.player != null)
        {
            enemy.agent.SetDestination(enemy.player.position);
        }
    }

    public void Exit()
    {
    }
}

