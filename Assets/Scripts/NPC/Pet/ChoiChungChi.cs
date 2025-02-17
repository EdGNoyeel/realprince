using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class ChoiChungChi : MonoBehaviour
{
    public string upgradeCost;
    [SerializeField]
    GameObject[] upgradeBTN;
    public GameObject hudDamageText;
    public Transform hudPos;
    [SerializeField]
    string petName;
    //[SerializeField]
    //protected Collider2D theDamegeBox;
    [SerializeField]
    float hp;
    [SerializeField]
    float hpLimit;
    [SerializeField]
    float range;
    [SerializeField]
    public float attackSpeed;
    [SerializeField]
    public float originAttackSpeed;
    [SerializeField] protected AudioClip[] sound_Normal;
    [SerializeField] protected AudioClip sound_Hurt;
    [SerializeField] protected AudioClip sound_Dead;
    [SerializeField] protected GameObject addOn;
    [SerializeField] GameObject hitBox;
    AudioSource theAudio;
    SpriteRenderer _spriteRenderer;
    public bool canHit;
    private Vector3 newPos;
    [SerializeField]
    Animator anim;
    public bool canAttack;
    AudioSource yo;
    //오브젝트풀실험

    //ScoreManager theScore;   

    [SerializeField]
    float speed;
    [SerializeField]
    float OriginSpeed;

    public float damageMultiply=1;

    public GameObject targetEnemy;
    public List<GameObject> enemy;
    public float shortDis;
    string upGrade;
    public bool canMove = false;
    public bool live = true;
    [SerializeField]
    Rigidbody2D rbody;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (addOn != null)
            addOn.gameObject.SetActive(false);

        hp = hpLimit;
        
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        theAudio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        upGrade = StatusManager.instance.fairySkillCCC;
        canHit = true;
        var color = _spriteRenderer.color;
        color.a = 1;
        canAttack = true;
        _spriteRenderer.color = color;
        targetEnemy = null;
        live = true;
        anim.SetTrigger("idle");
        Invoke("Beginnig", 3);
        yo = gameObject.GetComponent<AudioSource>();

        upGrade = StatusManager.instance.fairySkillCCC;
        if (this.transform.tag != "Neutral")
        {
            SetButtons();
            CheckUpGrade();
        }

    }

    void Start()
    {
        
        upGrade = StatusManager.instance.fairySkillCCC;
        if(this.transform.tag != "Neutral")
        {
            SetButtons();
            CheckUpGrade();
        }
        
    }

    void SetButtons()
    {
        //Debug.Log("최충치버튼세팅");
        string[] arr = StatusManager.instance.fairySkillCCC.Split(new char[] { ',' });

        for (int j = 0; j < upgradeBTN.Length; j++)
        {
            Image[] images = upgradeBTN[j].GetComponentsInChildren<Image>();
            upgradeBTN[j].GetComponent<Button>().interactable = false;
            images[images.Length - 1].enabled = false;
        }

        for (int k = 0; k < upgradeBTN.Length - 1; k++)
        {
            if (arr[k] == "1")
            {
                upgradeBTN[k + 1].GetComponent<Button>().interactable = true;
            }
        }

        upgradeBTN[0].GetComponent<Button>().interactable = true;


        for (int i = 0; i < upgradeBTN.Length; i++)
        {
            if (arr[i] == "1")
            {
                upgradeBTN[i].GetComponent<Button>().interactable = false;
                Image[] images = upgradeBTN[i].GetComponentsInChildren<Image>();
                images[images.Length - 1].enabled = true;
            }
        }
    }



    void CheckUpGrade()
    {
        string[] arr=upGrade.Split(',');

        if (arr[0] == "1")
        {
            damageMultiply = 1.2f;
        }
        else
        {
            damageMultiply = 1;
        }

        if (arr[1] == "1")
        {
            attackSpeed = originAttackSpeed/1.2f;
        }
        else
        {
            attackSpeed = originAttackSpeed;
        }

        if (arr[2] == "1")
        {
            speed = OriginSpeed * 1.2f;
        }
        else
        {
            speed = OriginSpeed;
        }

        if (arr[3] == "1")
        {
            string[] arr1 = StatusManager.instance.avatarUnlock.Split(new char[] { ',' });
            //Debug.Log(arr1[11]);
            if (arr1[11] == "0")
            {
                GameManager.instance.UnlockAvatar(11, "CCCNOGLASS");
            }
            
        }
        else
        {

        }

        if (arr[4] == "1")
        {
            
            attackSpeed = originAttackSpeed / 2;
        }
        else
        {

        }

        if (arr[5] == "1")
        {
            damageMultiply = 2;
        }
        else
        {

        }

        if (arr[6] == "1")
        {
            speed = OriginSpeed * 1.8f;
        }
        else
        {

        }

        if (arr[7] == "1")
        {
            GameManager.instance.UnlockAvatar(5, "CCCU");
        }
        else
        {

        }

        if (arr[8] == "1")
        {
            hitBox.GetComponent<CircleCollider2D>().radius = 1.5f;
        }
        else
        {

        }

    }

    public void UpGradeSkill(int numb)
    {
        string[] arr2 = upgradeCost.Split(',');
        int diaCost = int.Parse(arr2[numb]);
        if (diaCost <= StatusManager.instance.dia)
        {
            StatusManager.instance.dia = StatusManager.instance.dia - diaCost;

            upGrade = StatusManager.instance.fairySkillCCC;
            string[] arr = upGrade.Split(',');

            arr[numb] = "1";

            upGrade = string.Join(",", arr);
            StatusManager.instance.fairySkillCCC = upGrade;
            CheckUpGrade();
            SetButtons();
        }

        if (diaCost >= 1000000)
        {
            if (diaCost <= StatusManager.instance.score)
            {
                StatusManager.instance.score = StatusManager.instance.score - diaCost;

                upGrade = StatusManager.instance.fairySkillCCC;
                string[] arr = upGrade.Split(',');

                arr[numb] = "1";

                upGrade = string.Join(",", arr);
                StatusManager.instance.fairySkillCCC = upGrade;
                CheckUpGrade();
                SetButtons();
            }
        }
    }

    void Beginnig()
    {
        InvokeRepeating("Targetting", attackSpeed, attackSpeed);
        Targetting();
        canMove = true;

    }
    public void Targetting()
    {
        newPos = gameObject.transform.position;
        enemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("target"));

        if(enemy.Count !=0)
        {
            shortDis = Vector3.Distance(newPos, enemy[0].transform.position); // 첫번째를 기준으로 잡아주기 

            targetEnemy = enemy[0]; // 첫번째를 먼저 

            foreach (GameObject nearest in enemy)
            {
                float Distance = Vector3.Distance(newPos, nearest.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    targetEnemy = nearest;
                    shortDis = Distance;
                }
            }
        }
        
        canMove = true;
        //Debug.Log(targetTooth.name);

    }

    // Update is called once per frame
    void Update()
    {
        if(canMove && targetEnemy !=null)
            Move();
    }

    public void Move()
    {
        if (Vector2.Distance(targetEnemy.transform.position, gameObject.transform.position) >= range)
        {
            Vector3 dir = (targetEnemy.transform.position - gameObject.transform.position).normalized;
            float vx = dir.x * speed;
            float vy = dir.y * speed;
            rbody.linearVelocity = new Vector2(vx, vy);
            GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);
        }
        else
        {
            canMove = false;
            rbody.linearVelocity = new Vector2(0, 0);
            if (canAttack)
            {
                Attack();
            }
        }

    }
    void Attack()
    {
        anim.SetTrigger("attack");
        //Debug.Log("공격");
        hitBox.SetActive(true);
        hitBox.GetComponent<CCCHitBox>().damageMultiply = damageMultiply;
        //hitBox.SetActive(false);
        canAttack = false;
        Invoke("StopAttack", 0.1f);
        Invoke("AttackAgain", attackSpeed);
        //yo.Play();
    }
    void StopAttack()
    {
        hitBox.SetActive(false);
    }
    void AttackAgain()
    {
        canAttack = true;
    }
    
    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }


}
