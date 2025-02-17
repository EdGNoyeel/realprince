using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8 : EnemyCreater
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnded)
            return;
        Check_Timer();
        vicCondi = "존버  " + timer.ToString() + " / " + time_Max.ToString();
        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
