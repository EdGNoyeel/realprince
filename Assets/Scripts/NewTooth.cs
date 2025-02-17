using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class NewTooth : MonoBehaviour
{
    public float hp;    
    public float hpLimit;
    public GameObject portrait;

    public GameObject hudDamageText;
    public Transform hudPos;

    [SerializeField]
    public Collider2D theDamegeBox;
    [SerializeField]
    private GameObject layer1;
    [SerializeField]
    private GameObject layer2;
    [SerializeField]
    private GameObject layer3;    
    public bool canHeal=false;
    bool canHit;

    [SerializeField]
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        hp = hpLimit;
        canHeal = false;
        theDamegeBox = GetComponent<Collider2D>();
        portrait = GameObject.Find("Portrait_img");
        //anim = GetComponentInChildren<Animator>();
        canHit = true;
        Blink();
        
    }

    void OnEnable()
    {
        canHeal = false;
        canHit = true;
    }

    void Blink()
    {
        float a=Random.Range(1f, 3f);
        Invoke("ActionBlink", a);
    }

    void ActionBlink()
    {
        anim.SetTrigger("blink");
        Blink();
    }

    

    public void OnDamage(float damage)
    {
        anim.SetTrigger("damage");
        hp -= damage;
        //Debug.Log(damage);
        canHeal = true;
        GameObject hudText = ObjPuller.instance.objectPoolList[1].Dequeue();
        hudText.SetActive(true);
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = (int)damage; // 데미지 전달
        //hudText.transform.SetParent(this.transform, true);
        //StartCoroutine(ObjReturn(hudText));
        Timing.RunCoroutine(ObjReturn(hudText).CancelWith(gameObject));
        if (hp < hpLimit * 0.66)
        {

            anim.SetTrigger("getworse");
            //Debug.Log("이빨아파");
            if (hp > hpLimit * 0.33)
            {
                
                layer1.gameObject.SetActive(false);
                layer2.gameObject.SetActive(true);
            }
            else
            {
                layer2.gameObject.SetActive(false);
                layer3.gameObject.SetActive(true);
                anim.SetTrigger("getworst");


                if (hp <= 0)                    
                {
                    if (canHit)
                    {
                        ScoreManager.instance.DecreaseToothCount();
                    }
                    canHit = false;
                    GameManager.instance.CameraShake();
                    this.gameObject.SetActive(false);
                    portrait.GetComponent<Animator>().SetTrigger("damage");
                    GameObject stage = GameObject.Find("StageManager");
                    //stage.GetComponentInChildren<EnemyCreater>().CalStars();


                }
                    
            }
        }
    }

    public IEnumerator<float> ObjReturn(GameObject _obj)
    {
        yield return Timing.WaitForSeconds(2);
        ObjPuller.instance.objectPoolList[1].Enqueue(_obj);
        _obj.SetActive(false);
    }

    /*IEnumerator ObjReturn(GameObject _obj)
    {
        yield return new WaitForSeconds(2);
        ObjPuller.instance.objectPoolList[1].Enqueue(_obj);
        _obj.SetActive(false);
    }*/

    public void Restore(float heal)
    {
        if (canHeal)
        {
            hp += heal;
            if (hp >= hpLimit * 0.33)
            {
                anim.SetTrigger("getgood");
                if (hp <= hpLimit * 0.66)
                {
                    layer3.gameObject.SetActive(false);
                    layer2.gameObject.SetActive(true);
                }
                else
                {
                    anim.SetTrigger("getbetter");
                    layer3.gameObject.SetActive(false);
                    layer2.gameObject.SetActive(false);
                    layer1.gameObject.SetActive(true);

                    if (hp >= hpLimit)
                    {
                        hp = hpLimit;
                        canHeal = false;
                    }
                }
            }


        }

    }
}
