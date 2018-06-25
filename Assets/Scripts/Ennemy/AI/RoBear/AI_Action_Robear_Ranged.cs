using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Action/RoBear/Ranged")]
public class AI_Action_Robear_Ranged : AI_Action
{
    private RoBear2 roBear;

    public override void Act(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        Ranged();
    }

    private void Ranged()
    {
        
        if (roBear.stateInfo.startedRangedAttacking)
        {
            roBear.stateInfo.startedRangedAttacking = false;
            roBear.StartCoroutine(ShootAndWait());
        }
    }

    public IEnumerator ShootAndWait()
    {
        int i = 1;
        for (i = 1; i <= roBear.bossStats.numberOfNormalShots; i++)
        {
            if (roBear.enemyInfo.isRangedAttacking)
                ShootProjectile();
            else
                break;
            yield return new WaitForSeconds(roBear.bossStats.rangedCD);
        }
        roBear.enemyInfo.isRangedAttacking = false;
    }

    public void ShootProjectile()
    {
        GameObject newShot = Instantiate(roBear.shot, roBear.shotSpawnTransform.position, new Quaternion());
        newShot.transform.parent = roBear.shotSpawnTransform;
    }
}
