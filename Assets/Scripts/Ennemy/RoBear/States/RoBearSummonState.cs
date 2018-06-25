using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBearSummonState : IEnemyState
{
    private RoBear roBear;

    public  void Enter(Enemy enemy)
    {
        roBear = (RoBear)enemy;
        if (!roBear.stateInfo.isDying)
        {
            roBear.stateInfo.summonCounter = roBear.bossStats.summonNumber;
            roBear.stateInfo.isSummoning = true;
            roBear.EnemyAnim.SetTrigger(roBear.parameterSummonHash);
            roBear.PlaySoundOnce(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.SUMMONING);
        }
        
        

    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        roBear.stateInfo.isSummoning = false;
    }

    public  void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public  void OnTriggerStay2D(Collider2D other)
    {
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {

    }

    protected  void Summon()
    {

    }
}
