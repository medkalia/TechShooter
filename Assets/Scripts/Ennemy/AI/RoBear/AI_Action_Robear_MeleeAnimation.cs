using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Melee")]
public class AI_Action_Robear_MeleeAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
        StartMelee();
    }

    private void Animate()
    {
        roBear.HandleAnimation(roBear.parameterMeleeHash);
    }

    private void StartMelee()
    {
        roBear.enemyInfo.isMeleeAttacking = true;
    }
}
