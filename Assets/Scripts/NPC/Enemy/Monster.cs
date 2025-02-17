using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] protected string monsterName;
    [SerializeField] protected int hp;
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    

    protected Vector3 destination;

    protected bool isWalking;
    protected bool isAction;
    protected bool isRunnung;
    protected bool isDead;

    [SerializeField] protected float walkTime;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float runTime;
    protected float currenTime;

    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxcol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;

    [SerializeField] protected AudioClip[] sound_Normal;
    [SerializeField] protected AudioClip sound_Hurt;
    [SerializeField] protected AudioClip sound_Dead;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currenTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();            
            ElapseTime();
        }

    }

    protected void Move()
    {
        if (isWalking || isRunnung)
            // rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            nav.SetDestination(transform.position + destination * 5f);
    }



    protected void ElapseTime()
    {
        if (isAction)
        {
            currenTime -= Time.deltaTime;
            if (currenTime <= 0)
                ReSet();

        }
    }

    protected virtual void ReSet()
    {
        isWalking = false; isRunnung = false; isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking); anim.SetBool("Running", isRunnung);
        destination.Set(Random.Range(0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
        
    }




    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currenTime = walkTime;
        nav.speed = walkSpeed;
        //Debug.Log("걷기");
    }

    

    public virtual void Damage(int _dmg, Vector2 _targetPos)
    {
        if (!isDead)
        {
            hp -= +_dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(sound_Hurt);
            anim.SetTrigger("Hurt");            
        }

    }

    protected void Dead()
    {
        PlaySE(sound_Dead);
        isWalking = false;
        isRunnung = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(sound_Normal[_random]);
    }
    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
