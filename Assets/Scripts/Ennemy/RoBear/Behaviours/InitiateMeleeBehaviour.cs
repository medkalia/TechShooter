using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateMeleeBehaviour : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RoBear roBear = animator.gameObject.GetComponent<RoBear>();
        roBear.enemyInfo.isMeleeAttacking = true;
        roBear.stateInfo.startedMeleeAttacking = true;
    }

}
