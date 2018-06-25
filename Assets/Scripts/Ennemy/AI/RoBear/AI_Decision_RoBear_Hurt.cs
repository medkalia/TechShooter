using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Hurt")]
public class AI_Decision_RoBear_Hurt : AI_Decision
{
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool isHurt = CheckHealth();
        return isHurt;
    }

    private bool CheckHealth()
    {
        return (roBear.enemyStats.currentHealth < roBear.stateInfo.nextHealthMilestone) && (!roBear.IsDead);
    }

}
