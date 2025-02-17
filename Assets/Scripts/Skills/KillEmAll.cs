using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEmAll : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        //anim.SetTrigger("kill");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Enemy>()?.Damage(damage);
        other.gameObject.GetComponent<C_Bullet>()?.Damage(damage);
    }
}
