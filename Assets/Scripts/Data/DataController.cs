using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

    [HideInInspector]
    public static DataController Instance { get; set; }
    private CardService cardServiceInstance = new CardService();
    private ProgressService progressServiceInstance = new ProgressService();
    private SettingsService settingsServiceInstance = new SettingsService();

    void Start () {

        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        cardServiceInstance.BuildData();
        progressServiceInstance.BuildDataBase();
        settingsServiceInstance.MasterMixer = Resources.Load("Audio/MasterMixer") as AudioMixer;
        settingsServiceInstance.InitializeVolume();
        DontDestroyOnLoad(gameObject);
        if (!(SceneManager.GetActiveScene().name == "TestScene"))
            SceneManager.LoadScene("Main Menu");

    }

    public CardService GetCardService()
    {
        return cardServiceInstance;
    }
    public ProgressService GetProgressService()
    {
        return progressServiceInstance;
    }
    public SettingsService GetSettingsService()
    {
        return settingsServiceInstance;
    }

    //Fucking retarded shouldn't have to do this but for some reason the list of deck cards is losing elements when i load the main menu 
    public void RefreshCardsData()
    {
        cardServiceInstance.BuildData();
    }




    void Update () {
		
	}
}
