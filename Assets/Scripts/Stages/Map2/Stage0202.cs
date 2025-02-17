using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0202 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberP;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberP;
        if (currentScore >= initialScore + 3 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 3;
        vicCondi = " 떡볶이 찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}