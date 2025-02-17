using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBAI : MonoBehaviour
{
    public GameObject targetEnemy;
    public List<GameObject> enemy;
    public float shortDis;
    private Vector3 newPos;
    public bool canMove = false;
    public bool toRight = true;
    public float attackRate=0.2f;

    [SerializeField]
    public GameObject toothBrush;
    [SerializeField]
    public GameObject posBeacon;
    public float tbSpeed;
    Vector3 myPos;
    Vector3 enemyPos;
    // Start is called before the first frame update
    void Awake()
    {
        checkDir();
        myPos = posBeacon.transform.position;

        Invoke("Beginnig", 0.1f);
        InvokeRepeating("AiAttack", attackRate, attackRate);
    }

    void Update()
    {
        if (shortDis > 2.5f)
        {
            toothBrush.GetComponent<ToothBrushMove>().BoostOn();
        }
        else
        {
            toothBrush.GetComponent<ToothBrushMove>().BoostDown();
            //Debug.Log("부스터다운");
        }
    }

    public void Beginnig()
    {
        
        Targetting();
        InvokeRepeating("AiAttack", attackRate, attackRate);
    }

    public void Ending()
    {
        CancelInvoke("AiAttack");
        toothBrush.GetComponent<ToothBrushMove>().up = false;
        toothBrush.GetComponent<ToothBrushMove>().down = false;
        toothBrush.GetComponent<ToothBrushMove>().BoostDown();
    }


    public void checkDir()
    {
        tbSpeed = FindObjectOfType<ToothBrushMove>().speed;
    }
    public void Targetting()
    {        
        if (tbSpeed != 0)
        {
            Debug.Log("작동중");
            //newPos = gameObject.transform.position;
            enemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("target"));
            if(enemy.Count !=0)
            {
                shortDis = Vector3.Distance(myPos, enemy[0].transform.position); // 첫번째를 기준으로 잡아주기 


                targetEnemy = enemy[0]; // 첫번째를 먼저 
                enemyPos = targetEnemy.transform.position;

                foreach (GameObject nearest in enemy)
                {
                    float Distance = Vector3.Distance(newPos, nearest.transform.position);

                    if (Distance < shortDis && myPos.x < enemyPos.x) // 위에서 잡은 기준으로 거리 재기
                    {
                        targetEnemy = nearest;
                        shortDis = Distance;
                    }
                }
            }
            
            canMove = true;
        }
        

    }

    void AiAttack()
    {
        Targetting();
        if (targetEnemy != null)
        {
            enemyPos = targetEnemy.transform.position;
            myPos = posBeacon.transform.position;
        }

        //if(targetEnemy != null)
        //{
        if (myPos.y < enemyPos.y)
        {
            //Debug.Log("위로");
            toothBrush.GetComponent<ToothBrushMove>().up = true;
            toothBrush.GetComponent<ToothBrushMove>().down = false;
        }

        if (myPos.y > enemyPos.y)
        {
            //Debug.Log("아래로");
            toothBrush.GetComponent<ToothBrushMove>().up = false;
            toothBrush.GetComponent<ToothBrushMove>().down = true;
        }

        
    }

    // Update is called once per frame
    


}
