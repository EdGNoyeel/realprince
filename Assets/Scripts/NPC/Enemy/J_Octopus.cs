using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Octopus : Enemy
{
    [SerializeField]
    GameObject mukmul;
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        canAttack = true;
        CancelInvoke("Attack");
        
        SeletLine();
        
        //Invoke("CanHit", 1);
        InvokeRepeating("Attack", attackSpeed, attackSpeed);
        
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
                    //Debug.Log("공격");
                    TryAttack();
                    //canAttack = false;
                }
                //canTargetting = true;
            }




        }
        else
            base.Targetting();
        if (!live)
            CancelInvoke("Attack");
    }

    private void TryAttack()
    {
        canAttack = false;
        //Attack();
        //targetTooth.GetComponent<Tooth>().OnDamage(damage);
        RandomTargetting();

    }

    void Attack()
    {
        anim.SetTrigger("attack");
        Invoke("MakeMukmul", 0.7f);
    }

    void MakeMukmul()
    {
        GameObject story = GameObject.Find("MukMulPos");
        GameObject muk = Instantiate(mukmul);
        muk.transform.position = new Vector3(0, 0, 0);
        muk.transform.SetParent(story.transform, false);
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberJ;
        killcount++;
        StatusManager.instance.killNumberJ = killcount;
    }
    void RandomTargetting()
    {
        //Debug.Log("렌덤공격");

        canAttack = true;
        newPos = gameObject.transform.position;
        tooth = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tooth"));
        if (tooth.Count != 0)
        {
            int i = Random.Range(0, tooth.Count);

            targetTooth = tooth[i]; // 첫번째를 먼저 
            //ebug.Log(targetTooth);
            canMove = true;
            canAttack = true;

        }

    }
}
