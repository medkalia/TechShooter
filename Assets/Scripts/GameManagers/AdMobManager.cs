using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour {

    #region Variables
    [Header("Banner")]
    public string bannerId = "ca-app-pub-1283858062800159/4412846369";
    public string bannerId_IOS = "ca-app-pub-1283858062800159/6175988169";
    public float deckCD = 15f;
    [Space]
    [Header("Video")]
    public string interstitialId = "ca-app-pub-1283858062800159/6244802780";
    public string interstitialId_IOS = "ca-app-pub-1283858062800159/2376343956";
    [Tooltip("Time Between Main Menu Interstitial in minutes ")]
    public float mainMenuCD = 15f;
    [Tooltip("After how many deaths to show an Interstitial")]
    public int DeathCount = 5;

    [HideInInspector] public static AdMobManager Instance { get; set; }
    [HideInInspector] public Timer mainMenuInterstitialTimer;
    [HideInInspector] public Timer deckBannerTimer;

    private string testBannerAdUnit = "ca-app-pub-3940256099942544/6300978111";
    private string testVideoAdUnit = "ca-app-pub-3940256099942544/1033173712";
    private InterstitialAd interstitial;
    private BannerView bannerView;
    private int playerDeathCount = 0;


    #endregion

    #region Main Methods
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

    private void Start()
    {
        mainMenuInterstitialTimer = gameObject.AddComponent<Timer>();
        deckBannerTimer = gameObject.AddComponent<Timer>();
        deckBannerTimer.m_Timer = (int) Mathf.Floor(MathfUtil.MinutesToSeconds(deckCD+ 1));
        if (Application.platform == RuntimePlatform.Android)
            bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.TopRight);
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            bannerView = new BannerView(bannerId_IOS, AdSize.Banner, AdPosition.TopRight);
        LoadInterstitial();
    }
    #endregion

    #region Ads Methods
    public void ShowBanner(AdPosition position)
    {
        bannerView.SetPosition(position);
        //// Called when an ad request has successfully loaded.
        //bannerView.OnAdLoaded += HandleOnAdLoaded;
        //// Called when an ad request failed to load.
        //bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //// Called when an ad is clicked.
        //bannerView.OnAdOpening += HandleOnAdOpened;
        //// Called when the user returned from the app after an ad click.
        //bannerView.OnAdClosed += HandleOnAdClosed;
        //// Called when the ad click caused the user to leave the application.
        //bannerView.OnAdLeavingApplication += HandleOnAdLeftApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
    }
    public void HideBanner()
    {
       bannerView.Hide();
    }

    public void LoadInterstitial()
    {
        if (Application.platform == RuntimePlatform.Android)
            interstitial = new InterstitialAd(interstitialId);
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            interstitial = new InterstitialAd(interstitialId_IOS);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleDeckBanner(string scene)
    {
        if ((scene == "DeckBuilder" || scene == "Cards shop") && deckBannerTimer.m_Timer >= MathfUtil.MinutesToSeconds(deckCD))
        {
            ShowBanner(AdPosition.TopRight);
            deckBannerTimer.ResetTimer(true);
        }
    }

    public void HandleMainMenuInterstitial(string scene)
    {
        if (scene == "Main Menu")
        {
            HideBanner();
            if (mainMenuInterstitialTimer != null && mainMenuInterstitialTimer.m_Timer >= MathfUtil.MinutesToSeconds(mainMenuCD))
            {
                ShowInterstitial();
                mainMenuInterstitialTimer.ResetTimer(true);
            }
        }
    }

    public void HandleDeathCountInterstitial()
    {
        playerDeathCount++;
        if (playerDeathCount >= DeathCount)
        {
            ShowInterstitial();
            playerDeathCount = 0;
        }
    }
    #endregion

    #region Events
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleDeckBanner(scene.name);
        HandleMainMenuInterstitial(scene.name);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }
    #endregion
}
