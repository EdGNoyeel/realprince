using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0116 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberA;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberA;
        if (currentScore >= initialScore + 1 && canWin)
        {
            Victory();
            initialScore = currentScore;
            canWin = false;
        }
        int targetScore = initialScore + 1;
        vicCondi = " 포도사탕찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
