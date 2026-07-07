using UnityEngine;

public class IdleState : IState
{
    private RandomGuy enemy;
    private float timer;

    public IdleState(RandomGuy enemy)
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
            float totalChance =
                enemy.windowChance + enemy.ventChance;

            float randomValue =
                Random.Range(0f, totalChance);

            if (randomValue < enemy.windowChance)
            {
                enemy.isVent = false;

                enemy.currentWindow =
                    enemy.windows[
                        Random.Range(0, enemy.windows.Length)
                    ];


                Debug.Log("Ataque por Ventana");
            }
            else
            {
                enemy.isVent = true;

                enemy.currentWindow =
                    enemy.VentPoints[
                        Random.Range(0, enemy.VentPoints.Length)
                    ];
                enemy.AlertText.SetActive(true);
                Debug.Log("Ataque por Ventilación");

            }

            enemy.stateMachine.ChangeState(
                new GoToWindowState(enemy));

        }
    }

    public void Exit()
    {

    }
}