using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0139 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.score;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = ScoreManager.instance.currentScore;
        if (currentScore >= initialScore + 600 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 600;
        vicCondi = " 코인 모으기(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}