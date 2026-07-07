
using UnityEngine;
using UnityEngine.AI;

public class BreakWindowState : IState
{
    private RandomGuy enemy;

    private float timer;

    private bool waitingToEnter;
    private float enterTimer;

    private WoodPlanks planks;

    public BreakWindowState(RandomGuy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        GameObject wood =
            enemy.currentWindow.Find("WoodPlanks").gameObject;

        
        if (!wood.activeSelf)
        {
            waitingToEnter = true;
            enterTimer = 20f;

            Debug.Log("Esperando a que el jugador repare las tablas...");

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
        
        if (waitingToEnter)
        {
            enterTimer -= Time.deltaTime;

            GameObject wood =
                enemy.currentWindow.Find("WoodPlanks").gameObject;

            
            if (wood.activeSelf)
            {
                Debug.Log("El jugador reparó las tablas.");

                waitingToEnter = false;

                enemy.GoIdle();

                return;
            }

           
            if (enterTimer <= 0)
            {
                Debug.Log("El jugador no reparó las tablas.");

                Transform insidePoint =
                    enemy.currentWindow.Find("InsidePoint");

                enemy.agent.Warp(insidePoint.position);

                enemy.stateMachine.ChangeState(
                    new ChasePlayerState(enemy));

                return;
            }

            return;
        }

        
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (planks.health > 0)
            {
                planks.health--;

                Debug.Log("Vida tablas: " + planks.health);
            }

            timer = 1f;
        }

       
        if (planks.health <= 0)
        {
            planks.health = 0;

            GameObject wood =
                enemy.currentWindow.Find("WoodPlanks").gameObject;

            wood.SetActive(false);

            Debug.Log("Tablas rotas");

            waitingToEnter = true;
            enterTimer = 20f;
        }
    }
}





