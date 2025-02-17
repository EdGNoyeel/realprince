using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_WaterMelon : Enemy
{
    [SerializeField]
    public GameObject newBullets;
    [SerializeField]
    public float intervalSec;

    public GameObject[] piece;

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
        int killcount = StatusManager.instance.killNumberK;
        killcount++;
        StatusManager.instance.killNumberK = killcount;
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
        }
           
        //if (!live)
        //{
        //    CancelInvoke();
        //}
        if (hp > hpLimit * 0.75)
        {
            piece[0].SetActive(true);
            piece[1].SetActive(true);
            piece[2].SetActive(true);
        }
        else if (hp > hpLimit * 0.5)
        {
            piece[0].SetActive(false);
            piece[1].SetActive(true);
            piece[2].SetActive(true);
        }
        else if (hp > hpLimit * 0.25)
        {
            piece[0].SetActive(false);
            piece[1].SetActive(false);
            piece[2].SetActive(true);
        }
        else
        {
            piece[0].SetActive(false);
            piece[1].SetActive(false);
            piece[2].SetActive(false);
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
        GameObject newGameObject = Instantiate(newBullets) as GameObject;
        newGameObject.transform.position = this.transform.position;
    }
}
