using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGP_Store_PN : MonoBehaviour
{
    bool firstBuy;
    int goodsNumb;
    [SerializeField]
    GameObject[] firstBuyTMP;
    public string greeting;
    public string worning;
    public string firstThx;
    public string secondThx;
    void OnEnable()
    {
        greeting = worning;
        string[] arr = StatusManager.instance.firstBuy.Split(new char[] { ',' });
        for (int i = 0; i < firstBuyTMP.Length; i++)
        {
            if (arr[i] == "0")
            {
                firstBuyTMP[i].SetActive(true);
            }
            else
                firstBuyTMP[i].SetActive(false);
        }
        
    }

    void Update()
    {
        GetComponentInChildren<ImgPrefabs>().word.text = greeting;
    }
    public void BuyDia(int numb)
    {
        if (firstBuy)
        {
            StatusManager.instance.dia = StatusManager.instance.dia + 2*numb;
            string[] arr = StatusManager.instance.firstBuy.Split(new char[] { ',' });
            arr[goodsNumb] = (int.Parse(arr[goodsNumb])+1).ToString();
            firstBuyTMP[goodsNumb].SetActive(false);

            StatusManager.instance.firstBuy = string.Join(",", arr);
            greeting = firstThx;
            
        }
        else
        {
            StatusManager.instance.dia = StatusManager.instance.dia + numb;
            string[] arr = StatusManager.instance.firstBuy.Split(new char[] { ',' });
            arr[goodsNumb] = (int.Parse(arr[goodsNumb]) + 1).ToString();
            StatusManager.instance.firstBuy = string.Join(",", arr);
            greeting = secondThx;
        }
            

    }

    public void CheckFirstBuy(int numb)
    {
        string[] arr= StatusManager.instance.firstBuy.Split(new char[] { ',' });
        goodsNumb = numb;

        if(arr[numb] == "0")
        {
            firstBuy = true;
        }
        else
            firstBuy = false;
    }
}
