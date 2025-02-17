using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MeatMandu : Enemy
{
    Vector2 attackPos;
    bool canBack=true;
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        canAttack = true;
        CancelInvoke("Attack");
        SeletLine();
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberF;
        killcount++;
        StatusManager.instance.killNumberF = killcount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetTooth != null)
        {
            if (base.targetTooth.activeSelf == true)
            {
                if (live)
                {
                    if (base.canMove)
                    {
                        base.Move();

                    }
                    else
                    {
                        if (canAttack)
                            TryAttack();
                        else
                            StepBack();

                    }

                    if (Vector2.Distance(targetTooth.transform.position, gameObject.transform.position) <= 0.01)
                    {

                        if (canAttack)
                            targetTooth.GetComponent<NewTooth>().OnDamage(damage);
                        canAttack = false;
                        StepBack();
                    }
                }

            }
            else
                base.Targetting();
            if (!live)
                CancelInvoke("Attack");
        }
            
    }
    void TryAttack()
    {
        //Debug.Log("공격시도");
        if (canBack)
        {
            attackPos = this.transform.position;
            anim.SetTrigger("attack");
            
            canBack = false;
        }
        
        
        transform.position = Vector2.MoveTowards(transform.position, targetTooth.transform.position, 0.08f);
    }

    void StepBack()
    {
        //Debug.Log("물러서는중");
        transform.position = Vector2.MoveTowards(transform.position, attackPos, 0.08f);
        if(Vector2.Distance(this.transform.position, attackPos) <= 0.001)
        {
            //Debug.Log("다시공격");
            base.Targetting();
            canAttack = true;
            CancelInvoke("Attack");
            canBack = true;
        }
    }


}
