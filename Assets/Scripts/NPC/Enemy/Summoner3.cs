using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner3 : Enemy
{    
    [SerializeField]
    public GameObject newBullets1;
    [SerializeField]
    public GameObject newBullets2;
    [SerializeField]
    public GameObject newBullets3;
    [SerializeField]
    public float intervalSec;
    float shortDis1 = 20f;
    float shortDis2 = 20f;

    public GameObject targetTooth1;
    public GameObject targetTooth2;
    public GameObject targetTooth3;
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        //MultiTargetting();
        SeletLine();
        targetBeacon.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (targetTooth != null)
        {
            if (base.targetTooth.activeSelf == true)
            {
                if (base.canMove)
                {
                    base.Move();
                    //Debug.Log("이동중");
                }
                else
                {
                    if (canAttack)
                    {
                        MultiTargetting();
                        Attack();
                        canAttack = false;
                    }

                }
            }
            else
            {
                CancelInvoke("CreateBullets");
                base.Targetting();
                base.canMove = true;
                canAttack = true;
            }


        }
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberC;
        killcount++;
        StatusManager.instance.killNumberC = killcount;
    }

    void MultiTargetting()
    {
        newPos = gameObject.transform.position;
        tooth = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tooth"));
        shortDis = Vector3.Distance(newPos, tooth[0].transform.position); // 첫번째를 기준으로 잡아주기 
        if (tooth.Count != 0)
            targetTooth1 = tooth[0]; // 첫번째를 먼저 
        if (tooth.Count > 0)
            targetTooth2 = tooth[1];
        if (tooth.Count > 1)
            targetTooth3 = tooth[2];

        if(tooth[0] != null)
        {
            foreach (GameObject nearest in tooth)
            {
                float Distance = Vector3.Distance(newPos, nearest.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    targetTooth1 = nearest;
                    shortDis = Distance;
                }
            }
        }

        if (tooth[1] != null)
        {
            foreach (GameObject nearest1 in tooth)
            {

                float Distance1 = Vector3.Distance(newPos, nearest1.transform.position);

                if (Distance1 > shortDis && Distance1 < shortDis1) // 위에서 잡은 기준으로 거리 재기
                {
                    //Debug.Log("여기까지옴");
                    if (targetTooth1 != nearest1)
                    {
                        targetTooth2 = nearest1;
                        shortDis1 = Distance1;
                    }

                }
            }

        }
        if(tooth[1] != null)
        {
            foreach (GameObject nearest2 in tooth)
            {

                float Distance2 = Vector3.Distance(newPos, nearest2.transform.position);

                if (Distance2 > shortDis1 && Distance2 < shortDis2) // 위에서 잡은 기준으로 거리 재기
                {
                    //Debug.Log("여기까지옴");
                    if (targetTooth2 != nearest2)
                    {
                        targetTooth3 = nearest2;
                        shortDis2 = Distance2;
                    }

                }
            }
        }
        
    }

    private void Attack()
    {
        InvokeRepeating("CreateBullets", intervalSec, intervalSec);
    }

    void CreateBullets()
    {

        if (base.live)
        {
            base.anim.SetTrigger("rangeattack");
            Invoke("FireBullet", 0.5f);

        }
        else
            CancelInvoke("CreateBullets");
    }

    void FireBullet()
    {
        MultiTargetting();
        GameObject newGameObject1 = ObjPuller.instance.objectPoolList[3].Dequeue();
        
        newGameObject1.transform.position = targetTooth1.transform.position;
        newGameObject1.SetActive(true);
        newGameObject1.GetComponent<C_Bullet>().TryAttack(targetTooth1);
        GameObject newGameObject2 = ObjPuller.instance.objectPoolList[3].Dequeue();
        
        newGameObject2.transform.position = targetTooth2.transform.position;
        newGameObject2.SetActive(true);
        newGameObject2.GetComponent<C_Bullet>().TryAttack(targetTooth2);
        GameObject newGameObject3 = ObjPuller.instance.objectPoolList[3].Dequeue();
        
        newGameObject3.transform.position = targetTooth3.transform.position;
        newGameObject3.SetActive(true);
        newGameObject3.GetComponent<C_Bullet>().TryAttack(targetTooth3);
    }

}
