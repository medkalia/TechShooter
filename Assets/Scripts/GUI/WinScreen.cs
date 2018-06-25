using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinScreen : MonoBehaviour {

    public Text winingText;

    public Text goldCountText;
    

    public void SetWiningText()
    {
        int wonGoldAmount = Convert.ToInt16(goldCountText.text) - DataController.Instance.GetProgressService().playerProgress.Gold;
        DataController.Instance.GetProgressService().UpdateGold(Convert.ToInt16(goldCountText.text));
        winingText.text = "<color> Well Done ! </color> \n" +
            SceneManager.GetActiveScene().name + " Finished \n"+
            "You've collected <color=#F2F600FF> " + wonGoldAmount.ToString() + " </color> Gold Piece";
        DataController.Instance.GetProgressService().UpdateLevel();
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        //TODO:TAKE IT OFF
        DataController.Instance.RefreshCardsData();
        if (!(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1))
            NotificationsManager.Instance.SaveGameFinish();
        SceneManager.LoadScene("Main Menu");
    }

    public void OnClickNextLevel()
    {
        Time.timeScale = 1;
        //TODO:TAKE IT OFF
        DataController.Instance.RefreshCardsData();

        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            AdMobManager.Instance.ShowInterstitial();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        else
        {
            NotificationsManager.Instance.SaveGameFinish();
            AdMobManager.Instance.mainMenuInterstitialTimer.ResetTimer(true);
            SceneManager.LoadScene("Main Menu");
            
        }
            
        //Scene nextScene = SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
        //if (nextScene.name.Contains("Level"))
        //    SceneManager.LoadScene(nextScene.name);
        //else
        //    SceneManager.LoadScene("Main Menu");
    }
}

