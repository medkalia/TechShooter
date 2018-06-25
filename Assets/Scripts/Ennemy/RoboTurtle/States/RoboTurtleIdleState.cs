using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboTurtleIdleState : IdleState
{
    private RoboTurtle roboTurtle;
    private bool hasHidden = false;

    public override void Enter(Enemy enemy)
    {
        idleDuration = Random.Range(2, idleDuration + 1);
        roboTurtle = (RoboTurtle) enemy;
    }

    public override void Execute()
    {
        if(roboTurtle.target != null)
        {
            roboTurtle.ChangeState(new RoboTurtlePatrolState());
        }
        else
        {
            Idle();
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

    public override void Idle()
    {
        hasHidden = roboTurtle.PlaySoundWithAnimation(AudioParams.SoundPoolGroups.ROBOTURTLE, AudioParams.SoundPools.DUCKING, "Idle", hasHidden);
        roboTurtle.EnemyAnim.SetFloat(roboTurtle.parameterSpeedHash, 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            roboTurtle.ChangeState(new RoboTurtlePatrolState());
        }
    }

    
}
