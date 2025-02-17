using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0242 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberN;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberN;
        if (currentScore >= initialScore + 8 && canWin)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 8;
        vicCondi = "붕어빵찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
