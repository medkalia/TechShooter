using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateRangedBehaviour : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RoBear roBear = animator.gameObject.GetComponent<RoBear>();
        roBear.enemyInfo.isRangedAttacking = true;
        roBear.stateInfo.startedRangedAttacking = true;
    }
}
