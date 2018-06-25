using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Melee")]
public class AI_Action_Robear_Melee : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Melee();
    }

    private void Melee()
    {
        
        if (roBear.stateInfo.startedMeleeAttacking)
        {
            roBear.stateInfo.startedMeleeAttacking = false;
            roBear.stateInfo.currentMeleeShot = Instantiate(roBear.meleeShot, roBear.shotSpawnTransform.position, roBear.shotSpawnTransform.rotation);
            roBear.stateInfo.currentMeleeShot.transform.parent = roBear.shotSpawnTransform;
            roBear.StartCoroutine(ChargeShot());
        }
        else
        {
            if (roBear.stateInfo.canLaunchMelee)
            {
                roBear.stateInfo.canLaunchMelee = false;
                if (roBear.GetComponentInChildren<LineWithChargeProjectileMover>() != null)
                    roBear.GetComponentInChildren<LineWithChargeProjectileMover>().Release();
                roBear.stateInfo.currentMeleeShot = null;
                roBear.enemyInfo.isMeleeAttacking = false;
            }
        }
        
    }
    public IEnumerator ChargeShot()
    {
        float chargeTime = 0f;
        while (chargeTime < roBear.bossStats.normalMeleeChargingTime)
        {
            chargeTime += .5f;
            yield return new WaitForSeconds(.5f);
        }
        roBear.HandleAnimation(roBear.parameterLaunchMeleeHash);
    }

}
