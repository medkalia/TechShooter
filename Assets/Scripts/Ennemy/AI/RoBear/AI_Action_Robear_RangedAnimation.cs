using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Ranged")]
public class AI_Action_Robear_RangedAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
        StartAttack();
    }

    private void Animate()
    {
        roBear.HandleAnimation(roBear.parameterRangedHash);
    }

    private void StartAttack()
    {
        roBear.enemyInfo.isRangedAttacking = true;
    }
}
