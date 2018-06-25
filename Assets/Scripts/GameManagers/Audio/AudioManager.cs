using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;


public class AudioManager : MonoBehaviour {

    public List<SoundPoolGroup> soundPoolGroups;

    public static AudioManager instance;

    private Dictionary<int, CreatedAudioSources> createdAudioSourcesDictionnary;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        createdAudioSourcesDictionnary = new Dictionary<int, CreatedAudioSources>();
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
    }

    /// <summary>
    ///  Play sound on the game object 
    /// </summary>
    public void PlaySound(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller)
    {
        int audioSourceKey = (soundPoolGroupTag.ToString() + soundPoolTag.ToString() + caller.GetInstanceID().ToString()).GetHashCode();
        if (!createdAudioSourcesDictionnary.ContainsKey(audioSourceKey))
        {
            //Debug.Log("FIRST CALL CREATING THE DICT !! key = " + audioSourceKey);
            SoundPoolGroup foundSoundPoolGroup = soundPoolGroups.FirstOrDefault(currentSoundPoolGroup => currentSoundPoolGroup.Tag == soundPoolGroupTag);
            if (foundSoundPoolGroup == null)
            {
                //Debug.LogWarning("Sound Pool Group : " + soundPoolGroupTag.ToString() + " was not found !");
                return;
            }
            else
            {
                SoundPool foundSoundPool = foundSoundPoolGroup.GetSoundPool(soundPoolTag);
                if (foundSoundPool != null)
                {

                    AudioSource newAudioSource = caller.AddComponent<AudioSource>();
                    newAudioSource = foundSoundPool.UpdateAudioSource(newAudioSource);
                    if (newAudioSource != null)
                    {
                        newAudioSource.Play();
                        createdAudioSourcesDictionnary.Add(audioSourceKey, new CreatedAudioSources(foundSoundPool,newAudioSource));
                    }
                }
                
                     
            }
        }
        //here is the case where we already made this search but the audio source got destroyed between the privious call and this one
        else if (createdAudioSourcesDictionnary[audioSourceKey].audioSource == null) 
        {
            //Debug.Log("RECREATING THE AUDIO SOURCE");
            AudioSource recreatedAudioSource = caller.AddComponent<AudioSource>();
            recreatedAudioSource=  createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(recreatedAudioSource);

            createdAudioSourcesDictionnary[audioSourceKey].audioSource = recreatedAudioSource;
            createdAudioSourcesDictionnary[audioSourceKey].audioSource.Play();
        }
        //here is the case where we already made tgis search and the audio source is still there
        else
        {
            //Debug.Log("ALREADY THERE");
            createdAudioSourcesDictionnary[audioSourceKey].audioSource = 
                createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(createdAudioSourcesDictionnary[audioSourceKey].audioSource);
            createdAudioSourcesDictionnary[audioSourceKey].audioSource.Play();
        }
    }

    /// <summary>
    ///  Play sound on the game object without interruption if already playing
    /// </summary>
    public void PlaySoundOverFlow(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller)
    {
        int audioSourceKey = (soundPoolGroupTag.ToString() + soundPoolTag.ToString() + caller.GetInstanceID().ToString()).GetHashCode();
        if (!createdAudioSourcesDictionnary.ContainsKey(audioSourceKey))
        {
            //Debug.Log("FIRST CALL CREATING THE DICT !! key = " + audioSourceKey);
            SoundPoolGroup foundSoundPoolGroup = soundPoolGroups.FirstOrDefault(currentSoundPoolGroup => currentSoundPoolGroup.Tag == soundPoolGroupTag);
            if (foundSoundPoolGroup == null)
            {
                //Debug.LogWarning("Sound Pool Group : " + soundPoolGroupTag.ToString() + " was not found !");
                return;
            }
            else
            {
                SoundPool foundSoundPool = foundSoundPoolGroup.GetSoundPool(soundPoolTag);
                if (foundSoundPool != null)
                {

                    AudioSource newAudioSource = caller.AddComponent<AudioSource>();
                    newAudioSource = foundSoundPool.UpdateAudioSource(newAudioSource);
                    if (newAudioSource != null)
                    {
                        newAudioSource.Play();
                        createdAudioSourcesDictionnary.Add(audioSourceKey, new CreatedAudioSources(foundSoundPool, newAudioSource));
                    }
                }


            }
        }
        //here is the case where we already made this search but the audio source got destroyed between the privious call and this one
        else if (createdAudioSourcesDictionnary[audioSourceKey].audioSource == null)
        {
            //Debug.Log("RECREATING THE AUDIO SOURCE");
            AudioSource recreatedAudioSource = caller.AddComponent<AudioSource>();
            recreatedAudioSource = createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(recreatedAudioSource);

            createdAudioSourcesDictionnary[audioSourceKey].audioSource = recreatedAudioSource;
            createdAudioSourcesDictionnary[audioSourceKey].audioSource.Play();
        }
        //here is the case where we already made tgis search and the audio source is still there
        else
        {
            //Debug.Log("ALREADY THERE");
            createdAudioSourcesDictionnary[audioSourceKey].audioSource =
                createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(createdAudioSourcesDictionnary[audioSourceKey].audioSource);

            if (!createdAudioSourcesDictionnary[audioSourceKey].audioSource.isPlaying)
                createdAudioSourcesDictionnary[audioSourceKey].audioSource.Play();
        }
    }

    /// <summary>
    ///  Play sound on the game object with modified volume  
    /// </summary>
    public void PlaySoundModified(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller, float volumeEffector)
    {
        int audioSourceKey = (soundPoolGroupTag.ToString() + soundPoolTag.ToString() + caller.GetInstanceID().ToString()).GetHashCode();
        if (!createdAudioSourcesDictionnary.ContainsKey(audioSourceKey))
        {
            SoundPoolGroup foundSoundPoolGroup = soundPoolGroups.FirstOrDefault(currentSoundPoolGroup => currentSoundPoolGroup.Tag == soundPoolGroupTag);
            if (foundSoundPoolGroup == null)
            {
                return;
            }
            else
            {
                SoundPool foundSoundPool = foundSoundPoolGroup.GetSoundPool(soundPoolTag);
                if (foundSoundPool != null)
                {

                    AudioSource newAudioSource = caller.AddComponent<AudioSource>();
                    newAudioSource = foundSoundPool.UpdateAudioSource(newAudioSource);
                    if (newAudioSource != null)
                    {
                        createdAudioSourcesDictionnary.Add(audioSourceKey, new CreatedAudioSources(foundSoundPool, newAudioSource));
                        AudioSource modifiedAudioSource = newAudioSource;
                        modifiedAudioSource .volume *= volumeEffector;
                        modifiedAudioSource.Play();
                    }
                }
            }
        }
        //here is the case where we already made this search but the audio source got destroyed between the privious call and this one
        else if (createdAudioSourcesDictionnary[audioSourceKey].audioSource == null)
        {
            AudioSource recreatedAudioSource = caller.AddComponent<AudioSource>();
            recreatedAudioSource = createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(recreatedAudioSource);

            createdAudioSourcesDictionnary[audioSourceKey].audioSource = recreatedAudioSource;
            AudioSource modifiedAudioSource = recreatedAudioSource;
            modifiedAudioSource.volume *= volumeEffector;
            modifiedAudioSource.Play();
        }
        //here is the case where we already made tgis search and the audio source is still there
        else
        {
            createdAudioSourcesDictionnary[audioSourceKey].audioSource =
                createdAudioSourcesDictionnary[audioSourceKey].soundPool
                .UpdateAudioSource(createdAudioSourcesDictionnary[audioSourceKey].audioSource);

            AudioSource modifiedAudioSource = createdAudioSourcesDictionnary[audioSourceKey].audioSource;
            modifiedAudioSource.volume *= volumeEffector;
            modifiedAudioSource.Play();
        }
    }

    /// <summary>
    ///  Update the sound on the specified audio source
    /// </summary>
    public void UpdateSound(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller, float volumeEffector)
    {
        int audioSourceKey = (soundPoolGroupTag.ToString() + soundPoolTag.ToString() + caller.GetInstanceID().ToString()).GetHashCode();
        if (createdAudioSourcesDictionnary.ContainsKey(audioSourceKey))
        {
            AudioSource modifiedAudioSource = createdAudioSourcesDictionnary[audioSourceKey].audioSource;
            modifiedAudioSource.volume *= volumeEffector;
        }
    }

    /// <summary>
    ///  Stop playing a sound on the specified audio source
    /// </summary>
    public void StopSound(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller)
    {
        int audioSourceKey = (soundPoolGroupTag.ToString() + soundPoolTag.ToString() + caller.GetInstanceID().ToString()).GetHashCode();
        if (createdAudioSourcesDictionnary.ContainsKey(audioSourceKey))
        {
            Destroy(createdAudioSourcesDictionnary[audioSourceKey].audioSource);
            createdAudioSourcesDictionnary.Remove(audioSourceKey);
        }
    }

    /// <summary>
    ///  Play sound on the game object once then destroy audio source
    /// </summary>
    public void PlaySoundOnce(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller)
    {
        StartCoroutine(PlaySoundOnceCoroutine(soundPoolGroupTag, soundPoolTag, caller));
    }
    private IEnumerator PlaySoundOnceCoroutine(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller)
    {
        SoundPoolGroup foundSoundPoolGroup = soundPoolGroups.FirstOrDefault(currentSoundPoolGroup => currentSoundPoolGroup.Tag == soundPoolGroupTag);
        if (foundSoundPoolGroup == null)
        {
            yield return null;
        }
        else
        {
            SoundPool foundSoundPool = foundSoundPoolGroup.GetSoundPool(soundPoolTag);
            if (foundSoundPool != null)
            {
                AudioSource newAudioSource = caller.AddComponent<AudioSource>();
                newAudioSource = foundSoundPool.UpdateAudioSource(newAudioSource);
                if (newAudioSource != null)
                {
                    newAudioSource.PlayOneShot(newAudioSource.clip);
                    yield return new WaitWhile(() => newAudioSource != null && newAudioSource.isPlaying);
                    Destroy(newAudioSource);
                }
            }
        }
    }

    /// <summary>
    ///  Play sound if the specified animation is on and returns true if the sound has been played
    /// </summary>
    public bool PlaySoundWithAnimation(AudioParams.SoundPoolGroups soundPoolGroupTag, AudioParams.SoundPools soundPoolTag, GameObject caller, string animationName, bool test)
    {
        Animator animator = caller.GetComponent<Animator>();
        if (!test && animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            PlaySound(soundPoolGroupTag, soundPoolTag, caller);
            test = true;
        }
        return test;
    }

    /// <summary>
    ///  Clean the audio manager from all existing audio sources 
    /// </summary>
    public IEnumerator Refresh(GameObject caller, Action onFinish)
    {
        //Debug.Log("refreshing the " + caller.name + "'s audio sources");
        List<AudioSource> AudioManagerAudioSources = caller.GetComponents<AudioSource>().ToList();
        foreach (AudioSource currentAudioSource in AudioManagerAudioSources)
        {
            Destroy(currentAudioSource);
        }
        yield return null;
        onFinish();
    }

    //this is used to avoid creating an audio source and  to avoid looking for the soundpool each time 
    public class CreatedAudioSources
    {
        public SoundPool soundPool = null;
        public AudioSource audioSource = null;
        

        public CreatedAudioSources(SoundPool soundPool, AudioSource audioSource)
        {
            this.soundPool = soundPool;
            this.audioSource = audioSource;
        }
    }
}


