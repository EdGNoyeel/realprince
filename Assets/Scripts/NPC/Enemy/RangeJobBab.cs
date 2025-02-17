using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeJobBab : Enemy
{
    //private bool canAttack = true;
    [SerializeField]
    public GameObject newBullets;
    [SerializeField]
    public float intervalSec;
    
    // Start is called before the first frame update
    void Start()
    {
        //  theScore = FindObjectOfType<ScoreManager>();
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
            else
            {
                if (canAttack)
                {
                    Attack();
                    canAttack = false;
                }

            }
        }
        else
        {
            CancelInvoke("CreateBullets");
            base.Targetting();
            base.canMove = true;
            canAttack = true;
        }
    }

    private void Attack()
    {
        InvokeRepeating("CreateBullets", intervalSec, intervalSec);
    }

    void CreateBullets()
    {
        if (base.live)
        {
            GameObject newGameObject = ObjPuller.instance.objectPoolList[2].Dequeue();
            newGameObject.SetActive(true);
            newGameObject.transform.position = this.transform.position;
            
        }
        else
            CancelInvoke("CreateBullets");
    }

}
