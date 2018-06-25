using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamIdleState : IdleState
{
    private RoboRam roboRam;

    public override void Enter(Enemy enemy)
    {
        roboRam = (RoboRam) enemy;
        roboRam.EnemyRB.velocity = new Vector2(Mathf.Lerp(roboRam.EnemyRB.velocity.x, 0, 1), roboRam.EnemyRB.velocity.y);
    }

    public override void Execute()
    {
        Idle();
        if(roboRam.target != null)
        {
            roboRam.ChangeState(new RoboRamChargeState());
        }
    }

    public override void Exit()
    {
        roboRam.roboRamMovementInfo.isTurning = true;
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
        
        roboRam.LookAtTarget();
    }

    
}
