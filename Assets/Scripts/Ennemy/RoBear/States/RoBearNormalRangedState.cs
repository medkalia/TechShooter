using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBearNormalRangedState : IEnemyState
{
    private RoBear roBear;
    public bool startedAttacking = false;

    public  void Enter(Enemy enemy)
    {
        roBear = (RoBear)enemy;
        if (!roBear.stateInfo.isDying)
        {
            roBear.EnemyAnim.SetTrigger(roBear.parameterRangedHash);
        }
           
    }

    public void Execute()
    {
        startedAttacking = roBear.stateInfo.startedRangedAttacking;
        if (startedAttacking)
        {
            if (roBear.enemyInfo.isRangedAttacking)
            {
                startedAttacking = false;
                roBear.stateInfo.startedRangedAttacking = false;
                NormalRanged();
            }
        }
    }

    public void Exit()
    {
        roBear.enemyInfo.isRangedAttacking = false;
        roBear.stateInfo.startedRangedAttacking = false;
    }

    public  void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public  void OnTriggerStay2D(Collider2D other)
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {

    }

    protected  void NormalRanged()
    {
        roBear.Shoot();
    }
}
