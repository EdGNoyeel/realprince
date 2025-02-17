using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    //[SerializeField]
    public float hp;
    [SerializeField]
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
    [SerializeField]
    private GameObject layer4;
    public bool canHeal;
    // Start is called before the first frame update
    void Start()
    {
        hp = hpLimit;
        canHeal = false;
        theDamegeBox = GetComponentInChildren<Collider2D>();
        portrait = GameObject.Find("Portrait_img");
        //layer1 = GetComponentInChildren<Sprite>();
        //layer2 = GetComponentInChildren<Sprite>();
        //layer3 = GetComponentInChildren<Sprite>();
        //layer4 = GetComponentInChildren<Sprite>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            //Debug.Log("맞음");
            //DecreseHP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void DecreseHP()
    //{
    //    hp -= 1;
    //    Debug.Log("이빨체력" + hp);
    //    if (hp <= 0)
    //    {
    //        ScoreManager.instance.DecreaseToothCount();
    //        Destroy(gameObject);
    //    }
    //}

    public void OnDamage(float damage)
    {        
        hp -= damage;
        //Debug.Log(damage);
        canHeal = true;
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = this.transform.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = (int)damage; // 데미지 전달
        if (hp < hpLimit*0.75)
        {
            if (hp > hpLimit * 0.5)
            {
                layer1.gameObject.SetActive(false);
                layer2.gameObject.SetActive(true);
            }
            else
            {
                if (hp > hpLimit* 0.25)
                {
                    layer2.gameObject.SetActive(false);
                    layer3.gameObject.SetActive(true);
                }
                else
                {
                    if (hp <= 0)
                    {
                        ScoreManager.instance.DecreaseToothCount();
                        GameManager.instance.CameraShake();
                        this.gameObject.SetActive(false);
                        portrait.GetComponent<Animator>().SetTrigger("damage");
                        GameObject stage = GameObject.Find("StageManager");
                        //stage.GetComponentInChildren<EnemyCreater>().CalStars();
                        
                    }
                    else
                    {
                        layer3.gameObject.SetActive(false);
                        layer4.gameObject.SetActive(true);
                    }
                }
            }
        }       
    }

    public void Restore(float heal)
    {
        if (canHeal)
        {
            hp += heal;
            if (hp >= hpLimit * 0.25)
            {
                if (hp <= hpLimit*0.5)
                {
                    layer4.gameObject.SetActive(false);
                    layer3.gameObject.SetActive(true);
                }
                else
                {
                    if (hp <= hpLimit * 0.75)
                    {
                        layer4.gameObject.SetActive(false);
                        layer3.gameObject.SetActive(false);
                        layer2.gameObject.SetActive(true);
                    }
                    else
                    {
                        layer4.gameObject.SetActive(false);
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
}
