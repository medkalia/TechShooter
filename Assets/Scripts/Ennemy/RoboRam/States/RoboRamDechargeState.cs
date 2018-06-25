using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamDechargeState : DechargeState
{
    private RoboRam roboRam;

    public override void Enter(Enemy enemy)
    {
        roboRam = (RoboRam)enemy;
        roboRam.roboRamMovementInfo.isDecharging = true;
        roboRam.EnemyAnim.SetTrigger(roboRam.parameterDechargeHash);
    }

    public override void Execute()
    {
        Decharge();
        if (!roboRam.roboRamMovementInfo.isDecharging)
        {
            
            roboRam.ChangeState(new RoboRamIdleState());
        }
    }

    public override void Exit()
    {
        roboRam.enemyInfo.isMeleeAttacking = false;
        EnemySight sight = (roboRam.gameObject.GetComponentInChildren<EnemySight>());
        sight.GetComponent<BoxCollider2D>().size = new Vector2( sight.originalSightSize , sight.GetComponent<BoxCollider2D>().size.y);
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

    protected override void Decharge()
    {
        roboRam.Move();
    }
}
