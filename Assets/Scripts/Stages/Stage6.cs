using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : EnemyCreater
{
    bool canVic = true;
    // Start is called before the first frame update
    void Start()
    {
        vicCondi = "승리조건";
        initialScore = (int)GameObject.Find("E_Ginseng").GetComponent<E_Ginseng>().hpLimit;
        currentScore = (int)GameObject.Find("E_Ginseng").GetComponent<E_Ginseng>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = (int)GameObject.Find("E_Ginseng").GetComponent<E_Ginseng>().hp;
        if (currentScore <= 0 && canVic)
        {
            canVic = false;
            Invoke("Victory", 2);
            //Victory();

        }
        vicCondi = "승리조건 " + "(" + currentScore.ToString() + "/" + initialScore.ToString() + ")";

        ScoreManager.instance.vicConTxt = vicCondi;
    }
}
