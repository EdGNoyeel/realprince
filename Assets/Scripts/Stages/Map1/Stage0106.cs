using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0106 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberB;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberB;
        if (currentScore >= initialScore + 30 && canWin)
        {
            Victory();
            initialScore = currentScore;
            canWin = false;
        }
        int targetScore = initialScore + 30;
        vicCondi = " 레몬사탕 찌꺼기 제거(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
