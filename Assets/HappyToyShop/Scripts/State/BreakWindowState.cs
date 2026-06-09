using UnityEngine;
using UnityEngine.AI;

public class BreakWindowState : IState
{
    
   
    private EnemyShadow enemy;

    private float waitTimer;


    public BreakWindowState(EnemyShadow enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log(" Rompiendo las ventanas");

        GameObject wood = enemy.currentWindow.Find("WoodPlanks").gameObject;

         wood.SetActive(false);

        enemy.currentWindow = enemy.windows[Random.Range(0, enemy.windows.Length)];

        enemy.stateMachine.ChangeState(new GoToWindowState(enemy));
    }
    public void Exit()
    {

    }
    public void Update()
    {

    }
    
    
}
