using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBossMusic : MonoBehaviour {

    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        StartCoroutine(audioManager.Refresh(audioManager.gameObject, PlayMusic));
    }

    void PlayMusic()
    {
        audioManager.PlaySound(AudioParams.SoundPoolGroups.LEVELBossMUSIC, AudioParams.SoundPools.THEME, audioManager.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
