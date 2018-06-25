using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot01RangedState : RangedState
{

    private Robot01 robot01;

    public override void Enter(Enemy enemy)
    {
        robot01 = (Robot01) enemy;
        rangedCD = robot01.enemyStats.rangedCD;
    }

    public override void Execute()
    {
        
        if (robot01.InMeleeRange)
        {
            Robot01MeleeState newState = new Robot01MeleeState();
            robot01.ChangeState(newState);
        }
        else if (robot01.target != null && robot01.InRangedRange)
        {
            Ranged();
        }else if (robot01.target == null || robot01.target != null && !robot01.InRangedRange)
        {
            robot01.ChangeState(new Robot01IdleState());
        }
        

    }

    public override void Exit()
    {
        
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

    public override void Ranged()
    {
        rangedTimer += Time.deltaTime;
        if (rangedTimer >= rangedCD)
        {
            canRanged = true;
            rangedTimer = 0;
        }
        if (canRanged)
        {
            canRanged = false;
            robot01.Shoot();

        }
    }

}
