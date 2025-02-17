using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Enemy
{
    //ScoreManager theScore;

    // Start is called before the first frame update
    void Start()
    {
        //base.canMove = true;
        //base.canHit = true;
        base.Targetting();
    }

    


    // Update is called once per frame
    void Update()
    {
        if (base.targetTooth != null)
        {
            if (base.canMove)
            {
                base.Move();
                //Debug.Log("이동중");
            }
        }
        else
            base.Targetting();
        

    }




}
