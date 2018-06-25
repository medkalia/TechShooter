using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamForwardState : ForwardState
{
    private RoboRam roboRam;

    public override void Enter(Enemy enemy)
    {
        roboRam = (RoboRam)enemy;
    }

    public override void Execute()
    {
        if (roboRam.roboRamMovementInfo.FacingDirection == -1)
        {
            if (roboRam.EnemyTransform.position.x > roboRam.roboRamMovementInfo.ramDestination.x)
                Forward();
            else
                roboRam.ChangeState(new RoboRamDechargeState());
        }
        else
        {
            if (roboRam.EnemyTransform.position.x < roboRam.roboRamMovementInfo.ramDestination.x)
                Forward();
            else
                roboRam.ChangeState(new RoboRamDechargeState());
        }
           
        

    }

    public override void Exit()
    {
        roboRam.roboRamMovementInfo.isForwarding = false;
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

    protected override void Forward()
    {
        roboRam.Move();
    }
}
