using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandMap : MonoBehaviour
{
    [SerializeField]
    public GameObject[] stageBtn1;
    [SerializeField]
    public GameObject[] stageBtn2;
    [SerializeField]
    int landNumb;
    
    //public GameObject lshHead_prefab;

    public GameObject lshHead;
    [SerializeField]
    public GameObject lshHead_pos;
    public string songName;

    public int unlockNumb=0;
    public int stageUnlock1;
    public int stageUnlock2;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        lshHead = GameObject.Find("LSH_Head_prefab");
        Debug.Log(lshHead);
        for (int i = 0; i < stageBtn1.Length; i++)
        {
            //Debug.Log(stageBtn1[i]);


            stageBtn1[i].GetComponent<Button>().interactable = false;
            //Debug.Log("비활성화중");
        }
        for (int i = 0; i < stageBtn2.Length; i++)
        {
            stageBtn2[i].GetComponent<Button>().interactable = false;
        }


        rectTransform = GetComponent<RectTransform>();
        CheckStages();
        ScrollPositionLoad();

        PlayBGM();
    }

    public void ScrollPositionLoad()
    {
        if (PlayerPrefs.HasKey("Land" + landNumb.ToString() + "x")&&PlayerPrefs.HasKey("Land" + landNumb.ToString() + "y"))
        {
            rectTransform.position = new Vector3(PlayerPrefs.GetFloat("Land" + landNumb.ToString() + "x"), PlayerPrefs.GetFloat("Land" + landNumb.ToString() + "y"),0);
            //Debug.Log(PlayerPrefs.GetFloat("Land" + landNumb.ToString() + "x"));
            //Debug.Log(transform.position.x);
        }
    }

    public void ScrollPositionSave()
    {
        PlayerPrefs.SetFloat("Land" + landNumb.ToString() + "x", rectTransform.position.x);
        PlayerPrefs.SetFloat("Land" + landNumb.ToString() + "y", rectTransform.position.y);

        //Debug.Log(PlayerPrefs.GetFloat("Land" + landNumb.ToString() + "x"));
    }

    public void PlayBGM()
    {
        //GameManager.instance.mapBgmName = songName;
        GameObject bgm = GameObject.Find("BGM_Manager");
        bgm.GetComponent<BGM_Manager>().PlayBGM(songName);
    }

    public virtual void CheckStages()
    {
        if (StatusManager.instance.stageUnlock0101 == null|| StatusManager.instance.stageUnlock0101=="0")
        {
            string[] newArr = new string[stageBtn1.Length];
            Debug.Log(stageBtn1.Length);
            for (int i = 0; i < stageBtn1.Length; i++)
            {
                newArr[i] = "0";
            }

            StatusManager.instance.stageUnlock0101 = string.Join(",", newArr);
        }
        Debug.Log(StatusManager.instance.stageUnlock0101);

        //int numb=0;
        unlockNumb = 0;
        //stageUnlock1 = StatusManager.instance.stageLevel1+1;

        string[] newArr2= StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        for (int i = 0; i < newArr2.Length; i++)
        {
            if (newArr2[i] != "0")
            {
                unlockNumb++;
            }
        }

        for (int i = 0; i < unlockNumb+1; i++)
        {
            stageBtn1[i].GetComponent<Button>().interactable = true;
            //numb = i;         
        }
        //lshHead.transform.SetParent(lshHead_pos.transform, false);
        int k = StatusManager.instance.stageNumb[StatusManager.instance.currentLand];
        if (k == 0)
        {
            if(unlockNumb==0)
                lshHead.transform.position = stageBtn1[unlockNumb].transform.position;
            else
                lshHead.transform.position = stageBtn1[unlockNumb-1].transform.position;
        }
        else
        {
            lshHead.transform.position = stageBtn1[k].transform.position;
        }
                
        

        stageUnlock2 = StatusManager.instance.stageLevel2+1;
        for (int j = 0; j < stageUnlock2; j++)
        {
            stageBtn1[j].GetComponent<Button>().interactable = true;
        }
    }

    public virtual void upDateStage(int numb)
    {
        for (int i = 0; i < stageBtn1.Length; i++)
        {
            if (i <= numb+1)
            {
                stageBtn1[i].GetComponent<Button>().interactable = true;
                //Debug.Log("활성화중");
            }
        }

        /*string[] newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        for (int j = 0; j < newArr2.Length; j++)
        {
            if (j == numb)
            {
                if (newArr2[j] == "0")
                    newArr2[j] = "1";
                else
                {
                    int level = int.Parse(newArr2[j]);
                    level++;
                    Debug.Log("레벨" + level);
                    newArr2[j] = level.ToString();


                }


            }
        }
        StatusManager.instance.stageUnlock0101 = string.Join(",", newArr2);*/


        Debug.Log("기본저장");
        CheckStages();

        
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();

    }
}
