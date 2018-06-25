using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRamChargeBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.GetComponent<AudioSource>().volume = .3f;
        //SoundUtil.PlaySound(animator.gameObject.GetComponent<AudioSource>(), "Ennemies/RoboRam/RoboRamForward");
        animator.gameObject.GetComponent<Enemy>().StopSound(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.EXISTING);
        (animator.gameObject.GetComponent<Enemy>()).PlaySoundModified(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.MELEE, .5f);
    }

}
