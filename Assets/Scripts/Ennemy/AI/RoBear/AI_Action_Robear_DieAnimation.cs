using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Die")]
public class AI_Action_Robear_DieAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Animate();
    }

    private void Animate()
    {
        roBear.Die();
        roBear.PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.DUYING);
        roBear.HandleAnimation(roBear.parameterDieHash);
        roBear.aiActive = false;
    }
}
