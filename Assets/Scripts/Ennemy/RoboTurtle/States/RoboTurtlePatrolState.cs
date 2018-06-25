using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboTurtlePatrolState : PatrolState
{

    private RoboTurtle roboTurtle;
    

    public override void Enter(Enemy roboTurtle)
    {
        patrolDuration = Random.Range(patrolDuration/2 , patrolDuration + 1);
        this.roboTurtle = (RoboTurtle) roboTurtle;
    }

    public override void Execute()
    {
        if (roboTurtle.InRangedRange)
        {
            roboTurtle.ChangeState(new RoboTurtleRangedState());
        }
        else
        {
            Patrol();
        }
        
    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            if (roboTurtle.enemyMovementInfo.FacingDirection == 1)
            {
                roboTurtle.enemyMovementInfo.canMoveRight = false;
            }
            else
            {
                roboTurtle.enemyMovementInfo.canMoveLeft = false;
            }
            roboTurtle.enemyMovementInfo.FacingDirection *= -1;
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            roboTurtle.enemyMovementInfo.FacingDirection = roboTurtle.enemyMovementInfo.canMoveRight ? 1 : -1;
        }
        
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            if (roboTurtle.enemyMovementInfo.FacingDirection == 1)
            {
                roboTurtle.enemyMovementInfo.canMoveLeft = true;
            }
            else
            {
                roboTurtle.enemyMovementInfo.canMoveRight = true;
            }
        }
        
    }

    public override void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            roboTurtle.ChangeState(new RoboTurtleIdleState());
        }
        else
        {
            roboTurtle.Move();
        }
    }
}
