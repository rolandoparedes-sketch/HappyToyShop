using UnityEngine;
using UnityEngine.AI;

public class BreakWindowState : IState
{
    
   
    private EnemyShadow enemy;

    private float timer;

    private WoodPlanks planks;

    public BreakWindowState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        GameObject wood =
        enemy.currentWindow.Find("WoodPlanks").gameObject;

        if (!wood.activeSelf)
        {
            Transform insidePoint =
                enemy.currentWindow.Find("InsidePoint");

            enemy.agent.Warp(insidePoint.position);

            enemy.stateMachine.ChangeState(
                new ChasePlayerState(enemy));

            return;
        }

        Debug.Log("Rompiendo tablas");

        planks = wood.GetComponent<WoodPlanks>();

        timer = 1f;
    }
    public void Exit()
    {

    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            planks.health--;

            Debug.Log("Vida tablas: " + planks.health);

            timer = 1f;

            if (planks.health <= 0)
            {
                GameObject wood =
                    enemy.currentWindow.Find("WoodPlanks").gameObject;

                wood.SetActive(false);

                Debug.Log("Tablas rotas");  
            }
        }
    }
    
    
}
