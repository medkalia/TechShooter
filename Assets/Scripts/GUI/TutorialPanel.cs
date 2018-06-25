using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TutorialPanel : MonoBehaviour {

    public GameObject tutorialPanel;
    public Text okButtonText;
    public string[] okButtonTextArray; 

    [HideInInspector] public int parameterStepHash = Animator.StringToHash("step");

    private int tutorialProgress = 0;
    private Animator tutorialPanelAnimator;


    void Start () {
        if (tutorialPanel != null)
        {
            tutorialPanelAnimator = tutorialPanel.GetComponent<Animator>();
        }
            
        else
            Debug.LogError("No Tutorial panel found! fix it !");
    }
	
	public void StartTutorial()
    {
        tutorialPanel.SetActive(true);
        okButtonText.text = okButtonTextArray[0];
    }

    public void NextStepTutorial()
    {
        tutorialPanelAnimator.SetInteger(parameterStepHash, ++tutorialProgress);
        if (tutorialProgress < okButtonTextArray.Length)
            okButtonText.text = okButtonTextArray[tutorialProgress];
    }

    public void FinishTutorial()
    {
        tutorialPanel.SetActive(false);
        NotificationsManager.Instance.SaveTutorialFinish();
    }

}
