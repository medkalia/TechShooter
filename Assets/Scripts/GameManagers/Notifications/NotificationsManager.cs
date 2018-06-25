using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationsManager : MonoBehaviour {

    [HideInInspector] public static NotificationsManager Instance { get; set; }
    [HideInInspector] public enum NotificationType{Tutorial, Finish}

    private TutorialPanel tutorialPanel;
    private GameFinishPanel gameFinishPanel;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMainMenuNotifications(scene.name);
    }

    void Start () {
        
        
    }
	
	void Update () {
        

    }

    private void HandleMainMenuNotifications(string sceneName)
    {
        if (sceneName == "Main Menu")
        {
            int hasSeenTutorial = PlayerPrefs.GetInt(EnumUtil.GetString(NotificationType.Tutorial), 0);
            if (hasSeenTutorial == 0)
            {
                GameObject go = GameObject.FindGameObjectWithTag("UI_GRP");
                if (go != null) tutorialPanel = go.GetComponent<TutorialPanel>();
                else Debug.LogError("No UI_GRP found");
                if (tutorialPanel != null)
                {
                    tutorialPanel.StartTutorial();
                }
            }

            int hasFinishedGame = PlayerPrefs.GetInt(EnumUtil.GetString(NotificationType.Finish), 0);
            if (hasFinishedGame == 1)
            {
                GameObject go = GameObject.FindGameObjectWithTag("UI_GRP");
                if (go != null) gameFinishPanel = go.GetComponent<GameFinishPanel>();
                else Debug.LogError("No UI_GRP found");
                if (gameFinishPanel != null)
                {
                    gameFinishPanel.StartNotification();
                    PlayerPrefs.SetInt(EnumUtil.GetString(NotificationType.Finish), 2);
                }
            }
        }
    }

    public void SaveTutorialFinish()
    {
        PlayerPrefs.SetInt(EnumUtil.GetString(NotificationType.Tutorial), 1);
    }

    public void SaveGameFinish()
    {
        if (PlayerPrefs.GetInt(EnumUtil.GetString(NotificationType.Finish), 0) == 0)
        {
            PlayerPrefs.SetInt(EnumUtil.GetString(NotificationType.Finish), 1);
        }
            
    }



}
