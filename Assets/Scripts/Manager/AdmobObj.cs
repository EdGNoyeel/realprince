using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AdmobObj : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public int unLockNumb;
    public int rechargeHeartNumb;
    public bool loaded;

    [SerializeField]
    Button presentBTN;

    [SerializeField]
    bool ios;
    [SerializeField]
    bool android;
    [SerializeField]
    bool iosTest;
    [SerializeField]
    bool androidTest;

    [SerializeField]
    string iosId = "ca-app-pub-4902307626283456/9629524579";
    [SerializeField]
    string androidId = "ca-app-pub-4902307626283456/9339967696";
    [SerializeField]
    string iosTestId = "ca-app-pub-3940256099942544/1712485313";
    [SerializeField]
    string androidTestId = "ca-app-pub-3940256099942544/5224354917";

    private string adUnitId;

    static public AdmobObj instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        // 광고 ID 설정
        if (ios) adUnitId = iosId;
        if (android) adUnitId = androidId;
        if (iosTest) adUnitId = iosTestId;
        if (androidTest) adUnitId = androidTestId;

        unLockNumb = 0;

        LoadRewardedAd(); // 리워드 광고 로드
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
                rewardedAd.Destroy();
                rewardedAd = null;
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

                rewardedAd = ad;
            });
    }

    private void Update()
    {
        loaded = (rewardedAd != null);
    }

    public void ShowReChareHeartAd(int numb)
    {
        unLockNumb = numb;
        UserChoseToWatchAd();
    }

    public void UserChoseToWatchAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show(HandleUserEarnedReward);
            Debug.Log("🎬 Showing Rewarded Ad...");
        }
        else
        {
            Debug.LogError("🚫 Rewarded Ad not loaded yet. Reloading...");
            LoadRewardedAd(); // 광고 다시 로드
        }
    }

    public void HandleRewardedAdClosed()
    {
        Debug.Log("🎬 Rewarded ad closed. Reloading...");
        LoadRewardedAd();
    }

    public void HandleRewardedAdFailedToShow(AdError error)
    {
        Debug.LogError($"❌ Rewarded ad failed to show: {error.GetMessage()}");
        LoadRewardedAd();
    }

    public void HandleUserEarnedReward(Reward reward)
    {
        Debug.Log($"🏆 User earned reward: {reward.Amount} {reward.Type}");

        if (SceneManager.GetActiveScene().name == "Toilet")
        {
            GameObject daily = GameObject.Find("DailyPresent");
            if (daily != null)
            {
                StatusManager.instance.adRepeated++;
                daily.GetComponent<DailyPn>().InteractBTN(unLockNumb);
                daily.GetComponent<DailyPn>().ChargeHeart(rechargeHeartNumb);
            }
        }
        else
        {
            StatusManager.instance.adRepeated++;
            GameManager.instance.SecondChance(0);
            unLockNumb = 0;
        }

        LoadRewardedAd();
    }
}