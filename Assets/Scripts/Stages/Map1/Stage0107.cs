using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0107 : EnemyCreater
{


    // Start is called before the first frame update
    void Start()
    {

        vicCondi = "승리조건";
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnded)
            return;
        Check_Timer();
        vicCondi = " 존버" + timer.ToString() + " / " + time_Max.ToString();
        ScoreManager.instance.vicConTxt = vicCondi;

    }
}
