using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BIJ_UI_Module_LoadScene : MonoBehaviour {

    #region Variables
    [Space]
    [Header("Loading")]
    public GameObject m_LoadingBar;
    public Image m_LoadingImage;
    public Text m_LoadingText;

    #endregion

    #region Main Methods
    public void LoadScene(string LoadingSceneName)
    {
        StartCoroutine(LoadNewScene(LoadingSceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        if (m_LoadingBar != null && m_LoadingImage != null && m_LoadingText != null)
        {
            m_LoadingBar.SetActive(true);
            while (!async.isDone)
            {
                float progress = Mathf.Clamp01(async.progress / 0.9f);
                string progressText = string.Format("{0:0.0}", progress * 100f) + "%";
                m_LoadingText.text = progressText;
                m_LoadingImage.fillAmount = progress;
                yield return null;
            }
        }else
        {
            Debug.LogError("Loading bar not properly assigned");
        }
            
        
    }
    #endregion

    #region Helper Methods
    #endregion
}
