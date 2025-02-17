using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    public GameObject mySelf;
    [SerializeField]
    protected string BulletName;

    [SerializeField]
    public GameObject effectPrefab;
        
    [SerializeField]
    protected float range;
    [SerializeField]
    protected Collider2D theDamegeBox;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float damage;

    public GameObject targetTooth;
    public List<GameObject> tooth;
    public float shortDis;
    public int myNumb;
    public bool canMove = false;
    [SerializeField]
    protected Rigidbody2D rbody;
    // Start is called before the first frame update
    /*void OnEnable()
    {
    
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        theDamegeBox = GetComponentInChildren<Collider2D>();
        canMove = true;    
    }*/

    public void Targetting()
    {
        tooth = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tooth"));
        shortDis = Vector3.Distance(gameObject.transform.position, tooth[0].transform.position); // 첫번째를 기준으로 잡아주기 

        targetTooth = tooth[0]; // 첫번째를 먼저 

        foreach (GameObject found in tooth)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                targetTooth = found;
                shortDis = Distance;
            }
        }
        //Debug.Log(targetTooth.name);

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Move();
            //Debug.Log("이동중");
        }
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
        }
        else
        {
            canMove = false;
            rbody.linearVelocity = new Vector2(0, 0);
            //Dead();
        }

    }
        
    private void Dead()
    {
        //targetTooth.DecreaseHP();
        //Destroy(gameObject);
        ObjPuller.instance.objectPoolList[myNumb].Enqueue(mySelf);
        //mySelf.transform.SetParent(ObjPuller.instance.transform, false);
        mySelf.SetActive(false);
    }

}
