using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    [SerializeField]
    GameObject[] pos;
    GameObject actualPos;
    Animator anim;
    float range = 0.01f;
    public bool canMove=false;
    public bool moveAnim=false;
    bool flip = false;

    float time;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("idle");
        //time = Random.Range(0, 5);
        //canFlip = true;
        SetPos();

    }

    void Update()
    {
        if (canMove)
        {
            //Debug.Log(Vector2.Distance(targetPos.transform.position, gameObject.transform.position));
            //Move();
            if (Vector2.Distance(actualPos.transform.position, this.gameObject.transform.position) >= range)
            {
                transform.position = Vector2.MoveTowards(transform.position, actualPos.transform.position, 0.01f);
                /*Debug.Log(Vector2.Distance(targetPos.transform.position, gameObject.transform.position));
                Vector2 dir = (targetPos.transform.position - gameObject.transform.position).normalized;
                float vx = dir.x * speed;
                float vy = dir.y * speed;
                rbody.velocity = new Vector2(vx, vy);
                GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);*/
                //GetComponentInChildren<Image>().flipX = (vx < 0)
                /*if (canFlip)
                {
                    
                    canFlip = false;
                }*/
                
                //anim.SetBool("move", true);
            }
            else
            {
                //Debug.Log("오픈!");
                canMove = false;
                //canAttack = true;
                //rbody.velocity = new Vector2(0, 0);
                //anim.SetTrigger("idle");
                //anim.SetBool("move", false);
                //GameManager.instance.GameStarter();
                SetPos();
            }

            if (moveAnim)
            {
                anim.SetTrigger("walk");
                moveAnim = false;
            }

        }
        
    }

    void SetPos()
    {
        int numb = Random.Range(0, pos.Length);
        for (int i = 0; i < pos.Length; i++)
        {
            
            if (i == numb)
            {
                actualPos = pos[i];
            }
            

            
        }
        //Debug.Log(actualPos);


        if (this.transform.position.x< actualPos.transform.position.x)
        {
            if (!flip)
            {
                this.transform.localScale += new Vector3(-2f, 0f, 0f);
                flip = true;
            }
            
        }
        else
        {
            if (flip)
            {
                this.transform.localScale += new Vector3(2f, 0f, 0f);
                flip = false;
            }
        }
        Action();

    }

    void Action()
    {
        
        int numb = Random.Range(0, 2);
        if (numb == 0)
        {
            anim.SetTrigger("idle");
            Invoke("ReSet", 1);
            //Debug.Log("액션0");
            StartCoroutine(ReSetCou(1.5f));
        }
        else
        {
            anim.SetTrigger("bark");
            SoundManager.instance.PlaySE("bark");
            Invoke("ReSet", 0.5f);
            //Debug.Log("액션1");
            StartCoroutine(ReSetCou(0.5f));
            
        }
    }
    void ReSet()
    {
        
    }

    IEnumerator ReSetCou(float numb)
    {
        yield return new WaitForSecondsRealtime(numb);
        canMove = true;
        moveAnim = true;
        //Debug.Log("리셋");

    }


}
