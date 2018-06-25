using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Animation/Rest")]
public class AI_Action_Robear_RestAnimation : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        BuildCycle();
        Animate();
    }

    private void Animate()
    {
        roBear.HandleAnimation(roBear.parameterIdleHash);
    }

    private void BuildCycle()
    {
        roBear.BuildCycle();
    }
}
