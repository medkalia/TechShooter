﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {


    //**** Private variables
    private AudioSource audiosrc;
    private AudioClips audioClips ;


    void Start () {
        audioClips = new AudioClips();
        audiosrc = gameObject.GetComponent<AudioSource>();
        
    }
	
	
	void Update () {
        
    }
    //each animation will pick the type of sound to call and this methode will chose one randomly and play it
    public void playSound (string clip)
    {
        //audio types valume
        float footStepsVolume = 0.5f;
        float jumpVolume = 0.75f;
        float hurtingVolume = 1f;
        float dyingVolume = 0.5f;
        float robot01MeleeVolume = 0.25f;
        float robot01DestroyVolume = 0.25f;
        
        int pickedNumber;
        AudioClip pickedSound;
        switch (clip)
        {
            case "footSteps":
                pickedNumber = Random.Range(0,audioClips.footSteps.Count );
                pickedSound = Resources.Load<AudioClip>(audioClips.footSteps[pickedNumber]);
                audiosrc.volume = footStepsVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
            case "jump":
                pickedNumber = Random.Range(0, audioClips.jump.Count );
                pickedSound = Resources.Load<AudioClip>(audioClips.jump[pickedNumber]);
                audiosrc.volume = jumpVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
            case "hurting":
                pickedNumber = Random.Range(0, audioClips.hurting.Count );
                pickedSound = Resources.Load<AudioClip>(audioClips.hurting[pickedNumber]);
                audiosrc.volume = hurtingVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
            case "dying":
                pickedNumber = Random.Range(0, audioClips.dying.Count);
                pickedSound = Resources.Load<AudioClip>(audioClips.dying[pickedNumber]);
                audiosrc.volume = dyingVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
            case "robot01Melee":
                pickedNumber = Random.Range(0, audioClips.robot01Melee.Count);
                pickedSound = Resources.Load<AudioClip>(audioClips.robot01Melee[pickedNumber]);
                audiosrc.volume = robot01MeleeVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
            case "robot01Destroy":
                pickedNumber = Random.Range(0, audioClips.robot01Destroy.Count);
                pickedSound = Resources.Load<AudioClip>(audioClips.robot01Destroy[pickedNumber]);
                audiosrc.volume = robot01DestroyVolume;
                audiosrc.PlayOneShot(pickedSound);
                break;
        }
    }

    private class AudioClips
    {
        //clips paths
        //Player
            //foot steps
        public List<string> footSteps = new List<string>();
        string pathFootStep1 = "Audio/Player/Walking/footstepGround01";
        string pathFootStep2 = "Audio/Player/Walking/footstepGround02";
        string pathFootStep3 = "Audio/Player/Walking/footstepGround04";
            //jumping
        public List<string> jump = new List<string>();
        string jumping1 = "Audio/Player/Jumping/Jump01";
        string jumping2 = "Audio/Player/Jumping/Jump02";
            //hurting
        public List<string> hurting = new List<string>();
        string hurting1 = "Audio/Player/Hurting/Pain01";
        string hurting2 = "Audio/Player/Hurting/Pain03";
        string hurting3 = "Audio/Player/Hurting/Pain04";
        string hurting4 = "Audio/Player/Hurting/Pain05";
        string hurting5 = "Audio/Player/Hurting/Pain07";
        string hurting6 = "Audio/Player/Hurting/Pain08";
            //dying
        public List<string> dying = new List<string>();
        string dying01 = "Audio/Player/Dying/dying01";
        //Ennemies
        //Robot01
            //Melee
        public List<string> robot01Melee = new List<string>();
        string robot01Melee1 = "Audio/Ennemies/Robot01/Melee";
            //Destroy
        public List<string> robot01Destroy = new List<string>();
        string robot01Destroy1 = "Audio/Ennemies/Robot01/Destroy";

        public AudioClips()
        {
            //Player
                //foot steps
            footSteps.Add(pathFootStep1);
            footSteps.Add(pathFootStep2);
            footSteps.Add(pathFootStep3);
                //jumping
            jump.Add(jumping1);
            jump.Add(jumping2);
                //hurting
            hurting.Add(hurting1);
            hurting.Add(hurting2);
            hurting.Add(hurting3);
            hurting.Add(hurting4);
            hurting.Add(hurting5);
            hurting.Add(hurting6);
                //dying
            dying.Add(dying01);
            //Ennemies
            //Robot01
            //Melee
            robot01Melee.Add(robot01Melee1);
            //Destroy
            robot01Destroy.Add(robot01Destroy1);
        }
    }
}
