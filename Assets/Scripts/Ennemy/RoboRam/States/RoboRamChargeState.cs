using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamChargeState : ChargeState
{
    private RoboRam roboRam;

    public override void Enter(Enemy enemy)
    {
        roboRam = (RoboRam)enemy;
        roboRam.enemyInfo.isMeleeAttacking = true;
    }

    public override void Execute()
    {
        Charge();
        if (!roboRam.roboRamMovementInfo.isCharging && roboRam.roboRamMovementInfo.isForwarding)
        {
            roboRam.ChangeState(new RoboRamForwardState());
        }
    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        
    }

    protected override void Charge()
    {
        if (!roboRam.roboRamMovementInfo.isTurning && !roboRam.roboRamMovementInfo.isCharging && !roboRam.roboRamMovementInfo.isForwarding)
        { 
            roboRam.roboRamMovementInfo.isCharging = true;
        }
        roboRam.Move();
    }
}
