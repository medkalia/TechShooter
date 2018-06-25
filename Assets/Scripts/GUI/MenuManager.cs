using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Animator initiallyOpen;
    
    [Space]
    [Header("Loading")]
    public GameObject LoadingBar;
    public GameObject LoadingGraphics;
    public Image loadingImage;
    public Text loadingText;

    private int m_OpenParameterId;
    private Animator m_Open;
    private GameObject m_PreviouslySelected;

    const string k_OpenTransitionName = "Open";
    const string k_ClosedStateName = "Closed";

    
    public void OnEnable()
    {
        m_OpenParameterId = Animator.StringToHash(k_OpenTransitionName);

        if (initiallyOpen == null)
            return;

        OpenPanel(initiallyOpen);
    }

    public void OpenPanel(Animator anim)
    {
        if (m_Open == anim)
            return;

        anim.gameObject.SetActive(true);
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        m_Open = anim;
        m_Open.SetBool(m_OpenParameterId, true);

        GameObject go = FindFirstEnabledSelectable(anim.gameObject);

        SetSelected(go);
    }

    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    public void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParameterId, false);
        SetSelected(m_PreviouslySelected);
        StartCoroutine(DisablePanelDeleyed(m_Open));
        m_Open = null;
    }

    IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }

    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }

    public void LoadScene(string LoadingSceneName)
    {
        //loadingImage.SetActive(true);
        //Application.LoadLevel(level);
        //SceneManager.LoadScene(level);
        StartCoroutine(LoadNewScene(LoadingSceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        LoadingBar.SetActive(true);
        LoadingGraphics.SetActive(true);
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            //sliderBar.value = progress;
            //Debug.Log( progress * 100f + "%" ); 
            string progressText = string.Format("{0:0.0}", progress * 100f) + "%";
            loadingText.text = progressText;
            loadingImage.fillAmount = progress;
            yield return null;
        }
    }

    public void UpdateVolume(float value)
    {
        DataController.Instance.GetSettingsService().UpdateVolume(value);
    }

    public void UpdateSFX(float value)
    {
        DataController.Instance.GetSettingsService().UpdateSFXVolume(value);
    }

    public void UpdateMusic(float value)
    {
        DataController.Instance.GetSettingsService().UpdateMusicVolume(value);
    }
}
