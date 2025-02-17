using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class ToothBrushMove : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    [SerializeField]
    Rigidbody2D rbody;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float critical;
    [SerializeField]
    public float criticalRate;
    [SerializeField]
    GameObject attackBtn;
    [SerializeField]
    GameObject stopAttackBtn;
    [SerializeField]
    Collider2D myColl;
    [SerializeField]
    private Animator animator;
    float actualDamage;
    

    private GameObject scoreManager;
    public float speed = 3;
    public float originSpeed = 3;
    public float boostRatio = 4;
    public float boostSpeed = 12;
    public float attackSpeed = 10;
    public float attackRate = 0.3f;
    public float manualAttackSpeed = 100;
    private Vector2 originPos;
    public bool up=false;
    public bool down=false;
    bool keepAttack = false;
    
    //private bool upflag=false;

  //  [SerializeField]
   // public Sprite theSprite;
    //[SerializeField]
   // public ToothBrushAttack theAttack;
   // private Collider2D myColl;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed = originSpeed;
        boostSpeed = originSpeed * boostRatio;
        //  theSprite = GetComponent<Sprite>();
        // myColl = GetComponent<Collider2D>();

        TbCheckUpgrade();
        animator.SetBool("Attack", true);

    }

    public void TbCheckUpgrade()
    {
        //damage = float.Parse(GameObject.Find("DamageUp").GetComponent<UpGrade>().currentDamageTxtString);
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = new Vector2(speed, 0);
        //childRbody.velocity = new Vector2(speed, 0);
        if (Input.GetKey("w") || up==true)
        {
            Up();
            
            //    childRbody.velocity = new Vector2(speed, -attackSpeed);
            //   theAttack.TryAttack();
            //   this.GetComponentInChildren<SpriteRenderer>().flipX = upflag;
        }
        if (Input.GetKey("s")|| down==true)
        {
            Down();
            //   childRbody.velocity = new Vector2(speed, attackSpeed);
            //   theAttack.TryAttack();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed = boostSpeed;            
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            speed = originSpeed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TryAttack();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TryStopAttack();
        }


    }

    public void Initialize()
    {
        this.transform.position = new Vector3(10, 2, 0);
        if(speed <= 0)
        {
            speed = -speed;
        }
    }
    
    public void FixedUpdate()
    {        
        Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rbody.AddForce(direction * manualAttackSpeed * Time.fixedDeltaTime, (ForceMode2D)ForceMode.VelocityChange);
        /*if (direction.y > 0)
        {
            animator.SetBool("Up", true);
        }
        if (direction.y <= 0)
        {
            animator.SetBool("Up", false);
        }*/
    }

    public void Down()
    {
        //Debug.Log("down");
        originPos = this.transform.localPosition;
        rbody.linearVelocity = new Vector2(speed, -attackSpeed);
        //animator.SetBool("Up", false);
    }

    public void Up()
    {
        //Debug.Log("up");
        originPos = this.transform.localPosition;
        rbody.linearVelocity = new Vector2(speed, attackSpeed);
        //animator.SetBool("Up", true);
    }

    public void BoostOn()
    {
        speed = boostSpeed;
        //Invoke("BoostDown", 0.1f);
        TryStopAttack();
    }

    public void BoostDown()
    {
        speed = originSpeed;
        TryAttack();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "HitableBullet")
        {
            other.gameObject.GetComponent<C_Bullet>()?.Damage(actualDamage);
            SoundManager.instance.PlaySE("chica");
        }
        
        if (other.transform.tag== "Limit")
        {
            originSpeed = -originSpeed;
            boostSpeed = -boostSpeed;
            speed = -speed;
            //Debug.Log("벽꿍"+speed);
        }
        else if (other.gameObject.tag == "Monster")
        {
            float randomDamage = Random.Range(0.0f , 1.0f);
            if (randomDamage < criticalRate)
                actualDamage = damage * critical;
            else
                actualDamage = damage;
            //Debug.Log("몬스터때림");
            //Debug.Log(other.gameObject.name);
            
            other.gameObject.GetComponent<Enemy>()?.Damage(actualDamage);            
            
            if (other.GetComponent<Enemy>().canHit)
            {
                SoundManager.instance.PlaySE("chica");
            }
            //if (Input.GetKey("w"))
            //{
            //    animator.SetTrigger("Attack_Up");
            //}
            //else
            //    animator.SetTrigger("Attack_Down");
        }
        else if (other.gameObject.tag == "MortalBullet")
        {
            other.gameObject.GetComponent<C_Bullet>()?.Damage(damage);
            SoundManager.instance.PlaySE("chica");
        }
    }

    public void TryAttack()
    {
        
        if (!keepAttack)
        {
            animator.SetBool("Attack", true);
            //attackBtn.SetActive(false);
            //stopAttackBtn.SetActive(true);
            //animator.SetBool("Attack", true);
            /*keepAttack = true;
            attackBtn.SetActive(false);
            stopAttackBtn.SetActive(true);

            InvokeRepeating("Attack", attackRate, attackRate);*/
        }        
    }

    public void TryStopAttack()
    {
        animator.SetBool("Attack", false);
        //attackBtn.SetActive(true);
        //stopAttackBtn.SetActive(false);
        /*CancelInvoke("Attack");
        StopAttack();
        attackBtn.SetActive(true);
        stopAttackBtn.SetActive(false);*/
    }

    void Attack()
    {
        keepAttack = true;
        if (!up)
        {
            Debug.Log("위공격");
            up = true;
            down = false;
        }
        else
        {
            Debug.Log("아래공격");
            up = false;
            down = true;
        }
    }

    void StopAttack()
    {
        up = false;
        down = false;
        keepAttack = false;
    }


}
