using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamDechargeBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Enemy>().UpdateSound(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.MELEE, .5f);

    }

}
