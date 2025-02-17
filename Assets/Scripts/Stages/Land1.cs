using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Land1 : LandMap
{
    // Start is called before the first frame update
    public override void CheckStages()
    {
        if (StatusManager.instance.stageUnlock0101 == null || StatusManager.instance.stageUnlock0101 == "0")
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
        int unlockNumb = 0;
        //stageUnlock1 = StatusManager.instance.stageLevel1+1;

        string[] newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        for (int i = 0; i < newArr2.Length; i++)
        {
            if (newArr2[i] == "1")
            {
                unlockNumb++;
            }
        }

        for (int i = 0; i < unlockNumb + 1; i++)
        {
            stageBtn1[i].GetComponent<Button>().interactable = true;
            //numb = i;         
        }

        //lshHead.transform.SetParent(lshHead_pos.transform, false);
        lshHead.transform.position = stageBtn1[unlockNumb].transform.position;

        stageUnlock2 = StatusManager.instance.stageLevel2 + 1;
        for (int j = 0; j < stageUnlock2; j++)
        {
            stageBtn1[j].GetComponent<Button>().interactable = true;
        }
    }

    public override void upDateStage(int numb)
    {
        Debug.Log("여기있나?");
        for (int i = 0; i < stageBtn1.Length; i++)
        {
            if (i <= numb + 1)
            {
                stageBtn1[i].GetComponent<Button>().interactable = true;
            }
        }

        /*string[] newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        for (int j = 0; j < newArr2.Length; j++)
        {
            if (j == numb)
            {
                if(newArr2[j]=="0")
                    newArr2[j] = "1";
                else
                {
                    int level = int.Parse(newArr2[j]);
                    level++;
                    Debug.Log("레벨"+level);
                    newArr2[j] = level.ToString();

                    
                }
                    

            }
        }
        StatusManager.instance.stageUnlock0101 = string.Join(",", newArr2);*/

        CheckStages();

        
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();

    }
}
