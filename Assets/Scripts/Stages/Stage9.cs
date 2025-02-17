using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9 : EnemyCreater
{
    // Start is called before the first frame update
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
        if (currentScore >= initialScore + 36)
        {
            Victory();
            initialScore = currentScore;
        }
        int targetScore = initialScore + 36;
        vicCondi = "승리조건: 김치만두 찌꺼기 제거" + "(" + currentScore.ToString() + "/" + targetScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
