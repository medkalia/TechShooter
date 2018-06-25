using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour {

    public AudioParams.SoundPoolGroups level;

    AudioManager audioManager;


    void Start()
    {
        audioManager = AudioManager.instance;
        StartCoroutine(audioManager.Refresh(audioManager.gameObject, PlayMusic));
    }
        
	
    void PlayMusic()
    {
        audioManager.PlaySoundOverFlow(level, AudioParams.SoundPools.THEME, audioManager.gameObject);
    }
}
