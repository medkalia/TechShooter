using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRestricter : MonoBehaviour {

    public Button[] levelButtons;

    private void Start()
    {
        levelButtons = GetComponentsInChildren<Button>();

        foreach (Button levelButton in levelButtons)
        {
            if ((levelButton.gameObject.transform.GetSiblingIndex() < (levelButtons.Length - 1)) &&  
                DataController.Instance.GetProgressService().playerProgress.LastLevel < levelButton.gameObject.transform.GetSiblingIndex())
            {
                levelButton.interactable = false;
            }
        }
    }
}
