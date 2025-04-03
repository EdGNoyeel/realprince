using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class AdmobObj : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd;

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

    [SerializeField] string iosInterstitialId = "ca-app-pub-4902307626283456/1234567890";
    [SerializeField] string androidInterstitialId = "ca-app-pub-4902307626283456/1234567890";
    [SerializeField] string iosTestInterstitialId = "ca-app-pub-3940256099942544/4411468910";
    [SerializeField] string androidTestInterstitialId = "ca-app-pub-3940256099942544/1033173712";

    private string rewardAdUnitId;
    private string interstitialAdUnitId;

    public static AdmobObj instance;

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
        if(StatusManager.instance.adRemoved==0){
            if (Time.timeScale == 0f)
            {
                Debug.LogWarning("Game is paused. Delaying interstitial ad.");
                Time.timeScale = 1f;
                StartCoroutine(DelayAdAndThenPause(0.1f));
            }
            else
            {
                ShowRealAd();
            }
        }
    }

    private IEnumerator DelayAdAndThenPause(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        ShowRealAd();

        yield return new WaitForSecondsRealtime(0.5f);
        if (ShouldPauseAfterAd())
        {
            Debug.Log("Re-pausing game after ad.");
            Time.timeScale = 0f;
        }
    }

    private bool ShouldPauseAfterAd()
    {
        return true;
    }

    private void ShowRealAd()
    {
        
        Debug.Log("TryShowintAd");
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("AdSuccess");

            // 콜백을 명확하게 핸들러 함수로 연결
            //interstitialAd.OnFullScreenContentClosed += HandleInterstitialClosed;

            interstitialAd.Show();
            LoadInterstitialAd();
        }
        else
        {
            Debug.LogError("Interstitial ad not ready.");
            LoadInterstitialAd();
        }
    }

// 핸들러 함수 정의
    private void HandleInterstitialClosed(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Closed.");

        // 필요 시 게임 일시 정지
        if (ShouldPauseAfterAd())
        {
            Debug.Log("Re-pausing game after ad.");
            Time.timeScale = 0f;
        }

        // 기타 광고 이후 처리 로직 작성
    }

    private void OnInterstitialAdClosed(object sender, EventArgs e)
    {
        Debug.Log("Interstitial Ad Closed.");
        // 광고가 끝난 후 필요한 로직을 여기에 추가하세요.
        // 예: 게임을 다시 일시 정지 또는 다른 작업 수행
    }

    // 광고 준비 여부 확인 함수 추가
    public bool IsInterstitialAdReady()
    {
        return interstitialAd != null && interstitialAd.CanShowAd();
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
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PauseBGMForAd();

            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().ResumeBGMAfterAd();
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                        "with error : " + error);
        };
    }
}