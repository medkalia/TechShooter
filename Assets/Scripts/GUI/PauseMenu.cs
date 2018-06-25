using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = Convert.ToSingle(PlayerPrefs.GetFloat(SettingsService.MASTERVOLUME, 1));
    }

    public void OnClickResume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnVolumeSlide(float value)
    {
        DataController.Instance.GetSettingsService().UpdateVolume(value);
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        //TODO:TAKE IT OFF
        DataController.Instance.RefreshCardsData();
        SceneManager.LoadScene("Main Menu");
    }
}
