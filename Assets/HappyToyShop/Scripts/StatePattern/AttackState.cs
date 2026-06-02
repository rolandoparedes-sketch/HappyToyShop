using System;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IState
{
    private StateMachine stateMachine;
    private EnemyController enemyController;
    public float cooldowntimer;
    public AttackState(StateMachine stateMachine, EnemyController enemyController)
    {
        this.stateMachine = stateMachine;
        this.enemyController = enemyController;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        float distancePlayer = Vector3.Distance(enemyController.transform.position, enemyController.PlayerTransform.position);
        if (distancePlayer > enemyController.AttackRange)
        {

            stateMachine.ChangeState(enemyController.ChaseState);
            return;
        }


        cooldowntimer -= Time.deltaTime;

        if(cooldowntimer <= 0)
        {
            PerformAttack();
            cooldowntimer = enemyController.AttackCooldown;
        }
        Vector3 dirrecionToPlayer =(enemyController.PlayerTransform.position - enemyController.PlayerTransform.position).normalized;
        dirrecionToPlayer.y = 0f;

        enemyController.transform.rotation = Quaternion.LookRotation(dirrecionToPlayer);


    }

    private void PerformAttack()
    {
        Debug.Log("attacando...");
    }
}