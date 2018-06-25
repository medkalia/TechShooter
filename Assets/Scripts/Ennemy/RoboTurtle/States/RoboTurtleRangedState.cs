using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboTurtleRangedState : RangedState
{

    private RoboTurtle roboTurtle;

    public override void Enter(Enemy enemy)
    {
        roboTurtle = (RoboTurtle) enemy;
        rangedCD = roboTurtle.enemyStats.rangedCD;
    }

    public override void Execute()
    {
        
        if (roboTurtle.InMeleeRange)
        {
            roboTurtle.ChangeState(new RoboTurtleMeleeState());
        }
        else if (roboTurtle.target != null && roboTurtle.InRangedRange)
        {
            Ranged();
        }
        else if (roboTurtle.target == null || roboTurtle.target != null && !roboTurtle.InRangedRange)
        {
            roboTurtle.ChangeState(new RoboTurtleIdleState());
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
            roboTurtle.Shoot();

        }
    }

}
