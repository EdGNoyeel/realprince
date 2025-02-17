using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0136 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberI;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberI;
        if (currentScore >= initialScore + 8 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 8;
        vicCondi = " 계란찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}