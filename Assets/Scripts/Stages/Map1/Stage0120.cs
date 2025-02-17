using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0120 : EnemyCreater
{
    int killedByNPC;
    int killedByNPCNow;
    bool canVic;
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = StatusManager.instance.killNumberD+StatusManager.instance.killNumberF;
        currentScore = initialScore;
        killedByNPC = StatusManager.instance.killedByNPC;
        canVic = true;
        Invoke("RunStory", 0.1f);

    }

    void RunStory()
    {
        //Debug.Log("스토리시작!");
        StoryManager.instance.RunStory("meetccc");
    }

    // Update is called once per frame
    void Update()
    {

        CheckTimerForDual();
        currentScore = StatusManager.instance.killNumberD + StatusManager.instance.killNumberF;
        
        killedByNPCNow = StatusManager.instance.killedByNPC;
        vicCondi = " 내가 처치" + (currentScore - initialScore).ToString() + System.Environment.NewLine + " 최충치가 처치" + (killedByNPCNow - killedByNPC).ToString() + System.Environment.NewLine + " 존버  " + timer.ToString() + " / " + time_Max.ToString(); ;

        ScoreManager.instance.vicConTxt = vicCondi;
    }

    void CheckTimerForDual()
    {
        time_current = Time.time - time_start;
        if (time_current < time_Max)
        {
            timer = $"{time_current:N2}";
            //Debug.Log(time_current);
        }
        else if (!isEnded)
        {
            CheckDualVictory();
        }
    }

    void CheckDualVictory()
    {
        if((currentScore - initialScore)> (killedByNPCNow - killedByNPC))
        {
            if (canVic)
            {
                StoryManager.instance.RunStory("wonccc");
                Victory();
            }
            canVic = false;
            string[] arr = StatusManager.instance.fairyUnLock.Split(new char[] { ',' });
            arr[0] = "1";
            StatusManager.instance.fairyUnLock = string.Join(",", arr);
            if (StatusManager.instance.additionalSlotUnlock == 0)
            {
                StatusManager.instance.additionalSlotUnlock = 1;
                StatusManager.instance.currentFairy = 0;
            }
            GameObject.Find("FairyManager").GetComponent<FairyManager>().CheckFairy();
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
}
