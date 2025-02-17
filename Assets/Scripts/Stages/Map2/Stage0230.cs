using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0230 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberK;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberK;
        if (currentScore >= initialScore + 50 && canWin)
        {
            Victory();
            GameManager.instance.UnlockAvatar(3, "BGT");
            initialScore = currentScore;
        }
        int targetScore = initialScore + 50;
        vicCondi = "수박찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}