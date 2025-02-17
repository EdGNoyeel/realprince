using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7 : EnemyCreater
{
    // Start is called before the first frame update
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
        if (currentScore >= initialScore + 20)
        {            
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 20;
        vicCondi = "승리조건" + "고기만두 찌꺼기 제거(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
