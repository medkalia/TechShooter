using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBearIdleState : IdleState
{
    private RoBear roBear;

    public override void Enter(Enemy enemy)
    {
        roBear = (RoBear) enemy;
        if (!roBear.stateInfo.isDying)
        {
            idleDuration = roBear.bossStats.idleDuration;
        }
            
    }

    public override void Execute()
    {
        Idle();
    }

    public override void Exit()
    {
        roBear.stateInfo.isInCycle = true;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public override void OnTriggerStay2D(Collider2D other)
    {

    }

    public override void OnTriggerExit2D(Collider2D other)
    {

    }

    public override void Idle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration && !roBear.stateInfo.startedRangedAttacking && !roBear.enemyInfo.isMeleeAttacking && !roBear.enemyInfo.isRangedAttacking && !roBear.stateInfo.isSummoning && !roBear.stateInfo.startedMeleeAttacking)
        {
            roBear.BuildCycle();

            roBear.PickNextAttackState();
        }
        else
        {
            Debug.Log("startedMeleeAttacking" + roBear.stateInfo.startedMeleeAttacking);
            Debug.Log("startedRangedAttacking" + roBear.stateInfo.startedRangedAttacking);
            Debug.Log("isMeleeAttacking" + roBear.enemyInfo.isMeleeAttacking);
            Debug.Log("isRangedAttacking" + roBear.enemyInfo.isRangedAttacking);
            Debug.Log("isSummoning" + roBear.stateInfo.isSummoning);
        }
    }

    
}
