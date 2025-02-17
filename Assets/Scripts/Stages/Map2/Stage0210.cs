using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0210 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberD;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberD;
        if (currentScore >= initialScore + 40 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 40;
        vicCondi = " 복숭아사탕 찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}