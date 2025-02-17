using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DailyPn : MonoBehaviour
{
    bool canBuy;
    bool canRecordTime;
    public string targetProductId;
    public GameObject[] BTNs;
    public GameObject[] eventsBTNs;
    public int unlockBTN=0;
    public int nowTime;
    public int presentTimeInterval=30;
    [SerializeField]
    int eventsDia;
    [SerializeField]
    GameObject cantPresent;
    [SerializeField]
    TextMeshProUGUI cantPresentTMP;
    //private DateTime m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
    // Start is called before the first frame update
    /*void Start()
    {
        for (int i = 0; i < BTNs.Length; i++)
        {
            BTNs[i].interactable = false;
        }

        for (int i = 0; i < unlockBTN; i++)
        {
            BTNs[i].interactable = true;
        }

        if (targetProductId == IAPManager.removeDailyAd)
        {
            if (IAPManager.Instance.HadPurchased(targetProductId))
            {
                canBuy = false;
                return;
            }

        }
        canBuy = true;

    }*/

    void OnEnable()
    {
        string[] arr = StatusManager.instance.events.Split(new char[] { ',' });
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != "0")
            {
                eventsBTNs[i].GetComponent<Button>().interactable = false;
            }
        }
        canRecordTime = true;
        nowTime = DateTime.Now.Minute + DateTime.Now.Hour * 60;
        Debug.Log(nowTime.ToString()+"-"+ StatusManager.instance.presentedTime.ToString());
        if (nowTime >= StatusManager.instance.presentedTime + presentTimeInterval || StatusManager.instance.presentedTime == 1501)
        {
            for (int i = 0; i < BTNs.Length; i++)
            {
                BTNs[i].GetComponent<AdButton>().canEnable = false;
            }

            for (int i = 0; i < unlockBTN; i++)
            {
                BTNs[i].GetComponent<AdButton>().canEnable = true;
            }

            // if (targetProductId == IAPManager.removeDailyAd)
            // {
            //     if (IAPManager.Instance.HadPurchased(targetProductId))
            //     {
            //         canBuy = false;
            //         return;
            //     }

            // }
            canBuy = true;
        }
        else
        {
            cantPresent.SetActive(true);
            cantPresentTMP.text=(presentTimeInterval-(nowTime-StatusManager.instance.presentedTime)).ToString()+"분 뒤에 다시시도하세요";

        }
    }
    public void LinkURL(int numb)
    {
        string[] arr = StatusManager.instance.events.Split(new char[] { ',' });
        if (numb == 0)
        {
            if (arr[numb] == "0")
            {
                Application.OpenURL("https://cafe.naver.com/gameoim");
                arr[numb] = (numb + 1).ToString();
                StatusManager.instance.events = string.Join(",", arr);
                StatusManager.instance.dia = StatusManager.instance.dia + eventsDia;
            }
            
        }

        if (numb == 1)
        {
            if (arr[numb] == "0")
            {
                Application.OpenURL("https://www.youtube.com/channel/UCECjcGx2pwivtr7UVEg7BQQ?sub_confirmation=1");
                arr[numb] = (numb + 1).ToString();
                StatusManager.instance.events = string.Join(",", arr);
                StatusManager.instance.dia = StatusManager.instance.dia + eventsDia;
            }
        }
    }

    public void InteractBTN(int numb)
    {
        nowTime = DateTime.Now.Minute + DateTime.Now.Hour * 60;
        if (canRecordTime)
        {
            StatusManager.instance.presentedTime = nowTime;
        }
        canRecordTime = false;
        if(numb<BTNs.Length-1)
            BTNs[numb+1].GetComponent<AdButton>().canEnable = true;
    }

    public void ChargeHeart(int numb)
    {
        HeartRechargerManager.instance.GetComponent<HeartRechargerManager>().UseHeartNumb(-numb);
        HeartRechargerManager.instance.GetComponent<HeartRechargerManager>().UseHeart();
    }
    public void Close()
    {
        StatusManager.instance.dailyPresent = false;
    }

    public void RechargeHeartNumbChange(int numb)
    {
        //AdmobObj.instance.RechargeHeartNumbChange(numb);
    }

    public void ShowRechargeHeartAd(int numb)
    {
        AdmobObj.instance.ShowReChareHeartAd(numb);
    }
}


