using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundUtil  {

    /// <summary>
    /// Plays Sound with the soundName in the audiosrc 
    /// </summary>
    public static void PlaySound(AudioSource audiosrc, string soundName)
    {
        AudioClip pickedSound;
        pickedSound = Resources.Load<AudioClip>("Audio/"+soundName);
        audiosrc.clip = pickedSound;
        audiosrc.Play();
    }
    /*------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Plays Sound once with the soundName in the audiosrc 
    /// </summary>
    public static void PlaySoundOnce(AudioSource audiosrc, string soundName)
    {
        AudioClip pickedSound;
        pickedSound = Resources.Load<AudioClip>("Audio/" + soundName);
        audiosrc.PlayOneShot(pickedSound);
    }
    /*------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Plays Sound with the soundName in a new created audioSource then destroy it
    /// </summary>
    public static void PlaySoundOnTop(GameObject gameObject, string soundName)
    {
        MonoBehaviour mono = gameObject.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(PlaySoundOnTopCoroutine(gameObject, soundName));
    }
    private static IEnumerator PlaySoundOnTopCoroutine(GameObject gameObject, string soundName)
    {
        AudioSource tempAudioSrc = gameObject.gameObject.AddComponent<AudioSource>();
        PlaySoundOnce(tempAudioSrc, soundName);
        yield return new WaitWhile(() => tempAudioSrc.isPlaying);
        UnityEngine.Object.Destroy(tempAudioSrc);
    }
    /*------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Plays Sound with the soundName during the animation with the animaitonName returns true if sound was played (used to play sound once in a looping state for exemple)
    /// </summary>
    public static bool PlaySoundWithAnimation(GameObject gameObject, string animationName, string soundName, bool test)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (!test && animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            PlaySoundOnTop(gameObject, soundName);
            test = true;
        }
        return test;          
    }
    /// <summary>
    /// Plays Sound with the soundName during the animation with the animaitonName 
    /// </summary>
    public static void PlaySoundWithAnimation(GameObject gameObject, string animationName, string soundName)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            PlaySoundOnTop(gameObject, soundName);
        }
    }
    /*------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Plays Sound with the soundName when audiosrc finished playing
    /// </summary>
    public static void PlaySoundWhenReady(AudioSource audiosrc, string soundName)
    {
        MonoBehaviour mono = audiosrc.gameObject.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(PlaySoundWhenReadyCoroutine(audiosrc, soundName));
    }
    
    private static IEnumerator PlaySoundWhenReadyCoroutine(AudioSource audiosrc, string soundName)
    {
        yield return new WaitWhile(() => audiosrc.isPlaying);
        PlaySound(audiosrc, soundName);
    }

    /*------------------------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// Picks Sound with the type when audiosrc finished playing
    /// </summary>
    public static void PickSoundWhenReady(AudioSource audiosrc, string type, Func<string, string> soundPicker)
    {
        MonoBehaviour mono = audiosrc.gameObject.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(PickSoundWhenReadyCoroutine(audiosrc, type, soundPicker));
    }

    private static IEnumerator PickSoundWhenReadyCoroutine(AudioSource audiosrc, string type, Func<string, string> soundPicker)
    {
        yield return new WaitWhile(() => audiosrc.isPlaying);
        PlaySound(audiosrc,soundPicker(type));
    }
    



}
