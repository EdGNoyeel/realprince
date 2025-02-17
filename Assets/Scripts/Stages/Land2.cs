using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Land2 : LandMap
{
    public override void CheckStages()
    {
        if (StatusManager.instance.stageUnlock0102 == null || StatusManager.instance.stageUnlock0102 == "0")
        {
            string[] newArr = new string[stageBtn1.Length];
            Debug.Log(stageBtn1.Length);
            for (int i = 0; i < stageBtn1.Length; i++)
            {
                newArr[i] = "0";
            }

            StatusManager.instance.stageUnlock0102 = string.Join(",", newArr);
        }
        //Debug.Log(StatusManager.instance.stageUnlock0102);

        //int numb=0;
        int unlockNumb = 0;
        //stageUnlock1 = StatusManager.instance.stageLevel1+1;

        string[] newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
        for (int i = 0; i < newArr2.Length; i++)
        {
            if (newArr2[i] != "0")
            {
                unlockNumb++;
            }
        }

        for (int i = 0; i < unlockNumb + 1; i++)
        {
            stageBtn1[i].GetComponent<Button>().interactable = true;
            //numb = i;         
        }

        int k = StatusManager.instance.stageNumb[StatusManager.instance.currentLand];
        if (k == 0)
        {
            if (unlockNumb == 0)
                lshHead.transform.position = stageBtn1[unlockNumb].transform.position;
            else
                lshHead.transform.position = stageBtn1[unlockNumb - 1].transform.position;
        }
        else
        {
            lshHead.transform.position = stageBtn1[k].transform.position;
        }

        //lshHead.transform.SetParent(lshHead_pos.transform, false);
        //lshHead.transform.position = stageBtn1[unlockNumb].transform.position;

        stageUnlock2 = StatusManager.instance.stageLevel2 + 1;
        for (int j = 0; j < stageUnlock2; j++)
        {
            stageBtn1[j].GetComponent<Button>().interactable = true;
        }
    }

    public override void upDateStage(int numb)
    {
        for (int i = 0; i < stageBtn1.Length; i++)
        {
            if (i <= numb + 1)
            {
                stageBtn1[i].GetComponent<Button>().interactable = true;
            }
        }

        /*string[] newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
        for (int j = 0; j < newArr2.Length; j++)
        {
            if (j <= numb)
            {
                newArr2[j] = "1";
            }
        }
        StatusManager.instance.stageUnlock0102 = string.Join(",", newArr2);*/
        Debug.Log("하위클래스저장");
        CheckStages();

        
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();

    }
}
