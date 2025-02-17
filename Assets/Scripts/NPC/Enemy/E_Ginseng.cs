using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Ginseng : Enemy
{
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
    }

    
    void CanHit()
    {
        canHit = true;
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberE;
        killcount++;
        StatusManager.instance.killNumberE = killcount;
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
        }
            
    }

    private void TryAttack()
    {
        canAttack = false;
        Attack();

    }

    private void Attack()
    {
        if(live)
            base.anim.SetTrigger("attack");
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
        if (tooth.Count != 0 )
        {
            int i = Random.Range(0, tooth.Count);

            targetTooth = tooth[i]; // 첫번째를 먼저 

            canAttack = true;
            
        }

    }
}
