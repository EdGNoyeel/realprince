using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.SceneManagement;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;
    int unLockNumb;
    public int rechargeHeartNumb;
    
    [SerializeField]
    Button presentBTN;

    public const bool TESTMODE = true;
    public const bool BLOCKAD = false;

    static public AdmobManager instance;

    private RewardedAd rewardAd;
    private string adUnitId;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        unLockNumb = 0;
        MobileAds.Initialize(initStatus => { });

        LoadRewardAd();
    }

    public void LoadRewardAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardAd != null)
        {
                rewardAd.Destroy();
                rewardAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                            + ad.GetResponseInfo());

                rewardAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        if (rewardAd != null)
        {
            rewardAd.Show(HandleUserEarnedReward);
            Debug.Log("🎬 Showing Rewarded Ad...");
        }
        else
        {
            Debug.LogError("🚫 Rewarded Ad Not Loaded! Reloading...");
            LoadRewardAd();
        }
    }

    private void HandleRewardedAdClosed()
    {
        Debug.Log("🎬 Rewarded Ad Closed. Reloading...");
        LoadRewardAd();
    }

    private void HandleRewardedAdFailedToShow(AdError error)
    {
        Debug.LogError("❌ Rewarded Ad Failed to Show: " + error);
        LoadRewardAd();
    }

    private void HandleUserEarnedReward(Reward reward)
    {
        Debug.Log($"🏆 User Earned Reward: {reward.Amount} {reward.Type}");
        
        if (SceneManager.GetActiveScene().name == "Toilet")
        {
            GameObject daily = GameObject.Find("DailyPresent");
            if (daily != null)
            {
                presentBTN.interactable = true;
                daily.GetComponent<DailyPn>().InteractBTN(unLockNumb);
                daily.GetComponent<DailyPn>().ChargeHeart(rechargeHeartNumb);
            }
        }
        else
        {
            GameManager.instance.SecondChance(0);
            unLockNumb = 0;
        }

        LoadRewardAd();
    }
}