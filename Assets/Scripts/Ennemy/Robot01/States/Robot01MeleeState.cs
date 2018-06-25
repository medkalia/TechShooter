using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot01MeleeState : MeleeState
{
    private Robot01 robot01;

    public override void Enter(Enemy enemy)
    {
        robot01 = (Robot01) enemy;
        meleeCD = robot01.enemyStats.meleeCD;
    }


    public override void Execute()
    {
        
        if (robot01.InRangedRange && !robot01.InMeleeRange)
        {
            Robot01RangedState newState = new Robot01RangedState();
            newState.canRanged = false;
            robot01.ChangeState(newState);
        }
        else if (robot01.target == null)
        {
            robot01.ChangeState(new Robot01IdleState());
        }
        else if (robot01.target != null)
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
            robot01.Attack();

        }

    }

    
}
