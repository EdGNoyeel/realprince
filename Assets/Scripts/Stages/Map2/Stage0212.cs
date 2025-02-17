using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0212 : EnemyCreater
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
        if (currentScore >= initialScore + 20000 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 20000;
        vicCondi = " 코인 모으기(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}