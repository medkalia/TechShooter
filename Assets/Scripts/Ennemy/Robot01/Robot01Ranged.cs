using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot01Ranged : MonoBehaviour {

    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySound(AudioParams.SoundPoolGroups.ROBOT01, AudioParams.SoundPools.RANGED, gameObject);
    }
}
