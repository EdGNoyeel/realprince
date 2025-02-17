using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    public float damage;
    public float damageMultiply;
    CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        //damage = GetComponentInParent<Enemy>().damage;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tooth")
        {
            Debug.Log("데미지서클동작");
            other.gameObject.GetComponent<NewTooth>()?.OnDamage(damage * damageMultiply);
            // Destroy(gameObject);
        }
    }
}
