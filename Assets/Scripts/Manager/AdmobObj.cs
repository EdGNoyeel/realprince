using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AdmobObj : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd; // 전면 광고 추가

    public int unLockNumb;
    public int rechargeHeartNumb;
    public bool loaded;

    [SerializeField] Button presentBTN;

    [Header("플랫폼 설정")]
    [SerializeField] bool ios;
    [SerializeField] bool android;
    [SerializeField] bool iosTest;
    [SerializeField] bool androidTest;

    [Header("애드몹 ID")]
    [SerializeField] string iosRewardId = "ca-app-pub-4902307626283456/9629524579";
    [SerializeField] string androidRewardId = "ca-app-pub-4902307626283456/9339967696";
    [SerializeField] string iosTestRewardId = "ca-app-pub-3940256099942544/1712485313";
    [SerializeField] string androidTestRewardId = "ca-app-pub-3940256099942544/5224354917";

    [SerializeField] string iosInterstitialId = "ca-app-pub-4902307626283456/1234567890"; // 예시
    [SerializeField] string androidInterstitialId = "ca-app-pub-4902307626283456/1234567890"; // 예시
    [SerializeField] string iosTestInterstitialId = "ca-app-pub-3940256099942544/4411468910";
    [SerializeField] string androidTestInterstitialId = "ca-app-pub-3940256099942544/1033173712";

    private string rewardAdUnitId;
    private string interstitialAdUnitId;

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
        if (ios)
        {
            rewardAdUnitId = iosRewardId;
            interstitialAdUnitId = iosInterstitialId;
        }
        if (android)
        {
            rewardAdUnitId = androidRewardId;
            interstitialAdUnitId = androidInterstitialId;
        }
        if (iosTest)
        {
            rewardAdUnitId = iosTestRewardId;
            interstitialAdUnitId = iosTestInterstitialId;
        }
        if (androidTest)
        {
            rewardAdUnitId = androidTestRewardId;
            interstitialAdUnitId = androidTestInterstitialId;
        }

        unLockNumb = 0;

        LoadRewardedAd();
        LoadInterstitialAd();
    }

    // -------------------- 리워드 광고 --------------------
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();
        RewardedAd.Load(rewardAdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Rewarded ad failed to load: " + error);
                return;
            }
            rewardedAd = ad;
        });
    }

    public void ShowRewardedAd(int numb)
    {
        unLockNumb = numb;

        if (rewardedAd != null)
        {
            rewardedAd.Show(HandleUserEarnedReward);
        }
        else
        {
            Debug.LogError("Rewarded ad not ready. Reloading...");
            LoadRewardedAd();
        }
    }

    public void ShowReChareHeartAd(int numb)
    {
        unLockNumb = numb;

        if (rewardedAd != null)
        {
            rewardedAd.Show(HandleUserEarnedReward);
        }
        else
        {
            Debug.LogError("Rewarded ad not ready. Reloading...");
            LoadRewardedAd();
        }
    }

    private void HandleUserEarnedReward(Reward reward)
    {
        Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");

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

    // -------------------- 전면 광고 --------------------
    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        InterstitialAd.Load(interstitialAdUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Interstitial ad failed to load: " + error);
                return;
            }
            interstitialAd = ad;
            Debug.Log("IntAdLoaded");
        });
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("TryShowintAd");
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("AdSuccess");
            interstitialAd.Show();
            LoadInterstitialAd(); // 다음 광고 미리 로드
        }
        else
        {
            Debug.LogError("Interstitial ad not ready.");
            LoadInterstitialAd();
        }
    }

    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    void Update()
    {
        loaded = (rewardedAd != null);
    }
}