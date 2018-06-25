using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class GameFinishPanel : MonoBehaviour {

    public GameObject gameFinishPanel;
    public Text okButtonText;
    public string[] okButtonTextArray; 

    [HideInInspector] public int parameterStepHash = Animator.StringToHash("step");

    private int notificationProgress = 0;
    private Animator gameFinishPanelAnimator;


    void Start () {
        if (gameFinishPanel != null)
        {
            gameFinishPanelAnimator = gameFinishPanel.GetComponent<Animator>();
        }
            
        else
            Debug.LogError("No Game finish panel found! fix it !");
    }
	
	public void StartNotification()
    {
        gameFinishPanel.SetActive(true);
        okButtonText.text = okButtonTextArray[0];
    }

    public void NextStepNotification()
    {
        gameFinishPanelAnimator.SetInteger(parameterStepHash, ++notificationProgress);
        if (notificationProgress < okButtonTextArray.Length)
            okButtonText.text = okButtonTextArray[notificationProgress];
    }

    public void FinishNotification()
    {
        gameFinishPanel.SetActive(false);
        NotificationsManager.Instance.SaveTutorialFinish();
    }

}
