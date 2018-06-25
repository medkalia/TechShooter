using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback: OnLogIn);
    }

    void OnLogIn(ILoginResult resul)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
        }
        else
            Debug.Log("login canceled");
    }

    public void Share()
    {
        FB.ShareLink(
            contentTitle: "Hope 1",
            contentURL: new System.Uri("http://www.hope1.com"),
            contentDescription: "playing Hope 1",
            callback: OnShare);
    }

    void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("sahreLing error : " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        Debug.Log("share succeed");
    }
}*/