using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot01IdleState : IdleState
{
    private Robot01 robot01;

    public override void Enter(Enemy enemy)
    {
        idleDuration = Random.Range(idleDuration / 2, idleDuration + 1);
        robot01 = (Robot01) enemy;
    }

    public override void Execute()
    {
        

        if(robot01.target != null)
        {
            robot01.ChangeState(new Robot01PatrolState());
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

    public override void OnTriggerExit2D(Collider2D other)
    {

    }

    public override void OnTriggerStay2D(Collider2D other)
    {

    }

    public override void Idle()
    {
        robot01.Robot01JetAnim.SetFloat(robot01.parameterSpeedHash, 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            robot01.ChangeState(new Robot01PatrolState());
        }
    }

    
}
