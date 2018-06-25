using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBearNormalMeleeState : IEnemyState
{
    private RoBear roBear;
    private bool startedAttacking = false;

    public  void Enter(Enemy enemy)
    {
        roBear = (RoBear)enemy;
        if (!roBear.stateInfo.isDying)
        {
            roBear.EnemyAnim.SetTrigger(roBear.parameterMeleeHash);
        }
    }

    public void Execute()
    {
        startedAttacking = roBear.stateInfo.startedMeleeAttacking;
        if (startedAttacking)
        {
            if (roBear.enemyInfo.isMeleeAttacking)
            {
                startedAttacking = false;
                roBear.stateInfo.startedMeleeAttacking = false;
                NormalMelee();
            }
        }
    }

    public void Exit()
    {
        roBear.enemyInfo.isMeleeAttacking = false;
        roBear.stateInfo.startedMeleeAttacking = false;
    }

    public  void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
    }

    public  void OnTriggerStay2D(Collider2D other)
    {
        
    }

    protected  void NormalMelee()
    {
       roBear.ReleaseShot();
    }
}
