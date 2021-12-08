using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

using GoogleMobileAdsMediationTestSuite.Api;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    public static bool IsTestAds = false;
    private static BannerView _BannerView;
    private static InterstitialAd _InterstitialAd;
    private static RewardedAd _RewardedAd;
    private static bool _IsInitialized = false;

    public GameObject loadingPanel;
    public GameObject loadingAdsPanel;
    public GameObject adsNotAvailablePanel;
    public bool isGameplayScene;

    public UnityEvent onRewardedWin;

    private string liveBannerId = "ca-app-pub-8431988213576616/6571866727"; //"ca-app-pub-5212365178857760/1617669272";
    private string liveInterstitialId = "ca-app-pub-8431988213576616/7132756464"; //"ca-app-pub-5212365178857760/1425606319";
    private string liveRewardedId = "ca-app-pub-8431988213576616/5801809874"; //"ca-app-pub-5212365178857760/7942913200";
    
    private string testBannerId = "ca-app-pub-3940256099942544/6300978111";
    private string testInterstitialId = "ca-app-pub-3940256099942544/1033173712";
    private string testRewardedId = "ca-app-pub-3940256099942544/5224354917";

    private string _currentBannerId;
    private string _currentInterstitialId;
    private string _currentRewardedId;

    private bool _rewardedWon = false;

    private void Start()
    {
        _currentBannerId = (IsTestAds) ? testBannerId : liveBannerId;
        _currentInterstitialId = (IsTestAds) ? testInterstitialId : liveInterstitialId;
        _currentRewardedId = (IsTestAds) ? testRewardedId : liveRewardedId;
        
        if (_IsInitialized) { return; }
        
        SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
        
        MediationTestSuite.Show();
        
        var request = new RequestConfiguration.Builder().build();
        MobileAds.SetRequestConfiguration(request);
        
        loadingPanel.SetActive(true);
        MobileAds.Initialize(status =>
        {
            _IsInitialized = true;
            
            Dictionary<string, AdapterStatus> map = status.getAdapterStatusMap();
            Debug.Log(String.Join("\n",map.ToList()));
            
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus adapterStatus = keyValuePair.Value;
                
                switch (adapterStatus.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        Debug.Log("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        Debug.Log("Adapter: " + className + " is initialized.");
                        break;
                }
            }

            this.Invoke(() =>
            {
                RequestBanner();
                RequestInterstitial();
                RequestRewarded();
            }, 10.0f);
            
            this.Invoke(() =>
            {
                loadingPanel.SetActive(false);
            }, 6.0f);
        });
    }

    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        if (isGameplayScene)
        {
            ShowBannerAd();
        }
    }
    
    public IEnumerator OnRewardWin() {
        // give reward
        yield return null;
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("InitialAD");
        ShowInterstitialAd();
    }

    #region BannerAds

    public void ShowBannerAd()
    {
        RequestBanner();
        if (_BannerView != null) _BannerView.Show();
    }

    public void HideBannerAd()
    {
        if (_BannerView != null) _BannerView.Hide();
    }

    public void RequestBanner()
    {
        _BannerView = new BannerView(_currentBannerId, new AdSize(320,50), AdPosition.Top);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _BannerView.LoadAd(request);
        // Called when an ad request has successfully loaded.
        _BannerView.OnAdLoaded += HandleOnAdLoadedBanner;
        // Called when an ad request failed to load.
        _BannerView.OnAdFailedToLoad += HandleOnAdFailedToLoadBanner;
        Debug.Log("Banner Requested");
    }
    public void HandleOnAdLoadedBanner(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoadBanner(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd_Banner event received with message: "
                            + args.LoadAdError.GetMessage() + " , " + System.Environment.StackTrace);
    }
    
    #endregion

    #region Interstitial

    public void RequestInterstitial()
    {
        _InterstitialAd = new InterstitialAd(_currentInterstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        _InterstitialAd.LoadAd(request);
        // Called when an ad request has successfully loaded.
        _InterstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        _InterstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        Debug.Log("Interstitial Requested");
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd_Interstitial event received with message: "
                            + e.LoadAdError.GetMessage() + " , " + System.Environment.StackTrace);
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
    
    public bool ShowInterstitialAd()
    {
        if (_InterstitialAd != null && _InterstitialAd.IsLoaded())
        {
            Debug.Log("Loaded");
            _InterstitialAd.Show();
            this.Invoke(RequestInterstitial,2.0f);
            return true;
        }
        else
        {
            this.Invoke(RequestInterstitial,2.0f);
            Debug.Log("Not-Loaded");
            return false;
        }
    }

    #endregion

    #region Rewarded

    public void ShowRewardedAd()
    {
        loadingAdsPanel.SetActive(true);
        if(_RewardedAd!=null && _RewardedAd.IsLoaded())
        {
            _RewardedAd.Show();
        }
        else
        {
            RequestRewarded();
            this.Invoke(()=>
            {
                loadingAdsPanel.SetActive(false);
                adsNotAvailablePanel.SetActive(true);
            },2.0f);
        }
    }
    
    public void RequestRewarded()
    {
        _RewardedAd = new RewardedAd(_currentRewardedId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        _RewardedAd.LoadAd(request);
        _RewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        _RewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when the user should be rewarded for interacting with the ad.
        _RewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        Debug.Log("Rewarded Requested");
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        Debug.Log("Rewarded Ad Finished");
        PlayerPrefs.SetInt("RewardWon", 1);
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("RewardedAdFailed TO Load"+e.LoadAdError.GetMessage() + " , " + System.Environment.StackTrace);
        loadingAdsPanel.SetActive(false);
        adsNotAvailablePanel.SetActive(true);
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
        Debug.Log("Rewarded Ad Loaded");
        loadingAdsPanel.SetActive(false);
    }
    
    #endregion
}