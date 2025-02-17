using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public GameObject[] land;
    int[] stageNumb;
    public int landNumb;

    void Start()
    {
        LoadLand(StatusManager.instance.currentLand);
    }

    public void LoadLand(int numb)
    {
        landNumb = numb;
        for (int i = 0; i < land.Length; i++)
        {
            if (i == numb)
            {
                land[i].SetActive(true);
                GameManager.instance.landNumb = numb;
                GameManager.instance.stageNumb[0] = StatusManager.instance.stageLevel1;
                GameManager.instance.stageNumb[1] = StatusManager.instance.stageLevel2;
                GameManager.instance.currentLand=GameManager.instance.landName[numb];
                GameManager.instance.LandMap();
            }

            if(i != numb)
            {
                land[i].SetActive(false);
            }
        }
        int currentLand = StatusManager.instance.currentLand;
    }

    /*void checkStage()
    {
        stageNumb[0]=
    }*/

}
