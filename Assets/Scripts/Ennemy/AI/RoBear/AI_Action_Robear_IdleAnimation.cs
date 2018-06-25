using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Idle")]
public class AI_Action_Robear_IdleAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
        UpdateAttackCycle();


    }

    private void Animate()
    {
        roBear.HandleAnimation(roBear.parameterIdleHash);
    }

    private void UpdateAttackCycle ()
    {
        roBear.pickedAttackStates = new List<RoBear2.AttackStates>();
    }
}
