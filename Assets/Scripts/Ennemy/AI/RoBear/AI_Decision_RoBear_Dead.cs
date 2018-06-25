using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BIJ/AI/Decision/RoBear/Dead")]
public class AI_Decision_RoBear_Dead : AI_Decision
{
    private RoBear2 roBear;

    public override bool Decide(Enemy enemy)
    {
        roBear = (RoBear2)enemy;
        bool isDead = CheckHealth();
        return isDead;
    }

    private bool CheckHealth()
    {
        return (roBear.IsDead);
    }

}
