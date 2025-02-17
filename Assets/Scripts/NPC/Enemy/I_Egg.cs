using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Egg : Enemy
{
    public float secAttackRate=1;
    bool canTransform;
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        canAttack = true;
        CancelInvoke("Attack");
        CancelInvoke("SecondaryAttack");
        SeletLine();
        canHit = false;
        Invoke("CanHit", 0.1f);
        canTransform = true;
    }

    void CanHit()
    {
        canHit = true;
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberI;
        killcount++;
        StatusManager.instance.killNumberI = killcount;
    }

    // Update is called once per frame
    void Update()
    {

        if (base.targetTooth != null)
        {
            if (hp >= 0.5 * hpLimit)
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
            if(hp<0.5*hpLimit)
            {
                canMove = false;
                speed = 0;
                rbody.linearVelocity = new Vector2(0, 0);
                if (canTransform)
                {
                    //Debug.Log("변신");
                    canHit = false;
                    anim.SetTrigger("transform");
                    Invoke("Hitable", 1);
                    Targetting();
                    Invoke("SecondaryAttack", secAttackRate);
                    canTransform = false;
                }
                
            }
            
        }
        else
            base.Targetting();
        if (!live)
            CancelInvoke("Attack");
    }
    private void Hitable()
    {        
        canHit = true;
        anim.SetTrigger("idle2");
    }

    private void TryAttack()
    {
        canAttack = false;
        //Attack();
        targetTooth.GetComponent<NewTooth>().OnDamage(damage);
        RandomTargetting();
        

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

    void SecondaryAttack()
    {
        if (base.targetTooth.activeSelf == true && live)        
        {
            targetTooth.GetComponent<NewTooth>().OnDamage(damage);
            Invoke("SecondaryAttack", secAttackRate);
        }
            
    }
}
