using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RoBear roBear = animator.gameObject.GetComponent<RoBear>();
        roBear.ChangeState(new RoBearIdleState());
        roBear.PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.TALKING);
    }
}
