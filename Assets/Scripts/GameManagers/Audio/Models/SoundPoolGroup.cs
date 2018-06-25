using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SoundPoolGroup : ScriptableObject {

    public AudioParams.SoundPoolGroups Tag;
    [Space]
    public List<SoundPool> soundPools;


/// <summary>
///  Returns a sound pool with the defined tag
/// </summary>
    public SoundPool GetSoundPool(AudioParams.SoundPools soundPoolTag)
    {
        SoundPool foundSoundPool = soundPools.Find(currentSoundPool => currentSoundPool.Tag == soundPoolTag);
        if (foundSoundPool == null)
        {
            Debug.LogWarning("Sound Pool : " + soundPoolTag + " was not found !");
        }
        return foundSoundPool;
    }
}
