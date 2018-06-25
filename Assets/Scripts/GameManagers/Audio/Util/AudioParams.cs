using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioParams
{



    public enum SoundPools
    {
        HURTING,
        FALLING,
        DUYING,
        RUNNING,
        EXPLOSION,
        JUMPING,
        WALKING,
        MELEE,
        RANGED,
        EXISTING,
        SCREAMING,
        SUMMONING,
        FATIGUE,
        TALKING,
        SPAWNING,
        MELEEEFFECT,
        THEME,
        DUCKING,
    }

    //public static string GetSoundPoolGroupName(SoundPools value)
    //{
    //    string SoundPoolName = "";
    //    switch (value)
    //    {
    //        case SoundPools.HURTING:
    //            SoundPoolName = "Hurting";
    //            break;
    //        case SoundPools.FALLING:
    //            SoundPoolName = "Falling";
    //            break;
    //        case SoundPools.DUYING:
    //            SoundPoolName = "Duying";
    //            break;
    //        case SoundPools.RUNNING:
    //            SoundPoolName = "Runing";
    //            break;
    //        case SoundPools.EXPLOSION:
    //            SoundPoolName = "Explosion";
    //            break;
    //        default:
    //            break;
    //    }
    //    return SoundPoolName;
    //}

    //*******************************************************************SoundPoolGroups

    public enum SoundPoolGroups
    {
        PLAYER,
        ROBOT01,
        ROBOTURTLE,
        ROBORAM,
        ROBEAR,
        COIN,
        MUSIC,
        MAINMENU,
        LEVEL01MUSIC,
        LEVEL02MUSIC,
        LEVELBossMUSIC,
    }

    //public static string GetSoundPoolGroupName(SoundPoolGroups value)
    //{
    //    string SoundPoolGroupName = "";
    //    switch (value)
    //    {
    //        case SoundPoolGroups.PLAYER:
    //            SoundPoolGroupName = "Player";
    //            break;
    //        case SoundPoolGroups.ROBEAR:
    //            SoundPoolGroupName = "RoBear";
    //            break;
    //        case SoundPoolGroups.ROBORAM:
    //            SoundPoolGroupName = "RoboRam";
    //            break;
    //        case SoundPoolGroups.ROBOT01:
    //            SoundPoolGroupName = "Robot01";
    //            break;
    //        case SoundPoolGroups.ROBOTURTLE:
    //            SoundPoolGroupName = "RoboTurtle";
    //            break;
    //        default:
    //            break;
    //    }
    //    return SoundPoolGroupName;
    //}

    //*******************************************************************AudioMixerGroups

    public enum AudioMixerGroups
    {
        NONE,
        MUSIC,
        SFX,
        PATROL,
    }

    public static string GetAudioMixerGroupName(AudioMixerGroups value)
    {
        string audioMixerGroupName = "";
        switch (value)
        {
            case AudioMixerGroups.MUSIC:
                audioMixerGroupName = "Music";
                break;
            case AudioMixerGroups.SFX:
                audioMixerGroupName = "SFX";
                break;
            case AudioMixerGroups.PATROL:
                audioMixerGroupName = "Patrol";
                break;
            default:
                break;
        }
        return audioMixerGroupName;
    }
}
