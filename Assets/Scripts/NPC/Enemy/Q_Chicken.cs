using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_Chicken : Enemy
{
    public GameObject damageCycle;
    public GameObject eggs;
    bool anger=false;
    //private bool canTargetting=true;
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        canAttack = true;
        CancelInvoke("Attack");
        SeletLine();
        canHit = false;
        Invoke("CanHit", 1);
        damageCycle.SetActive(false);
    }


    void CanHit()
    {
        canHit = true;
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberQ;
        killcount++;
        StatusManager.instance.killNumberQ = killcount;
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
                        TryAttack();
                        canAttack = false;
                    }
                    //canTargetting = true;



                }
            }
            else
                base.Targetting();
            if (!live)
                CancelInvoke("Attack");
            if (hp < hpLimit * 0.5f)
            {
                anger = true;
            }



        }
    }

    private void TryAttack()
    {
        canAttack = false;
        if (!anger)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                Attack();
            }
            else
                Attack2();
        }
        else
        {
            Attack3();
        }
        


    }

    void Attack3()
    {
        anim.SetTrigger("attack3");
        canMove = false;
        Invoke("MakeEgg", 0.3f);
    }

    void MakeEgg()
    {
        RandomTargetting();
        canMove = true;
        GameObject egg = Instantiate(eggs);
        egg.transform.position = this.transform.position;
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");
        canMove = false;
        Invoke("Damage2", 0.3f);

    }

    void Damage2()
    {
        damageCycle.SetActive(true);
        RandomTargetting();
        canMove = true;
        Invoke("CloseDamage2", 0.1f);

    }
    void CloseDamage2()
    {
        GetComponentInChildren<DamageCircle>().damage = damage;
        damageCycle.SetActive(false);
    }

    private void Attack()
    {
        if (live)
            base.anim.SetTrigger("attack1");
        Invoke("Damage", 1f);


    }

    void Damage()
    {
        targetTooth.GetComponent<NewTooth>().OnDamage(damage);
        RandomTargetting();
        canMove = true;

    }

    void RandomTargetting()
    {
        newPos = gameObject.transform.position;
        tooth = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tooth"));
        if (tooth.Count != 0)
        {
            int i = Random.Range(0, tooth.Count);

            targetTooth = tooth[i]; // 첫번째를 먼저 

            canAttack = true;

        }

    }
}
