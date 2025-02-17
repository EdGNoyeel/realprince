using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MEC;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject EffectPrefab;
    string lineText;
    public GameObject hpBar;
    public GameObject lineTPM;
    public Transform linePos;
    [SerializeField]
    public string lineText1;
    [SerializeField]
    public string lineText2;
    [SerializeField]
    public string lineText3;
    [SerializeField]
    public string lineText4;
    [SerializeField]
    public string dyingMessege;
    public GameObject hudDamageText;
    public Transform hudPos;
    [SerializeField]
    protected string enemyName;
    [SerializeField]
    public float hp;
    [SerializeField]
    public float hpLimit;

    public float originHp;
    public int difficulty=1;

    [SerializeField]
    protected float range;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    public float damage = 1;
    [SerializeField]
    protected int getScore;    
    [SerializeField] protected GameObject addOn;

    GameObject lineTextTMP;
    
    protected SpriteRenderer _spriteRenderer;
    public bool canHit;
    protected Vector3 newPos;
    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected GameObject targetBeacon;
    public bool canAttack = true;
    public bool canTryAttack = true;

    [SerializeField]
    protected float speed;
    float speedSave;

    public GameObject targetTooth;
    public List<GameObject> tooth;
    public float shortDis;

    public bool canMove = false;
    public bool live = true;
    [SerializeField]
    protected Rigidbody2D rbody;
    bool canPFflib;

    public GameObject hpBarSp;
    float hpBarMax;
    bool canTalk;

    void OnEnable()
    {
        int stageDifficulty = GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().totalDifficulty;
        //Debug.Log(stageDifficulty);
        hpLimit = originHp;
        for (int i = 0; i < difficulty+stageDifficulty; i++)
        {
            hpLimit = (float)(hpLimit * 1.2);
         
        }
        if(addOn!=null)
            addOn.gameObject.SetActive(false);

        hp = hpLimit;
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;        
        canMove = true;
        canTryAttack = true;
        
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        canHit = true;
        var color = _spriteRenderer.color;
        color.a = 1;

        _spriteRenderer.color = color;
        targetTooth = null;
        live = true;
        anim.SetTrigger("idle");
        //SeletLine();
        //InvokeRepeating("Lines", 1, 3);
        targetBeacon.SetActive(true);
        canPFflib=true;
        canTalk = true;
        Targetting();
        //SeletLine();

    }

    public void SeletLine()
    {
        int dice = Random.Range(0, 4);
        if (dice == 0)
            lineText = lineText1;
        if (dice == 1)
            lineText = lineText2;
        if (dice == 2)
            lineText = lineText3;
        if (dice == 3)
            lineText = lineText4;
        float delay = Random.Range(3f, 4f);
        Invoke("Lines", delay);
    }
    public void Lines()
    {
        if (canTalk)
        {
            lineTextTMP = ObjPuller.instance.objectPoolList[9].Dequeue();

            //newGameObject1.transform.position = targetTooth.transform.position;
            if (lineTextTMP != null)
            {
                lineTextTMP.SetActive(true);
                //lineTextTMP.transform.position = gameObject.transform.position; // 표시될 위치
                //lineTextTMP.GetComponent<EnemyLines>().lines = lineText;
                lineTextTMP.GetComponent<TextMeshPro>().text = lineText;
                lineTextTMP.transform.SetParent(this.transform, false);
                //lineTextTMP.transform.position = new Vector3(0, 0.5f, 0);
            }

        }
        
        
        SeletLine();
    }

    public void Targetting()
    {
        newPos = gameObject.transform.position;
        tooth = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tooth"));
        if (tooth.Count != 0)
        {
            shortDis = Vector3.Distance(newPos, tooth[0].transform.position); // 첫번째를 기준으로 잡아주기 

            targetTooth = tooth[0]; // 첫번째를 먼저 

            foreach (GameObject nearest in tooth)
            {
                float Distance = Vector3.Distance(newPos, nearest.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    targetTooth = nearest;
                    shortDis = Distance;
                }
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void Move()
    {
        if (Vector2.Distance(targetTooth.transform.position, gameObject.transform.position) >= range)
        {
            Vector3 dir = (targetTooth.transform.position - gameObject.transform.position).normalized;
            float vx = dir.x * speed;
            float vy = dir.y * speed;
            rbody.linearVelocity = new Vector2(vx, vy);
            GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);
            canAttack = false;
            canMove = true;
            
            


        }
        else
        {
            canAttack = true;
            canMove = false;
            //canAttack = true;
            rbody.linearVelocity = new Vector2(0, 0);
            if (canPFflib && EffectPrefab !=null)
            {
                float pfx = EffectPrefab.transform.localScale.x;
                float pfy = EffectPrefab.transform.localScale.y;
                float pfz = EffectPrefab.transform.localScale.z;
                EffectPrefab.transform.localScale = new Vector3(-pfx, pfy, pfz);
                canPFflib = false;
            }

        }

    }

    public virtual void KillCount()
    {
        int killcount = StatusManager.instance.killNumberF;
        killcount++;
        StatusManager.instance.killNumberF = killcount;
    }

    public virtual void Damage(float damage)
    {
        if (canHit)
        {
            hp -= damage;
            //GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
            GameObject hudText = ObjPuller.instance.objectPoolList[0].Dequeue();
            hudText.SetActive(true);
            hudText.transform.position = hudPos.position; // 표시될 위치
            hudText.GetComponent<DamageText>().damage = (int)damage; // 데미지 전달
            //hudText.transform.SetParent(this.transform, true);
            hudText.transform.SetParent(ObjPuller.instance.transform, true);
            //StartCoroutine(ObjReturn(hudText));
            Timing.RunCoroutine(ObjReturn(hudText).CancelWith(gameObject));

            //Invoke("EnQDamageText")
            if (hp > 0)
            {
                if(live)
                    anim.SetTrigger("damaged");
                hpBarSp.SetActive(true);
                hpBarSp.transform.localScale = new Vector3(hp / hpLimit, hpBarSp.transform.localScale.y, hpBarSp.transform.localScale.z);
            }
            else
            {
                hpBarSp.SetActive(false);
                if (addOn != null)
                    addOn.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("diring");
                //canMove = false;
                canHit = false;
                canTalk = false;
                ScoreManager.instance.IncreaseScore(getScore);
                live = false;
                speedSave = speed;
                speed = 0.00001f;

                anim.SetTrigger("death");
                //Timing.RunCoroutine(FadeAway());
                Timing.RunCoroutine(FadeAway().CancelWith(gameObject));
                CancelInvoke();
                KillCount();
                targetBeacon.SetActive(false);
            }
        }   
    }

    public IEnumerator<float> ObjReturn(GameObject _obj) 
    { 
        yield return Timing.WaitForSeconds(2); 
        ObjPuller.instance.objectPoolList[0].Enqueue(_obj); 
        _obj.SetActive(false); 
    }

    


    void EnQDamageText(GameObject _obj)
    {
        ObjPuller.instance.objectPoolList[0].Enqueue(_obj); 
        _obj.SetActive(false);
    
    }

    public virtual void DamageByNPC(float damage)
    {
        if (canHit)
        {
            hp -= damage;
            GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
            hudText.transform.position = hudPos.position; // 표시될 위치
            hudText.GetComponent<DamageText>().damage = (int)damage; // 데미지 전달
            hudText.transform.SetParent(this.transform, true);

            if (hp > 0)
            {
                if (live)
                    anim.SetTrigger("damaged");
                hpBarSp.SetActive(true);
                hpBarSp.transform.localScale = new Vector3(hp / hpLimit, hpBarSp.transform.localScale.y, hpBarSp.transform.localScale.z);
            }
            else
            {
                hpBarSp.SetActive(false);
                if (addOn != null)
                    addOn.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("diring");
                //canMove = false;
                canHit = false;
                //ScoreManager.instance.IncreaseScore(getScore);
                live = false;
                speedSave = speed;
                speed = 0.00001f;
                canAttack = false;
                anim.SetTrigger("death");
                //Timing.RunCoroutine(FadeAway());
                Timing.RunCoroutine(FadeAway().CancelWith(gameObject));
                CancelInvoke();
                StatusManager.instance.killedByNPC++;
                targetBeacon.SetActive(false);
            }
        }
    }




    public IEnumerator<float> FadeAway()
    {
        if (lineTextTMP != null)
        {
            lineTextTMP.transform.SetParent(GameObject.Find("ObjPuller").transform, false);
            ObjPuller.instance.objectPoolList[9].Enqueue(lineTextTMP);
            lineTextTMP.SetActive(false);
        }
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
        speed = speedSave;
        
        
        EnemyCreater.ReturnObject(this);
    }

    
}
