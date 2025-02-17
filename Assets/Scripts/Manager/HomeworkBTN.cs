using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HomeworkBTN : MonoBehaviour
{
    [SerializeField]
    GameObject homeworkPN;
    [SerializeField]
    GameObject homeworkPos;
    [SerializeField]
    GameObject homeworkPrefab;
    [SerializeField]
    GameObject dot;
    public List<GameObject> homeworkList;
    public float prefabGab = 0.2f;

    

    void Start()
    {
        Invoke("CheckDot", 0.2f);
        
    }

    void CheckDot()
    {
        int don = 0;
        int reward = 0;
        //Debug.Log("Do Something");

        for (int i = 0; i < HomeWorkManager.instance.homeworks.Count; i++)
        {
            if (HomeWorkManager.instance.homeworks[i].done)
            {
                don++;
                Debug.Log("숙제달성갯;" + don);
            }
        }

        for (int i = 0; i < HomeWorkManager.instance.homeworks.Count; i++)
        {
            if (HomeWorkManager.instance.homeworks[i].rewarded)
            {
                reward++;
                Debug.Log("리와드갯숫" + reward);
            }
        }
        if (don > reward)
        {
            dot.SetActive(true);
        }
    }

    public void ClickHomeworkBTN()
    {
        HomeWorkManager.instance.CheckHomeWork();
        Invoke("MakePrefab", 0.1f);
        //MakePrefab();
        //homeworkPN.SetActive(true);
    }

    void MakePrefab()
    {
        string[] arr=StatusManager.instance.homeworkReward.Split(new char[] { ',' });
        for (int i = 0; i < HomeWorkManager.instance.homeworks.Count; i++)
        {
            if (i == 0)
            {
                GameObject obj = Instantiate(homeworkPrefab);

                obj.transform.SetParent(homeworkPos.transform, false);
                obj.transform.position = homeworkPos.transform.position;
                obj.GetComponent<HomeworkPrefab>().myNumb = i;
                TextMeshProUGUI[] tmp= obj.GetComponentsInChildren<TextMeshProUGUI>();
                tmp[0].text = HomeWorkManager.instance.homeworkSentence[i];
                tmp[1].text= HomeWorkManager.instance.homeworks[i].rewardNumb.ToString();
                if (HomeWorkManager.instance.homeworks[i].done)
                {
                    obj.GetComponentInChildren<Button>().interactable = true;
                 //   obj.GetComponentInChildren<Toggle>().interactable = true;
                }

                if (arr[i] == "1")
                {
                    obj.GetComponentInChildren<Button>().interactable = false;
                }

                if (HomeWorkManager.instance.homeworks[i].rewarded)
                {
                    obj.GetComponentInChildren<Button>().interactable = false;
                }
                homeworkList.Add(obj);
            }
            else
            {
                GameObject obj = Instantiate(homeworkPrefab);
                homeworkList.Add(obj);
                homeworkList[i].transform.SetParent(homeworkPos.transform, false);
                obj.GetComponent<HomeworkPrefab>().myNumb = i;
                homeworkList[i].transform.position = new Vector3(homeworkList[i-1].transform.position.x, homeworkList[i-1].transform.position.y - prefabGab, homeworkList[i-1].transform.position.z);
                TextMeshProUGUI[] tmp1 = homeworkList[i].GetComponentsInChildren<TextMeshProUGUI>();
                tmp1[0].text = HomeWorkManager.instance.homeworkSentence[i];
                tmp1[1].text = HomeWorkManager.instance.homeworks[i].rewardNumb.ToString();
                if (HomeWorkManager.instance.homeworks[i].done)
                {
                    obj.GetComponentInChildren<Button>().interactable = true;
                }

                if (arr[i] == "1")
                {
                    obj.GetComponentInChildren<Button>().interactable = false;
                }

                if (HomeWorkManager.instance.homeworks[i].rewarded)
                {
                    obj.GetComponentInChildren<Button>().interactable = false;
                }

            }
            
        }

        homeworkPN.SetActive(true);
    }

    public void DestroyPrefab()
    {
        for (int i = 0; i < homeworkList.Count; i++)
        {
            Destroy(homeworkList[i]);
        }

        homeworkList.Clear();
    }
}
