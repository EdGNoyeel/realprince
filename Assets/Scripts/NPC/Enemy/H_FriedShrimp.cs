using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_FriedShrimp : Enemy
{
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
        int killcount = StatusManager.instance.killNumberH;
        killcount++;
        StatusManager.instance.killNumberH = killcount;
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
                        TryAttack();

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
        canMove = true;
        //InvokeRepeating("Attack", 0.1f, base.attackSpeed);
        if (canTryAttack)
        {
            canTryAttack = false;
            base.anim.SetTrigger("attack");
            Invoke("Damage", 0.1f);
            Invoke("LetAttack", attackSpeed);
        }

    }

    void LetAttack()
    {
        canTryAttack = true;
        canAttack = true;
    }

    void Damage()
    {
        GameObject exEf = Instantiate(EffectPrefab);
        exEf.transform.position = targetTooth.transform.position;
        if (targetTooth != null)
            targetTooth.GetComponent<NewTooth>().OnDamage(damage);
        else
            Targetting();
    }


}
