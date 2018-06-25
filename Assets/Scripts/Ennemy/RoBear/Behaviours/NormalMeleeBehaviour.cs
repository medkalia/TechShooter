using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeleeBehaviour : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //RoBear roBear = animator.gameObject.GetComponent<RoBear>();
        //roBear.enemyInfo.isMeleeAttacking = false;
        //decided to launch the melee shot at the same time with the animation
        //RoBear roBear = animator.gameObject.GetComponent<RoBear>();
        //roBear.ReleaseShot();
    }
}
