using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboTurtleMeleeState : MeleeState
{
    private RoboTurtle roboTurtle;

    public override void Enter(Enemy enemy)
    {
        roboTurtle = (RoboTurtle) enemy;
        meleeCD = roboTurtle.enemyStats.meleeCD;
    }


    public override void Execute()
    {
        
        if (roboTurtle.InRangedRange && !roboTurtle.InMeleeRange)
        {
            RoboTurtleRangedState newState = new RoboTurtleRangedState();
            newState.canRanged = true;
            roboTurtle.ChangeState(newState);
        }
        else if (roboTurtle.target == null)
        {
            roboTurtle.ChangeState(new RoboTurtleIdleState());
        }
        else if (roboTurtle.target != null)
        {
            Melee();
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

    public override void Melee()
    {
        meleeTimer += Time.deltaTime;
        if (meleeTimer >= meleeCD)
        {
            canMelee = true;
            meleeTimer = 0;
        }
        if (canMelee)
        {
            canMelee = false;
            roboTurtle.Attack();

        }

    }

    
}
