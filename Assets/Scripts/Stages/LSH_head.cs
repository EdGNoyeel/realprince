using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LSH_head : MonoBehaviour
{
    Animator anim;
    public GameObject targetPos;
    public float speed;
    Rigidbody2D rbody;
    public float range = 0.1f;
    public bool canMove = true;
    public bool moveAnim = true;
    public bool justMove = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        //rbody = GetComponent<Rigidbody2D>();
        targetPos = GameObject.Find("TargetPos");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(targetPos.transform.position, gameObject.transform.position));
        if (canMove)
        {
            //Debug.Log(Vector2.Distance(targetPos.transform.position, gameObject.transform.position));
            //Move();
            if (Vector2.Distance(targetPos.transform.position, gameObject.transform.position) >= range)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos.transform.position, 0.1f);
                /*Debug.Log(Vector2.Distance(targetPos.transform.position, gameObject.transform.position));
                Vector2 dir = (targetPos.transform.position - gameObject.transform.position).normalized;
                float vx = dir.x * speed;
                float vy = dir.y * speed;
                rbody.velocity = new Vector2(vx, vy);
                GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);*/
                anim.SetBool("move", true);
            }
            else
            {
                //Debug.Log("오픈!");
                canMove = false;
                if(StatusManager.instance.currentStory != "0")
                {
                    Debug.Log(StatusManager.instance.currentStory);
                    StoryManager.instance.RunStory(StatusManager.instance.currentStory);
                }
                //canAttack = true;
                //rbody.velocity = new Vector2(0, 0);
                //anim.SetTrigger("idle");
                anim.SetBool("move", false);
                if (!justMove)
                {
                    GameManager.instance.GameStarter();
                }
                

            }

            if (moveAnim)
            {
                //anim.SetTrigger("walk");
                moveAnim = false;
            }

        }
        else
            anim.SetBool("move", false);
    }

    public void SetTarget(Vector3 _pos)
    {
        targetPos.transform.position = _pos;
        canMove = true;
    }

    public void Move()
    {
        if (Vector2.Distance(targetPos.transform.position, gameObject.transform.position) >= range)
        {
            Vector2 dir = (targetPos.transform.position - gameObject.transform.position).normalized;
            float vx = dir.x * speed;
            float vy = dir.y * speed;
            rbody.linearVelocity = new Vector2(vx, vy);
            GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);
        }
        else
        {
            //Debug.Log("오픈!");
            canMove = false;
            //canAttack = true;
            rbody.linearVelocity = new Vector2(0, 0);
            anim.SetTrigger("idle");
            GameManager.instance.GameStarter();

        }

        if (moveAnim)
        {
            anim.SetTrigger("walk");
            moveAnim = false;
        }
        
    }

}
