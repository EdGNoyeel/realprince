using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class C_Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject mySelf;
    float hp = 1;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField]
    Animator anim;
    protected Rigidbody2D rbody;
    public float damage = 1;
    public float attackSpeed = 3;
    GameObject targetTooth;
    bool canhit;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        var color = _spriteRenderer.color;
        color.a = 1;
        _spriteRenderer.color = color;
        canhit = true;
    }

    void Enable()
    {
        var color = _spriteRenderer.color;
        color.a = 1;
        _spriteRenderer.color = color;
        canhit = true;
    }

    // Update is called once per frame
    public void TryAttack(GameObject target)
    {
        targetTooth= target;
        //Invoke("Attack", attackSpeed);
        Timing.RunCoroutine(Attack().CancelWith(gameObject));

    }

    public IEnumerator<float> Attack()
    {
        yield return Timing.WaitForSeconds(2);
        targetTooth.GetComponent<NewTooth>().OnDamage(damage);
        
    }

    public void Damage(float damage)
    {
     
        if (canhit)
        {
            hp -= damage;
            anim.SetTrigger("death");
            //Debug.Log(transform.name);
            //StartCoroutine(FadeAway());
            Timing.RunCoroutine(FadeAway().CancelWith(gameObject));
            canhit = false;
        }
        
        /*if (hp < 0)
        {
            



        }*/
    }
    public IEnumerator<float> FadeAway()
    {
        //yield return new WaitForSeconds(1);
        while (_spriteRenderer.color.a > 0)
        {
            var color = _spriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (0.3f * Time.deltaTime);

            _spriteRenderer.color = color;
            //wait for a frame
            yield return Timing.WaitForOneFrame;
        }

        ObjPuller.instance.objectPoolList[3].Enqueue(mySelf);        
        mySelf.SetActive(false);
    }


}

