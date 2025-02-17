using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0249 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberQ;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberQ;
        if (currentScore >= initialScore + 1 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 1;
        vicCondi = "치킨찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
