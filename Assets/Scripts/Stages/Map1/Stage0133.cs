using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0133 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberH;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberH;
        if (currentScore >= initialScore + 12 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 12;
        vicCondi = " 새우튀김찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}