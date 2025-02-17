using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGPFairy : MonoBehaviour
{
    public string upgradeCost;
    [SerializeField]
    GameObject[] _posObj;
    int destNumb;
    [SerializeField]
    CircleCollider2D myCol;
    public float speed = 0.02f;
    public int bulletNumb = 1;
    public float bulletSpeed=3;
    [SerializeField]
    GameObject[] firePos;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Rigidbody2D rbody;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject[] upgradeBTN;
    float damage;
    public float damageMultiply=2;
    string upGrade;

    // Start is called before the first frame update
    void Start()
    {
        destNumb = 0;
        upGrade = StatusManager.instance.fairySkillSGP;
        SetButtons();
        CheckUpGrade();
    }

    void OnEnable()
    {
        destNumb = 0;
        upGrade = StatusManager.instance.fairySkillSGP;
        SetButtons();
        CheckUpGrade();
        anim.SetTrigger("idle");
    }

    void SetButtons()
    {
        //Debug.Log("사계팔버튼세팅");
        string[] arr = StatusManager.instance.fairySkillSGP.Split(new char[] { ',' });

        for (int j = 0; j < upgradeBTN.Length; j++)
        {
            Image[] images = upgradeBTN[j].GetComponentsInChildren<Image>();
            upgradeBTN[j].GetComponent<Button>().interactable = false;
            images[images.Length - 1].enabled = false;
        }

        for (int k = 0; k < upgradeBTN.Length-1; k++)
        {
            if (arr[k] == "1")
            {
                upgradeBTN[k+1].GetComponent<Button>().interactable = true;                
            }
        }

        upgradeBTN[0].GetComponent<Button>().interactable = true;


        for (int i = 0; i < upgradeBTN.Length; i++)
        {
            if (arr[i] == "1")
            {
                upgradeBTN[i].GetComponent<Button>().interactable = false;
                Image[] images = upgradeBTN[i].GetComponentsInChildren<Image>();
                images[images.Length-1].enabled = true;
            }
        }
    }

    void CheckUpGrade()
    {
        string[] arr = upGrade.Split(',');

        if (arr[0] == "1")
        {
            GameManager.instance.UnlockAvatar(6, "SGPU");
        }
        else
        {
            //damageMultiply = 1;
        }

        if (arr[1] == "1")
        {
            bulletSpeed = 4;
        }
        else
        {
            bulletSpeed = 3;
        }

        if (arr[2] == "1")
        {
            speed = 2f;
        }
        else
        {
            speed = 1f;
        }

        if (arr[3] == "1")
        {
            bulletNumb = 3;
        }
        else
        {
            bulletNumb = 1;
        }

        if (arr[4] == "1")
        {
            damageMultiply = 4;
        }
        else
        {
            damageMultiply = 2;
        }

        if (arr[5] == "1")
        {
            speed = 3f;
        }
        else
        {

        }

        if (arr[6] == "1")
        {
            bulletNumb = 4;
        }
        else
        {
            bulletNumb = 3;
        }

        if (arr[7] == "1")
        {

        }
        else
        {

        }

        if (arr[8] == "1")
        {

        }
        else
        {

        }

    }

    public void UpGradeSkill(int numb)
    {
        string[] arr2 = upgradeCost.Split(',');
        int diaCost = int.Parse(arr2[numb]);
        if (diaCost <= StatusManager.instance.dia)
        {
            StatusManager.instance.dia = StatusManager.instance.dia - diaCost;

            upGrade = StatusManager.instance.fairySkillSGP;
            string[] arr = upGrade.Split(',');

            arr[numb] = "1";

            upGrade = string.Join(",", arr);
            StatusManager.instance.fairySkillSGP = upGrade;
            CheckUpGrade();
            SetButtons();
        }

        if(diaCost >= 1000000)
        {
            if(diaCost <= StatusManager.instance.score)
            {
                StatusManager.instance.score = StatusManager.instance.score - diaCost;

                upGrade = StatusManager.instance.fairySkillSGP;
                string[] arr = upGrade.Split(',');

                arr[numb] = "1";

                upGrade = string.Join(",", arr);
                StatusManager.instance.fairySkillSGP = upGrade;
                CheckUpGrade();
                SetButtons();
            }
        }




        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject== _posObj[0])
        {
            destNumb=1;
            Fire();
        }

        if (other.gameObject == _posObj[1])
        {
            destNumb = 2;
            Fire();
        }

        if (other.gameObject == _posObj[2])
        {
            destNumb = 3;
            Fire();
        }

        if (other.gameObject == _posObj[3])
        {
            destNumb = 0;
            Fire();
        }

    }

    void Fire()
    {
        anim.SetTrigger("fire");
        GameObject newGameObject1 = ObjPuller.instance.objectPoolList[10].Dequeue();

        newGameObject1.transform.position = gameObject.transform.position;
        newGameObject1.SetActive(true);

        for (int i = 0; i < bulletNumb; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = firePos[i].transform.position;
            bullet.GetComponent<SGPBullet>().damage = damage * damageMultiply;
            bullet.GetComponent<SGPBullet>().speed = bulletSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _posObj[destNumb].transform.position, speed);
        Move();
        damage = GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().damage;
    }

    public void Move()
    {
        if (Vector2.Distance(_posObj[destNumb].transform.position, gameObject.transform.position) >= 0.0001)
        {
            Vector3 dir = (_posObj[destNumb].transform.position - gameObject.transform.position).normalized;
            float vx = dir.x * speed;
            float vy = dir.y * speed;
            rbody.linearVelocity = new Vector2(vx, vy);
            GetComponentInChildren<SpriteRenderer>().flipX = (vx < 0);
        }
        else
        {
            /*canMove = false;
            rbody.velocity = new Vector2(0, 0);
            if (canAttack)
            {
                Attack();
            }*/
        }

    }
}
