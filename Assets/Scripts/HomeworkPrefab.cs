using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkPrefab : MonoBehaviour
{
    public int myNumb;
    [SerializeField]
    GameObject checkV;

    private void Start()
    {
        if (HomeWorkManager.instance.homeworks[myNumb].rewarded)
        {
            checkV.SetActive(true);
        }
    }
    public void AddDia(int numb)
    {
        StatusManager.instance.dia = StatusManager.instance.dia + numb;
    }

    public void Rewarded()
    {
        HomeWorkManager.instance.homeworks[myNumb].rewarded = true;
        checkV.SetActive(true);
        string[] arr = StatusManager.instance.homeworkReward.Split(new char[] { ',' });
        arr[myNumb] = "1";
        StatusManager.instance.homeworkReward = string.Join(",", arr);
    }
}
