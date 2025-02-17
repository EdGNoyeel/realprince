using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeJobbabBullet : Bullets
{
    
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        canMove = true;
    }

    void OnEnable()
    {
        Targetting();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (base.targetTooth.activeSelf == true)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tooth")
        {
            //Debug.Log("명중");
            other.gameObject.GetComponent<NewTooth>()?.OnDamage(base.damage);
            GameObject exEf = ObjPuller.instance.objectPoolList[6].Dequeue();
            exEf.transform.position = other.transform.position;
            exEf.SetActive(true);
            if (targetTooth != null)
                //Destroy(gameObject);
            ObjPuller.instance.objectPoolList[2].Enqueue(mySelf);
            //mySelf.transform.SetParent(ObjPuller.instance.transform, false);
            mySelf.SetActive(false);
        }
        

    }
}
