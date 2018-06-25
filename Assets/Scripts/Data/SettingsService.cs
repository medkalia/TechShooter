using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsService
{

    public AudioMixer MasterMixer { get; set; }
    public const string MASTERVOLUME = "Master volume";
    public const string SFXVOLUME = "sfx volume";
    public const string MUSICVOLUME = "music volume";

    public enum AudioMixerGroups
    {
        MasterVolume,
        MusicVolume,
        SFXVolume
    }

    //TODO: THESE METHODS ARE DEPRECATED REPLACE THEM WITH UpdateVolume(AudioMixerGroups audioMixerGroup, float value)
    public void UpdateVolume (float value)
    {
        PlayerPrefs.SetFloat(MASTERVOLUME, value);
        MasterMixer.SetFloat("MasterVolume", value);
    }

    public void UpdateMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MUSICVOLUME, value);
        MasterMixer.SetFloat("MusicVolume", value);
    }

    public void UpdateSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(SFXVOLUME, value);
        MasterMixer.SetFloat("SFXVolume", value);
    }
    //-----------------------------------------------------------------------------------------------------------------

    public void UpdateVolume(AudioMixerGroups audioMixerGroup, float value)
    {
        string audioMixerGroupToUpdate = Enum.GetName(typeof(AudioMixerGroups), audioMixerGroup);

        PlayerPrefs.SetFloat(audioMixerGroupToUpdate, value);
        MasterMixer.SetFloat(audioMixerGroupToUpdate, value);
    }

    public float GetVolume (AudioMixerGroups audioMixerGroup)
    {
        string audioMixerGroupName = Enum.GetName(typeof(AudioMixerGroups), audioMixerGroup);
        return Convert.ToSingle(PlayerPrefs.GetFloat(audioMixerGroupName, 1));
    }

    public void InitializeVolume()
    {
        foreach(var amg in EnumUtil.GetValues<AudioMixerGroups>())
        {
            string audioMixerGroupName = Enum.GetName(typeof(AudioMixerGroups), amg);
            if (!audioMixerGroupName.Equals("") && MasterMixer != null)
            {
                float volume = PlayerPrefs.GetFloat(Enum.GetName(typeof(AudioMixerGroups), amg));
                if (!MasterMixer.SetFloat(audioMixerGroupName, volume))
                {
                    Debug.LogError("No mixer group found with the name \"" + audioMixerGroupName + "\"");
                }
                else
                {
                    //Debug.Log("Setting the volume for " + audioMixerGroupName + " to " + volume);
                }
            }
            
        }
    }
}
