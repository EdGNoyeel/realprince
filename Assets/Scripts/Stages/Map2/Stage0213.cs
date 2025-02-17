using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0213 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberM;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberM;
        if (currentScore >= initialScore + 5 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 5;
        vicCondi = "돈까스찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}