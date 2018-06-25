using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Finish attack")]
public class AI_Decision_RoBear_FinishAttack : AI_Decision
{
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool finishedRanged = CheckRanged();
        return finishedRanged;
    }

    private bool CheckRanged()
    {
        bool test =  (!roBear.enemyInfo.isRangedAttacking &&
            !roBear.enemyInfo.isMeleeAttacking &&
            !roBear.stateInfo.isSummoning);

        if (test)
        {
            if (roBear.pickedAttackStates.Count > 0)
            {
                //Debug.Log(roBear.currentAIState.name + " is Removing Attack state " + EnumUtil.GetString(roBear.pickedAttackStates[0]) + " and moving to the next" + (roBear.pickedAttackStates.Count > 2 ? EnumUtil.GetString(roBear.pickedAttackStates[1]) : " NONE"));
                roBear.pickedAttackStates.RemoveAt(0);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
