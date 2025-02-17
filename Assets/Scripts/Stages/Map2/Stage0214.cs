using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0214 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberL;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberL;
        if (currentScore >= initialScore + 40 && canWin)
        {
            Victory();
            GameManager.instance.UnlockAvatar(12, "STUDENT");
            initialScore = currentScore;
        }
        int targetScore = initialScore + 40;
        vicCondi = "멸치찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}