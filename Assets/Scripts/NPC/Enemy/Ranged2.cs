using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged2 : Enemy
{
    //public GameObject lineTPM;
    //public Transform linePos;
    
    [SerializeField]
    public GameObject newBullets;
    [SerializeField]
    public float intervalSec;

    
    //string lineText;
    //[SerializeField]
    //public string lineText1;
    //[SerializeField]
    //public string lineText2;
    //[SerializeField]
    //public string lineText3;
    //[SerializeField]
    //public string lineText4;
    //[SerializeField]
    //public string dyingMessege;
    // Start is called before the first frame update
    void Start()
    {
        //  theScore = FindObjectOfType<ScoreManager>();
        base.Targetting();
        //SeletLine();
        SeletLine();
        targetBeacon.SetActive(true);

    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberB;
        killcount++;
        StatusManager.instance.killNumberB = killcount;
    }

    //void SeletLine()
    //{
    //    int dice=Random.Range(0, 3);
    //    if (dice == 0)
    //        lineText = lineText1;
    //    if (dice == 1)
    //        lineText = lineText2;
    //    if (dice == 2)
    //        lineText = lineText3;
    //    if (dice == 3)
    //        lineText = lineText4;
    //}
    //void Lines()
    //{
    //    SeletLine();
    //    GameObject linesText = Instantiate(lineTPM); // 생성할 텍스트 오브젝트
    //    linesText.transform.position = hudPos.position; // 표시될 위치
    //    linesText.GetComponent<EnemyLines>().lines = lineText;
    //}




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




            //if (!live)
            //{
            //    CancelInvoke();
            //}
        }
    }

    private void Attack()
    {
        canAttack = false;
        canMove = true;
        //InvokeRepeating("Attack", 0.1f, base.attackSpeed);
        if (canTryAttack)
        {
            canTryAttack = false;
            base.anim.SetTrigger("attack");
            Invoke("FireBullet", 0.3f);
            Invoke("LetAttack", attackSpeed);
        }

    }

    void LetAttack()
    {
        canTryAttack = true;
        canAttack = true;
    }

    /*private void Attack()
    {
        InvokeRepeating("CreateBullets", intervalSec, intervalSec);
    }*/

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
        GameObject newGameObject = ObjPuller.instance.objectPoolList[2].Dequeue();
        
        newGameObject.transform.position = this.transform.position;
        newGameObject.SetActive(true);
    }

}

