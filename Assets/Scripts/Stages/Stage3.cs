using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : EnemyCreater
{
    

    // Start is called before the first frame update
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberC;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {        
        currentScore = StatusManager.instance.killNumberC;
        if (currentScore >= initialScore + 8)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 8;
        vicCondi = "승리조건" + "메론맛 사탕찌꺼기 처치(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
