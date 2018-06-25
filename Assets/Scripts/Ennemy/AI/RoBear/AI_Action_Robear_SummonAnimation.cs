using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Summon")]
public class AI_Action_Robear_SummonAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
        StartSummon();
    }

    private void Animate()
    {
        roBear.HandleAnimation(roBear.parameterSummonHash);
    }

    private void StartSummon()
    {
        roBear.stateInfo.isSummoning = true;
        roBear.PlaySoundOnce(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.SUMMONING);
        roBear.stateInfo.summonCounter = roBear.bossStats.summonNumber;
    }
}
