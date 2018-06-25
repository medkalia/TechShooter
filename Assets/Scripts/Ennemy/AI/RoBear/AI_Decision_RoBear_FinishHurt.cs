using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Finish hurt")]
public class AI_Decision_RoBear_FinishHurt : AI_Decision
{
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool finishedHurting = CheckHurting();
        return finishedHurting;
    }

    private bool CheckHurting()
    {
        return (!roBear.stateInfo.isHurting);
    }
}
