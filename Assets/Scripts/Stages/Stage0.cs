using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0 : EnemyCreater
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
        if (currentScore >= initialScore + 5)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 5;
        vicCondi = "승리조건: 포도사탕 찌꺼기 제거" + "(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
