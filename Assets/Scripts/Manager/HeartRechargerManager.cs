using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class HeartRechargerManager : MonoBehaviour
{
    #region Heart
    /* 화면에 표시하기 위한 UI변수. NGUI가 있다면 사용가능
    public UILabel appQuitTimeLabel = null;
    public UILabel heartRechargeTimer = null;
    public UILabel heartAmountLabel = null;
    */
    static public HeartRechargerManager instance;
    public int m_HeartAmount; //보유 하트 개수
    private DateTime m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
    public const int MAX_HEART = 60; //하트 최대값
    public int HeartRechargeInterval;// 하트 충전 간격(단위:초)
    private Coroutine m_RechargeTimerCoroutine = null;
    private int m_RechargeRemainTime = 0;
    //public TextMeshProUGUI heartTMP;
    int heartUseNumb = 1;
    #endregion

    private void Awake()
    {
        //GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Load();
        Debug.Log("어웨이크당시 하트갯수: " + StatusManager.instance.heartCount);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Init();
        }

        else
            Destroy(this.gameObject);
        
    }
    //게임 초기화, 중간 이탈, 중간 복귀 시 실행되는 함수
    public void OnApplicationFocus(bool value)
    {
        Debug.Log("OnApplicationFocus() : " + value);
        if (value)
        {
            LoadHeartInfo();
            LoadAppQuitTime();
            SetRechargeScheduler();
        }
        else
        {
            SaveHeartInfo();
            SaveAppQuitTime();
        }
    }
    //게임 종료 시 실행되는 함수
    public void OnApplicationQuit()
    {
        Debug.Log("GoodsRechargeTester: OnApplicationQuit()");
        SaveHeartInfo();
        SaveAppQuitTime();
    }
    //버튼 이벤트에 이 함수를 연동한다.
    public void OnClickUseHeart()
    {
        Debug.Log("OnClickUseHeart");
        UseHeart();
    }

    public void Init()
    {
        //if (PlayerPrefs.HasKey("HeartAmount"))
        //{
        //Debug.Log("PlayerPrefs has key : HeartAmount");
        //m_HeartAmount = PlayerPrefs.GetInt("HeartAmount");
        m_HeartAmount = StatusManager.instance.heartCount;
        Debug.Log("여기까지"+m_HeartAmount);
        if (m_HeartAmount < 0)
        {
            m_HeartAmount = 0;
        }
        //}
        LoadHeartInfo();
        SetRechargeScheduler();

        //m_HeartAmount = 0;
        //heartTMP.text = m_HeartAmount.ToString();
        m_RechargeRemainTime = 0;
        //m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
        //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
        //heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);
    }
    public bool LoadHeartInfo()
    {
        //Debug.Log("LoadHeartInfo");
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("HeartAmount"))
            {
                //Debug.Log("PlayerPrefs has key : HeartAmount");
                //m_HeartAmount = PlayerPrefs.GetInt("HeartAmount");
                m_HeartAmount = StatusManager.instance.heartCount;
                if (m_HeartAmount < 0)
                {
                    m_HeartAmount = 0;
                }
            }
            else
            {
                //m_HeartAmount = MAX_HEART;
            }
            //heartAmountLabel.text = m_HeartAmount.ToString();
            //ebug.Log("Loaded HeartAmount : " + m_HeartAmount);
            //heartTMP.text = m_HeartAmount.ToString();
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveHeartInfo()
    {
        //Debug.Log("SaveHeartInfo");
        bool result = false;
        try
        {
            //PlayerPrefs.SetInt("HeartAmount", m_HeartAmount);
            StatusManager.instance.heartCount = m_HeartAmount;
            GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();
            //PlayerPrefs.Save();
            //Debug.Log("Saved HeartAmount : " + m_HeartAmount);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool LoadAppQuitTime()
    {
        //Debug.Log("LoadAppQuitTime");
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("AppQuitTime"))
            {
                //Debug.Log("PlayerPrefs has key : AppQuitTime");
                var appQuitTime = string.Empty;
                appQuitTime = PlayerPrefs.GetString("AppQuitTime");
                Debug.Log(m_AppQuitTime);
                m_AppQuitTime = DateTime.FromBinary(Convert.ToInt64(appQuitTime));
            }
            //Debug.Log(string.Format("Loaded AppQuitTime : {0}", m_AppQuitTime.ToString()));
            //appQuitTimeLabel.text = string.Format("AppQuitTime : {0}", m_AppQuitTime.ToString());
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }
    public bool SaveAppQuitTime()
    {
        Debug.Log("SaveAppQuitTime");
        bool result = false;
        try
        {
            var appQuitTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
            PlayerPrefs.SetString("AppQuitTime", appQuitTime);
            PlayerPrefs.Save();

            Debug.Log("Saved AppQuitTime : " + DateTime.Now.ToLocalTime().ToString());
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }
    public void SetRechargeScheduler(Action onFinish = null)
    {
        if (m_RechargeTimerCoroutine != null)
        {
            StopCoroutine(m_RechargeTimerCoroutine);
        }
        LoadAppQuitTime();
        Debug.Log(m_AppQuitTime);
        Debug.Log(DateTime.Now.ToLocalTime());
        var timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - m_AppQuitTime).TotalSeconds);
        //Debug.Log("TimeDifference In Sec :" + timeDifferenceInSec + "s");
        Debug.Log(timeDifferenceInSec);
        var heartToAdd = timeDifferenceInSec / HeartRechargeInterval;
        Debug.Log(HeartRechargeInterval);
        Debug.Log("Heart to add : " + heartToAdd);
        Debug.Log(m_HeartAmount);
        var remainTime = timeDifferenceInSec % HeartRechargeInterval;
        //Debug.Log("RemainTime : " + remainTime);
        if (m_HeartAmount < MAX_HEART)
        {
            int sumNumb = m_HeartAmount + heartToAdd;

            if (sumNumb >= MAX_HEART)
            {
                m_HeartAmount = MAX_HEART;
            }
            else
                m_HeartAmount = sumNumb;
        }
        
        //heartTMP.text = m_HeartAmount.ToString();
        if (m_HeartAmount >= MAX_HEART)
        {
            //m_HeartAmount = MAX_HEART;
            //heartTMP.text = m_HeartAmount.ToString();
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(remainTime, onFinish));
        }
        //heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
        //Debug.Log("HeartAmount : " + m_HeartAmount);
    }
    public void UseHeart(Action onFinish = null)
    {
        //heartTMP.text = m_HeartAmount.ToString();
        if (m_HeartAmount <= heartUseNumb)
        {
            return;
        }

        m_HeartAmount=m_HeartAmount-heartUseNumb;
        StatusManager.instance.heartCount = m_HeartAmount;
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();
        //heartTMP.text = m_HeartAmount.ToString();
        //heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
        if (m_RechargeTimerCoroutine == null)
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval));
        }
        if (onFinish != null)
        {
            onFinish();
        }
    }

    public void UseHeartNumb(int numb)
    {
        heartUseNumb = numb;
    }

    private IEnumerator DoRechargeTimer(int remainTime, Action onFinish = null)
    {
        //Debug.Log("DoRechargeTimer");
        if (remainTime <= 0)
        {
            m_RechargeRemainTime = HeartRechargeInterval;
        }
        else
        {
            m_RechargeRemainTime = remainTime;
        }
        //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
        //heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);

        while (m_RechargeRemainTime > 0)
        {
            //Debug.Log("heartRechargeTimer : " + m_RechargeRemainTime + "s");
            //heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);
            m_RechargeRemainTime -= 1;
            //heartTMP.text = m_HeartAmount.ToString();
            //Debug.Log("하트충전/" +m_HeartAmount);
            yield return new WaitForSecondsRealtime(1);
            
        }
        m_HeartAmount++;
        
        if (m_HeartAmount >= MAX_HEART)
        {
            //m_HeartAmount = MAX_HEART;
            StatusManager.instance.heartCount = m_HeartAmount;
            m_RechargeRemainTime = 0;
            //heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);
            //Debug.Log("HeartAmount reached max amount");
            m_RechargeTimerCoroutine = null;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval, onFinish));
        }
        //heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
        //Debug.Log("HeartAmount : " + m_HeartAmount);
    }


}


