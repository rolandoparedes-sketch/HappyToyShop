using UnityEngine;

public class ChaseState : IState
{
    private StateMachine stateMachine;
    private EnemyController enemyController;

    public ChaseState(StateMachine stateMachine, EnemyController enemyController)
    {
        this.stateMachine = stateMachine;
        this.enemyController = enemyController;
    }

    public void Enter()
    {
        Debug.Log("Enemigo Empezo a perseguir al player");
    }
    public void Update()
    {
        float distancePlayer = Vector3.Distance(enemyController.transform.position, enemyController.PlayerTransform.position);
        if(distancePlayer <= enemyController.AttackRange)
        {
            stateMachine.ChangeState(enemyController.AttackState);
            return;

        }
        if(distancePlayer >= enemyController.AttackRange)
        {

            stateMachine.ChangeState(enemyController.RoamState);
            return;
        }

        enemyController.Agent.SetDestination(enemyController.PlayerTransform.position);


        

    }

    public void Exit()
    {
        Debug.Log("Saliendo del ChaseState");

        enemyController.Agent.ResetPath();
    }


}