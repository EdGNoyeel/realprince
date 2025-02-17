using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Donkaz : Enemy
{
    [SerializeField]
    public GameObject newBullets;
    [SerializeField]
    public float intervalSec;
    

    void Start()
    {
        //  theScore = FindObjectOfType<ScoreManager>();
        base.Targetting();
        //SeletLine();
        SeletLine();
        targetBeacon.SetActive(true);

    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberM;
        killcount++;
        StatusManager.instance.killNumberM = killcount;
    }






    // Update is called once per frame
    void Update()
    {
        if (targetTooth != null)
        {
            if (base.targetTooth.activeSelf == true)
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

            //if (!live)
            //{
            //    CancelInvoke();
            //}

        }
    }

    private void Attack()
    {
        canAttack = false;
        canMove = true;
        //InvokeRepeating("Attack", 0.1f, base.attackSpeed);
        if (canTryAttack)
        {
            canTryAttack = false;
            base.anim.SetTrigger("attack");
            Invoke("FireBullet", 0.3f);
            Invoke("LetAttack", attackSpeed);
        }

    }

    void LetAttack()
    {
        canTryAttack = true;
        canAttack = true;
    }

    void FireBullet()
    {
        GameObject newGameObject = ObjPuller.instance.objectPoolList[4].Dequeue();
        
        newGameObject.transform.position = this.transform.position;
        newGameObject.SetActive(true);
    }
}
