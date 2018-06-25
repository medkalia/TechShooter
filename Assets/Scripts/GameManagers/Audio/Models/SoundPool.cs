using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class SoundPool : ScriptableObject {
    
    public AudioParams.SoundPools Tag;
    [Range(0f, 1f)]
    public float volume = 1f ;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
    public bool playOnAwake;

    [Space]
    [Range(0f, 1f)]
    public float spatialBlend = 1f;
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
    public float minDistance = 0.5f;
    public float maxDistance = 17f;
    [Space]
    public AudioParams.AudioMixerGroups audioMixerGroup;

    [Space]
    public List<Sound> sounds;


    private AudioMixer masterMixer;

    /// <summary>
    ///  Pick a sound from the pool and returns an Audio Source with the default values
    /// </summary>
    public AudioSource UpdateAudioSource(AudioSource audioSourceToUpdate)
    {
        int pickedNumber = 0;
        pickedNumber = UnityEngine.Random.Range(0, sounds.Count);
        AudioClip pickedSound = sounds[pickedNumber].clip;
        audioSourceToUpdate.clip = pickedSound;
        audioSourceToUpdate.volume = volume;
        audioSourceToUpdate.pitch = pitch;
        audioSourceToUpdate.loop = loop;
        audioSourceToUpdate.spatialBlend = spatialBlend;
        audioSourceToUpdate.rolloffMode = AudioRolloffMode.Linear;
        audioSourceToUpdate.minDistance = minDistance;
        audioSourceToUpdate.maxDistance = maxDistance;
        audioSourceToUpdate.playOnAwake = playOnAwake;
        if (audioMixerGroup != AudioParams.AudioMixerGroups.NONE)
            audioSourceToUpdate.outputAudioMixerGroup = masterMixer.FindMatchingGroups(AudioParams.GetAudioMixerGroupName(audioMixerGroup))[0];

        return audioSourceToUpdate;
    }

    private void OnEnable()
    {
        masterMixer = Resources.Load("Audio/MasterMixer") as AudioMixer;
    }
}
