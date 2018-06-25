using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamIdleBehaviour : StateMachineBehaviour {

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Enemy>().StopSound(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.MELEE);
        animator.gameObject.GetComponent<Enemy>().PlaySound(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.EXISTING);
    }

}
