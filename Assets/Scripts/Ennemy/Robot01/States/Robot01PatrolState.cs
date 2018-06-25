using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot01PatrolState : PatrolState
{

    private Robot01 robot01;

    public override void Enter(Enemy robot01)
    {
        patrolDuration = Random.Range(patrolDuration / 2, patrolDuration + 1);
        this.robot01 = (Robot01) robot01;
        Patrol();
    }

    public override void Execute()
    {
        
        
        if (robot01.target != null && robot01.InRangedRange)
        {
            robot01.ChangeState(new Robot01RangedState());
        }
        else
        {
            Patrol();
            robot01.Move();
        }
        
    }

    public override void Exit()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            if (robot01.enemyMovementInfo.FacingDirection == 1)
            {
                robot01.enemyMovementInfo.canMoveRight = false;
            }else
            {
                robot01.enemyMovementInfo.canMoveLeft = false;
            }
            robot01.enemyMovementInfo.FacingDirection *= -1;
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag == "Edge")
        {
            robot01.enemyMovementInfo.FacingDirection = robot01.enemyMovementInfo.canMoveRight ? 1 : -1;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        //we are out of the edge so we can move left/right again when needed
        if (other.tag == "Edge")
        {
            if (robot01.enemyMovementInfo.FacingDirection == 1)
            {
                robot01.enemyMovementInfo.canMoveLeft = true;
            }
            else
            {
                robot01.enemyMovementInfo.canMoveRight = true;
            }
        }
    }

    public override void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            robot01.ChangeState(new Robot01IdleState());
        }
    }
}
