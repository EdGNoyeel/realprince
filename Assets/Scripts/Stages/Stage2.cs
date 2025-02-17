using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : EnemyCreater
{
    

    // Start is called before the first frame update
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberB;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        

        currentScore = StatusManager.instance.killNumberB;
        if (currentScore >= initialScore + 20)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 20;
        vicCondi = "승리조건" + "오렌지맛 사탕찌꺼기 제거(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
