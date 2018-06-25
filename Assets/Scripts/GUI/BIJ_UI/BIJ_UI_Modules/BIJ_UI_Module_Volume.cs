using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BIJ_UI_Module_Volume : MonoBehaviour {

    #region Variables
    [Header("Volume")]
    [Tooltip("Insert each slider with its appropriate audio mixer group")]
    public SliderAudioMixerGroupDictionary volumeSliders;
    #endregion

    #region Main Methods
    public void Start()
    {
        foreach(Slider volumeSlider in volumeSliders.dictionary.Keys)
        {
            volumeSlider.value = DataController.Instance.GetSettingsService().GetVolume(volumeSliders.dictionary[volumeSlider]);
        }
    }

    public void OnVolumeSlide(float value)
    {
        if (volumeSliders != null)
        {
            //this is used cause on start when the sliders values are updated the event is actually triggered
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<Slider>() != null)
            {
                if (volumeSliders.dictionary.ContainsKey(EventSystem.current.currentSelectedGameObject.GetComponent<Slider>()))
                {
                    if (DataController.Instance)
                    {
                        DataController.Instance.GetSettingsService().UpdateVolume(
                            volumeSliders.dictionary[EventSystem.current.currentSelectedGameObject.GetComponent<Slider>()], value);
                    }
                    else
                    {
                        Debug.LogError("Data Controller not found to be able to update the volume");
                    }
                }
                else
                {
                    Debug.LogError("Slider not configured (check the screen's BIJ_UI_Module_Volume dictionnary)");
                }
            }
        }
        else
        {
            Debug.LogError("Sliders dictionnary empty");
        }
    }
    #endregion

    #region Helper Methods
    #endregion
}

#region Helper Classes

// ---------------
//  SettingsService.AudioMixerGroups => Slider
// ---------------
[Serializable]
public class SliderAudioMixerGroupDictionary : SerializableDictionary<Slider, SettingsService.AudioMixerGroups> { }

//---------------
// SettingsService.AudioMixerGroups => Slider
//---------------
#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(SliderAudioMixerGroupDictionary))]

public class SliderAudioMixerGroupDrawer : SerializableDictionaryDrawer<Slider, SettingsService.AudioMixerGroups>
{
    protected override SerializableKeyValueTemplate<Slider, SettingsService.AudioMixerGroups> GetTemplate()
    {
        return GetGenericTemplate<SerializableSliderAudioMixerGroupTemplate>();
    }
}
internal class SerializableSliderAudioMixerGroupTemplate : SerializableKeyValueTemplate<Slider, SettingsService.AudioMixerGroups> { }
#endif
#endregion
