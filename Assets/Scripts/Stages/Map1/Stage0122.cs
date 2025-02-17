using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0122 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberG;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberG;
        if (currentScore >= initialScore + 12 && canWin)
        {
            Victory();
            GameManager.instance.UnlockAvatar(1, "CCC");
            initialScore = currentScore;
            canWin = false;
        }
        int targetScore = initialScore + 12;
        vicCondi = " 김치만두찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
