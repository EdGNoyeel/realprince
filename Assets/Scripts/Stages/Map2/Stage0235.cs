using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0235 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberE;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberE;
        if (currentScore >= initialScore + 2 && canWin)
        {
            Victory();
            initialScore = currentScore;
            string[] arr = StatusManager.instance.fairyUnLock.Split(new char[] { ',' });
            arr[1] = "1";
            StatusManager.instance.fairyUnLock = string.Join(",", arr);
            if (StatusManager.instance.additionalSlotUnlock == 1)
            {
                StatusManager.instance.additionalSlotUnlock = 2;
                StatusManager.instance.currentFairy = 1;
            }
            GameObject.Find("FairyManager").GetComponent<FairyManager>().CheckFairy();

        }
        int targetScore = initialScore + 2;
        vicCondi = "마늘찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}