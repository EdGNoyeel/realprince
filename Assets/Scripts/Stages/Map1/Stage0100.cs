using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0100 : EnemyCreater
{
    
    // Start is called before the first frame update
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberA;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberA;
        if (currentScore >= initialScore + 5&&canWin)
        {
            Victory();
            GameManager.instance.UnlockAvatar(0, "LSH");
            initialScore = currentScore;
            canWin = false;
        }
        int targetScore = initialScore + 5;
        vicCondi = " 포도사탕 찌꺼기 제거" + "(" + (currentScore-initialScore).ToString() + "/" + (targetScore-initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
