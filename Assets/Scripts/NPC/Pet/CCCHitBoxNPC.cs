using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCCHitBoxNPC : CCCHitBox
{
    [SerializeField]
    //float damage = 5;

    //public float damageMultiply;

    void Update()
    {
        damage = GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //Debug.Log("명중");
            other.gameObject.GetComponent<Enemy>()?.DamageByNPC(damage*damageMultiply);
           // Destroy(gameObject);
        }
    }
}
