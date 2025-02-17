using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCreater : MonoBehaviour
{
    public int stageDifficulty=0;
    public int levelDifficulty;
    public int totalDifficulty;
    public int exp;
    [SerializeField]
    public string stageName;
    [SerializeField]
    public int stageNumb;
    public static EnemyCreater Instance;
    //[SerializeField] public GameObject nextBtn;
    [SerializeField] protected GameObject Prefab1;
    [SerializeField] protected GameObject Prefab2;
    [SerializeField] protected GameObject Prefab3;
    [SerializeField] protected GameObject Prefab4;
    [SerializeField] public GameObject stageTitle;
    //[SerializeField] public GameObject stageTitleBtn;
    [TextArea]
    public string story="..";
    public string recomPower = "..";
    public string recomPowerNumb = "..";
    public string vicMent="..";
    Queue<Enemy> poolingObjectQueue = new Queue<Enemy>();
    GameObject poolingObjectPrefab = null;
    [SerializeField] public GameObject bG;
    //[SerializeField]
    //public int prefab1Ratio;
    //[SerializeField]
    //public int prefab2Ratio;
    //승리가능 확인
    public bool canWin = true;

    public bool[] presentMonster;

    int ratio1 = 10;
    int ratio2 = 20;
    int ratio3 = 30;

    //웨이브관련변수
    public GameObject[] waves;    
    int waveNumb = 0;
    public float waveInterval=8f;
    
    int i = 0;
    //승리조건
    public int initialScore;
    public int currentScore;
    public string vicCondi;

    //시간변수
    float _sec;
    int _min;
    public float time_start;
    public float time_current;
    public float time_Max = 60f;
    public bool isEnded;
    public string timer;

    public int initialTooth;    
    public int stars;
    //GameObject starsBoard;


    [SerializeField]
    public float intervalSec;
    public float intervalClone;
    [SerializeField]
    public int enemyCountLimit;
    public GameObject[] enemy;
    public int enemyCount;
    public string currentStory = "0";

    private void Awake() 
    {
        GameObject.Find("FairyManager").GetComponent<FairyManager>().CheckFairy();
        GameManager.instance.canSecondChance = true;
        StatusManager.instance.currentStory = currentStory;
        string[] newArr = null;
        int currentLand = StatusManager.instance.currentLand;
        if (currentLand == 0)
        {
            newArr = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            GameManager.instance.stageNumb[0] = stageNumb;
        }
        if (currentLand == 1)
        {
            newArr = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            GameManager.instance.stageNumb[1] = stageNumb;
        }

        levelDifficulty = int.Parse(newArr[stageNumb]);
        totalDifficulty = stageDifficulty + levelDifficulty;
        bG = GameObject.Find("BackGround_Img");        
        Instance = this; Initialize(100);
        stageTitle = GameObject.Find("StageName_txt");        
        TextMeshProUGUI title = stageTitle.GetComponent<TextMeshProUGUI>();
        title.text = stageName;
        //initialTooth = ScoreManager.instance.remainTooth;
        //stars =initialTooth- ScoreManager.instance.remainTooth;
        //starsBoard = GameObject.Find("Stars");
        //starsBoard.GetComponent<Stars>().Initialize();

        /*stageTitleBtn = GameObject.Find("StagdNumbBtn_txt");
        TextMeshProUGUI titleBtn = stageTitleBtn.GetComponent<TextMeshProUGUI>();
        titleBtn.text = stageName;*/
        GameManager.instance.currentStageName = stageName;

        //CheckStage();
        //Debug.Log("생성시작");
        CountEnemy();
        InvokeRepeating("CreatePrefab", intervalSec, intervalSec);
        InvokeRepeating("Waving", waveInterval, waveInterval);
        ScoreManager.instance.vicMentTxT = vicMent;
        //ScoreManager.instance.updateVicMent();
        GameManager.instance.TBInitialize();
        recomPower = "권장전투력  " + recomPowerNumb;
        Reset_Timer();
        intervalClone = intervalSec;
        //CalStars();
        GameManager.instance.expUp = exp;
        canWin = true;


    }

    /*public void CalStars()
    {
        stars = initialTooth - ScoreManager.instance.remainTooth;
        Debug.Log(stars);
        if (stars == 1)
            starsBoard.GetComponent<Stars>().Star2();
        if (stars == 2)
            starsBoard.GetComponent<Stars>().Star1();
        if (stars == 3)
            starsBoard.GetComponent<Stars>().Star0();
    }*/

    void Waving()
    {
        if (waves.Length > i)
        {
            waves[i].SetActive(true);
            i++;
            //Debug.Log("웨이브" + i);
        }
        else
        {
            CancelInvoke("CreatePrefab");
            intervalSec = intervalClone / 20;
            InvokeRepeating("CreatePrefab", intervalSec, intervalSec);
        }
            

    }

    public void Check_Timer()
    {
        time_current = Time.time - time_start;
        if (time_current < time_Max)
        {
            timer = $"{time_current:N2}";
            //Debug.Log(time_current);
        }
        else if (!isEnded)
        {
            End_Timer();
        }
    }

    public void End_Timer()
    {
        Debug.Log("End");
        time_current = time_Max;
        timer = $"{time_current:N2}";
        isEnded = true;
        if (canWin)
        {
            Victory();
            canWin = false;
        }
        
    }


    public void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        timer = $"{time_current:N2}";
        isEnded = false;
        //Debug.Log("Start");
    }

    public void ActWave(int numb)
    {
        //Debug.Log("웨이브");
        waves[numb].gameObject.SetActive(true);
    }
    


    private void Initialize(int initCount) 
    {
        for (int i = 0; i < initCount; i++) 
        {
            poolingObjectQueue.Enqueue(CreateNewObject()); 
        } 
    }

    public Enemy CreateNewObject() 
    {
        int sellection = Random.Range(0, 100);
        if (sellection < 40)
        {
            poolingObjectPrefab = Prefab1;
        }
        else
        {
            if (sellection < 70)
            {
                poolingObjectPrefab = Prefab2;
            }
            else
            {
                if (sellection < 90)
                    poolingObjectPrefab = Prefab3;
                else
                    poolingObjectPrefab = Prefab4;

            }
                
        }
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Enemy>(); 
        newObj.gameObject.SetActive(false); 
        newObj.transform.SetParent(transform); return newObj; 
    }   


    public static Enemy GetObject() 
    { 
        if (Instance.poolingObjectQueue.Count > 0) 
        { 
            var obj = Instance.poolingObjectQueue.Dequeue(); 
            //obj.transform.SetParent(null); 
            obj.gameObject.SetActive(true); 
            return obj; 
        } 
        else 
        {
            var newObj = Instance.CreateNewObject(); 
            newObj.gameObject.SetActive(true); 
            //newObj.transform.SetParent(null); 
            return newObj; 
        } 
    }    



    

    public void CountEnemy()
    {
        enemy = GameObject.FindGameObjectsWithTag("Monster");
        enemyCount = enemy.Length;
    }

    public static void ReturnObject(Enemy obj) 
    {
        obj.GetComponent<Enemy>().canMove = true;
        obj.gameObject.SetActive(false); 
        obj.transform.SetParent(Instance.transform); 
        Instance.poolingObjectQueue.Enqueue(obj); 
    }

    



    // Update is called once per frame
    void CreatePrefab()
    {
        if (enemyCount <= enemyCountLimit)
        {
            //Vector3 area = GetComponent<SpriteRenderer>().bounds.size;
            Vector3 area = bG.GetComponent<SpriteRenderer>().bounds.size;
            Vector3 newPos = this.transform.position;
            Vector3 randomPositive = this.transform.position;
            Vector3 randomNegative = this.transform.position;            

            randomPositive.y += Random.Range(-area.y / 2.5f, -area.y / 2.4f);
            randomNegative.y += Random.Range(area.y / 2.4f, area.y / 2.5f);

            newPos.x += Random.Range(-area.x / 2.5f, area.x / 2.5f);

            int randY = Random.Range(0, 2);
            if (randY == 0)
            {
                newPos.y += randomPositive.y;
            }

            if (randY == 1)
            {
                newPos.y += randomNegative.y;
            }
            
            newPos.z = 0;

            Enemy Enemy1 = GetObject();
            Enemy1.transform.position = newPos;
            //Enemy1.transform.parent = this.transform;
            CountEnemy();
            //Debug.Log(enemyCount);
        }
        else
            CountEnemy();
        

    }

    public void Victory()
    {
        canWin = false;
        GameObject mouth = GameObject.Find("Mouth");
        mouth.GetComponent<TeethManager>().RestoreTeeth();
        StatusManager.instance.exp = StatusManager.instance.exp + exp;
        //Debug.Log(StatusManager.instance.exp);
        GameManager.instance.Victory(stageNumb);
        if (levelDifficulty == 0)
        {
            StatusManager.instance.dia = StatusManager.instance.dia + 20;
        }
        if (levelDifficulty == 1)
        {
            StatusManager.instance.dia = StatusManager.instance.dia + 30;
        }
        if (levelDifficulty == 2)
        {
            StatusManager.instance.dia = StatusManager.instance.dia + 50;
        }
    }

}
