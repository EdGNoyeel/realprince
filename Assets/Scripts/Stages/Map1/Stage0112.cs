using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0112 : EnemyCreater
{
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberC;
        currentScore = initialScore;
        Invoke("RunStory", 0.1f);
    }

    void RunStory()
    {
        //Debug.Log("스토리시작!");
        StoryManager.instance.RunStory("dentist2");
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = StatusManager.instance.killNumberC;
        if (currentScore >= initialScore + 36 && canWin)
        {
            
            Victory();
            initialScore = currentScore;
            canWin = false;
        }
        int targetScore = initialScore + 36;
        vicCondi = " 메론사탕찌꺼기 제거" + "(" + (currentScore - initialScore).ToString() + "/" + (targetScore - initialScore).ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
