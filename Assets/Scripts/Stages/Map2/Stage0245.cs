using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0245 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberJ;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberJ;
        if (currentScore >= initialScore + 20 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 20;
        vicCondi = "문어찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
