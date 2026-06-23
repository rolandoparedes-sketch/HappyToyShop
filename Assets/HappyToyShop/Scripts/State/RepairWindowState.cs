using UnityEngine;

public class RepairWindowState : IState
{
    private EnemyShadow enemy;
    private WoodPlanks planks;

    private float timer;

    public RepairWindowState(EnemyShadow enemy, WoodPlanks planks)
    {
        this.enemy = enemy;
        this.planks = planks;
    }

    public void Enter()
    {
        timer = 10f;

        if (enemy.warningText != null)
            enemy.warningText.SetActive(true);

        Debug.Log("Tienes 10 segundos para reparar las tablas");
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (planks.health > 0)
        {
            if (enemy.warningText != null)
                enemy.warningText.SetActive(false);

            enemy.stateMachine.ChangeState(
                new IdleState(enemy));

            return;
        }
        if (timer <= 0)
        {
            if (enemy.warningText != null)
                enemy.warningText.SetActive(false);

            GameObject wood =
                enemy.currentWindow.Find("WoodPlanks").gameObject;

            wood.SetActive(false);

            Transform insidePoint =
                enemy.currentWindow.Find("InsidePoint");

            enemy.agent.Warp(insidePoint.position);

            enemy.stateMachine.ChangeState(
                new ChasePlayerState(enemy));

            return;
        }
    }

    public void Exit()
    {
    }
}

