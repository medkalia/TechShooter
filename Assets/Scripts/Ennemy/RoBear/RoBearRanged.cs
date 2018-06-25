using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBearRanged : MonoBehaviour {

    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.RANGED, gameObject);
    }
}
