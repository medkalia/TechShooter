using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour {

    public Text deathText;

    public Text goldCountText;
    

    public void SetLoosingText()
    {
        //Debug.Log("TEXT GOLD : " + Convert.ToInt16(goldCountText.text) + "\n Progress Gold : " + DataController.Instance.GetProgressService().playerProgress.Gold);
        int lostGoldAmount = Convert.ToInt16(goldCountText.text) - DataController.Instance.GetProgressService().playerProgress.Gold;
        deathText.text = "<color> Too Bad you're dead </color> \n" +
            SceneManager.GetActiveScene().name + " Falied \n" +
            "You've lost <color=red> " + lostGoldAmount.ToString() + " </color> Gold Piece";
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        //TODO:TAKE IT OFF
        DataController.Instance.RefreshCardsData();
        SceneManager.LoadScene("Main Menu");
    }

    public void OnClickRespawn()
    {
        AdMobManager.Instance.HandleDeathCountInterstitial();
        Time.timeScale = 1;
        //TODO:TAKE IT OFF
        DataController.Instance.RefreshCardsData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

