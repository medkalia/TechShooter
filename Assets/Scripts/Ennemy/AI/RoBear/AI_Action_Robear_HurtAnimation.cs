using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Hurt")]
public class AI_Action_Robear_HurtAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
        UpdateHealthMileStone();
        UpdateAttackCycle();
    }

    private void Animate()
    {
        roBear.stateInfo.isHurting = true;
        roBear.PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.FATIGUE);
        roBear.HandleAnimation(roBear.parameterHurtHash);
    }

    private void UpdateHealthMileStone()
    {
        roBear.stateInfo.nextHealthMilestone = roBear.stateInfo.nextHealthMilestone - ((roBear.stateInfo.milestonePercentage * roBear.enemyStats.baseHealth) / 100);
        roBear.bossStats.rageLevel += 1;
        roBear.bossStats.Enrage(roBear.bossStats.rageLevel);
    }

    private void UpdateAttackCycle()
    {
        roBear.pickedAttackStates = new List<RoBear2.AttackStates>();
    }
}
