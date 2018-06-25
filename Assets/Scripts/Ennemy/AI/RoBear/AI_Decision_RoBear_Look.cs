using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Look")]
public class AI_Decision_RoBear_Look : AI_Decision
{
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool targetVisivle = Look(roBear);
        return targetVisivle;
    }

    private bool Look(RoBear2 roBear)
    {
        return (roBear.target != null);
    }
}
