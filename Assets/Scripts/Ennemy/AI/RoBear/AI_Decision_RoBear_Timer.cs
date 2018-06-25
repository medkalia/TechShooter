using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Timer")]
public class AI_Decision_RoBear_Timer : AI_Decision
{
    public Enemy.TimerType timerType;
    public RoBear2.AttackStates attackStateType;
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool timerEnded = Timer();
        return timerEnded;
    }

    private bool Timer()
    {
        return roBear.CheckStateTime(roBear.GetTimerDuration(timerType)) && (roBear.pickedAttackStates[0] == attackStateType);
        
    }

}
