using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Summon")]
public class AI_Action_Robear_Summon : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Summon();
    }

    private void Summon()
    {
        
        if (roBear.stateInfo.canSummonNext)
        {
            roBear.stateInfo.canSummonNext = false;
            Instantiate(roBear.summonPool[0], roBear.summonSpawn.position, roBear.summonSpawn.rotation);
            roBear.stateInfo.summonCounter--;
            if (roBear.stateInfo.summonCounter <= 0)
            {
                roBear.stateInfo.isSummoning = false;
            }
        }
    }

}
