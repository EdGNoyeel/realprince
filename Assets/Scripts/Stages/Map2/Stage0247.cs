using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0247 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberE;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberE;
        if (currentScore >= initialScore + 3 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 3;
        vicCondi = "인삼찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
