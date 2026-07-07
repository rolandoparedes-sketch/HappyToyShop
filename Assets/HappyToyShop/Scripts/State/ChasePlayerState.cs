using UnityEngine;

public class ChasePlayerState : IState
{
    private RandomGuy enemy;

    public ChasePlayerState(RandomGuy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Persiguiendo jugador");

        enemy.agent.speed = enemy.chaseSpeed;
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

