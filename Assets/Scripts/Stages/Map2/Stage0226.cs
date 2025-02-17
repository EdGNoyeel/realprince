using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0226 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberF;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberF;
        if (currentScore >= initialScore + 32 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 32;
        vicCondi = "고기만두찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}