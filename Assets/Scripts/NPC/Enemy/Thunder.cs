using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Bullets
{

    public GameObject secondaryTarget1;
    public GameObject secondaryTarget2;
    public GameObject secondaryTarget3;

    int attackTime=3;
    // Start is called before the first frame update
    void Start()
    {
        Targetting();
        SecondaryTarget();
        Invoke("KillMySelf", 5);
    }
    void OnEnable()
    {
        Targetting();
        SecondaryTarget();
        Invoke("KillMySelf", 5);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = targetTooth.transform.position - transform.position;

    
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    

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

    void KillMySelf()
    {
        ObjPuller.instance.objectPoolList[4].Enqueue(mySelf);
        mySelf.SetActive(false);
    }

    void SecondaryTarget()
    {
        GameObject[] newArr = GameObject.FindGameObjectsWithTag("Tooth");

        ArrayList list = new ArrayList();



        while(true)
        {
            int i = Random.Range(0, newArr.Length);
            secondaryTarget1 = newArr[i];
            if (secondaryTarget1 != targetTooth)
            {                
                break;
            }
        }

        while(true)
        {
            int i = Random.Range(0, newArr.Length);
            secondaryTarget2 = newArr[i];
            if (secondaryTarget2 != targetTooth && secondaryTarget2 != secondaryTarget1)
            {                
                break;
            }
            
        }

        while (true)
        {
            int i = Random.Range(0, newArr.Length);
            secondaryTarget3 = newArr[i];
            if (secondaryTarget3 != targetTooth && secondaryTarget3 != secondaryTarget1 && secondaryTarget3 != secondaryTarget2)
            {                
                break;
            }
            
        }

        /*Debug.Log(secondaryTarget1);
        Debug.Log(secondaryTarget2);
        Debug.Log(secondaryTarget3);*/

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tooth")
        {
            //Debug.Log("명중");
            other.gameObject.GetComponent<NewTooth>()?.OnDamage(base.damage);
            GameObject exEf = ObjPuller.instance.objectPoolList[5].Dequeue();
            exEf.transform.position = other.transform.position;
            exEf.SetActive(true);
            if (targetTooth != null)
            {
                if (attackTime == 3)
                {
                    targetTooth = secondaryTarget1;
                    attackTime--;
                    canMove = true;
                    damage = damage / 2;
                    return;
                    
                    
                }

                if (attackTime == 2)
                {
                    targetTooth = secondaryTarget2;
                    attackTime--;
                    canMove = true;
                    damage = damage / 2;
                    return;
                }

                if (attackTime == 1)
                {
                    targetTooth = secondaryTarget3;
                    attackTime--;
                    canMove = true;
                    damage = damage / 2;
                    return;
                }

                if (attackTime == 0)
                {
                    ObjPuller.instance.objectPoolList[4].Enqueue(mySelf);
                    mySelf.SetActive(false);
                }

            }
                
        }


    }
}
