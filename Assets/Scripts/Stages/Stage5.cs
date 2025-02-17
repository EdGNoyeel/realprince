using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : EnemyCreater
{
    
    // Start is called before the first frame update
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.score;
        currentScore = initialScore;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = ScoreManager.instance.currentScore;
        if (currentScore >= initialScore + 1000)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 1000;
        vicCondi = "승리조건" + "(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
