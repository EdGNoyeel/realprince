using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGPBullet : MonoBehaviour
{
    Vector3 newPos;
    List<GameObject> enemy;
    GameObject target;
    float shortDis;
    bool canMove = true;
    [SerializeField]
    Rigidbody2D rbody;
    public float speed;
    public float range;
    public float damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        Targetting();
    }

    public void Targetting()
    {
        newPos = gameObject.transform.position;
        enemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        if (enemy.Count != 0)
        {
            shortDis = Vector3.Distance(newPos, enemy[0].transform.position); // 첫번째를 기준으로 잡아주기 

            target = enemy[0]; // 첫번째를 먼저 

            foreach (GameObject nearest in enemy)
            {
                float Distance = Vector3.Distance(newPos, nearest.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    if (nearest.GetComponent<Enemy>().canHit == true)
                    {
                        target = nearest;
                        shortDis = Distance;
                    }
                    
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Move()
    {
        if (Vector2.Distance(target.transform.position, gameObject.transform.position) >= range)
        {
            Vector3 dir = (target.transform.position - gameObject.transform.position).normalized;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //Debug.Log("명중");
            if (other.gameObject.GetComponent<Enemy>().canHit)
            {
                other.gameObject.GetComponent<Enemy>()?.Damage(damage);
            }
            
            //GameObject exEf = Instantiate(effectPrefab);
            //exEf.transform.position = other.transform.position;
            if (target != null)
                Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (target != null &&target.activeSelf==true && target.GetComponent<Enemy>().canHit==true)
        {
            if (canMove)
            {
                Move();
                //Debug.Log("이동중");
            }
        }
        else
        {
            if (enemy.Count == 0)
            {
                Destroy(gameObject);
            }
            else
                Targetting();
        }
            
    }
}
