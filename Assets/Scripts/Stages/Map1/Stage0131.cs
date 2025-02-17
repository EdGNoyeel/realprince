using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0131 : EnemyCreater
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
        if (currentScore >= initialScore + 7000 && canWin)
        {
            Victory();
            GameManager.instance.UnlockAvatar(8, "GDZ");
            initialScore = currentScore;
        }
        int targetScore = initialScore + 7000;
        vicCondi = " 코인 모으기(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}