using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialOpener;
    [SerializeField]
    GameObject AvatarUnlockPN;
    bool avatarUnlock = false;
    [SerializeField]
    GameObject auto;
    [SerializeField]
    GameObject toiletBtn;
    [SerializeField]
    GameObject dentistBtn;
    [SerializeField]
    GameObject victoryPn;
    [SerializeField]
    GameObject nextBtn;
    [SerializeField]
    GameObject gameOverPn;
    [SerializeField]
    GameObject pausePn;
    [SerializeField]
    GameObject pauseBtn;
    [SerializeField]
    GameObject starterPn;
    [SerializeField]
    GameObject noHeartPn;
    [SerializeField]
    TextMeshProUGUI StartButtonHeartNumbTMP;
    int heartCunsumeNumb;
    public static GameManager instance = null;
    public UnityEvent m_MyEvent;
    public UnityEvent heal;
    public string currentStageName="삼겹살";
    public string mapBgmName;
    public string[] landName;
    public string currentLand;
    public int landNumb;
    public int expUp;
    public int landStageNumb;
    public bool canSecondChance;
    bool landWelcome;

    [SerializeField]
    public Animator killZone;

    [SerializeField]
    GameObject autoOn;
    [SerializeField]
    GameObject autoOff;

    [SerializeField]
    GameObject secondChancePN;
    [SerializeField]
    GameObject secondChanceGo;
    
    [SerializeField]
    public Animator theCamera;
    [SerializeField]
    public string thunder_Sound;
    [SerializeField]
    public GameObject dentalClinic;
    [SerializeField]
    public Animator upper;
    [SerializeField]
    public Animator under;
    [SerializeField]
    GameObject landMap;
    public string bgmName = "Dracula";
    [SerializeField]
    TextMeshProUGUI failedNumbTMP;
    [SerializeField]
    GameObject welcomLandPN;

    public int[] stageNumb;

    private void Awake()
    {
        

        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때 
        {
            instance = this; //내자신을 instance로 넣어줍니다. 
            //DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지 
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미 
               Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제 
        }
    }


    void Start()
    {
        currentLand = landName[landNumb];
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(Dead);

        if (heal == null)
            heal = new UnityEvent();

        heal.AddListener(Heal);


        //Pause();
        Time.timeScale = 0;
        stageNumb[0] = StatusManager.instance.stageLevel1;
        stageNumb[1] = StatusManager.instance.stageLevel2;
        landNumb = StatusManager.instance.currentLand;
        
        canSecondChance = false;
        string[] arr=StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });

        if (arr[0]=="0")
        {
            welcomLandPN.SetActive(true);            
        }
        


    }

    public void UnlockAvatar(int numb, string name)
    {
        string[] arr = StatusManager.instance.avatarUnlock.Split(new char[] { ',' });

        if(arr[numb] == "0")
        {
            avatarUnlock = true;
            //AvatarUnlockPN.SetActive(true);
            AvatarUnlockPN.GetComponent<AvatarUnlockPrefab>().LoadAvatarPrefab(name);
            arr[numb] = "1";
            StatusManager.instance.avatarUnlock = string.Join(",", arr);
        }       
        
    }
    public void UnlockAvatarPNOpen()
    {
        if (avatarUnlock)
        {
            AvatarUnlockPN.SetActive(true);
            avatarUnlock = false;
        }
        
    }

    public void LandMap()
    {
        dentistBtn.SetActive(true);
        Time.timeScale = 0;
        dentalClinic.SetActive(false);
        pausePn.SetActive(false);
        toiletBtn.SetActive(true);
        victoryPn.SetActive(false);
        landMap.SetActive(true);
        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(StatusManager.instance.tootheCount);
        heal.Invoke();
        ScoreManager.instance.remainTooth = StatusManager.instance.tootheCount;
        pauseBtn.SetActive(false);
        //GameObject bgm = GameObject.Find("BGM_Manager");
        GameObject mouth = GameObject.Find("Mouth");
        mouth.GetComponent<TeethManager>().RestoreTeeth();
        tutorialOpener.SetActive(true);

        GameObject.Find(currentLand).GetComponentInChildren<LandMap>().upDateStage(stageNumb[landNumb]);
        GameObject.Find(currentLand).GetComponentInChildren<LandMap>().CheckStages();
        GameObject.Find(currentLand).GetComponentInChildren<LandMap>().PlayBGM();
    }

    public void QuitStage()
    {
        dentistBtn.SetActive(true);
        Time.timeScale = 0;
        dentalClinic.SetActive(false);
        pausePn.SetActive(false);
        toiletBtn.SetActive(true);
        victoryPn.SetActive(false);
        landMap.SetActive(true);
        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(StatusManager.instance.tootheCount);
        heal.Invoke();
        ScoreManager.instance.remainTooth = StatusManager.instance.tootheCount;
        pauseBtn.SetActive(false);
        //GameObject bgm = GameObject.Find("BGM_Manager");
        GameObject mouth = GameObject.Find("Mouth");
        mouth.GetComponent<TeethManager>().RestoreTeeth();


        //GameObject.Find(currentLand).GetComponentInChildren<LandMap>().upDateStage(stageNumb[landNumb]);
        //GameObject.Find(currentLand).GetComponentInChildren<LandMap>().CheckStages();
        GameObject.Find(currentLand).GetComponentInChildren<LandMap>().PlayBGM();
    }

    public void GameStarter()
    {
        int currentLandNumb = StatusManager.instance.currentLand;
        string[] newArr2 = null;
        int difficult = 0;
        if (currentLandNumb == 0)
        {
            newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            difficult=int.Parse(newArr2[landStageNumb]);
            heartCunsumeNumb = difficult + 5;

        }
        if (currentLandNumb == 1)
        {
            newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            difficult = int.Parse(newArr2[landStageNumb]);
            heartCunsumeNumb = difficult + 5;
        }

        StartButtonHeartNumbTMP.text = heartCunsumeNumb.ToString();

        tutorialOpener.SetActive(false);
        starterPn.SetActive(true);
        //GameObject.Find("Item_EXP").GetComponentInChildren<TextMeshProUGUI>().text = expUp.ToString();
    }

    public void DiaHeartCharge()
    {
        if (StatusManager.instance.dia >= 500)
        {
            HeartRechargerManager.instance.UseHeartNumb(-60);
            HeartRechargerManager.instance.UseHeart();
            StatusManager.instance.dia = StatusManager.instance.dia - 500;
            noHeartPn.SetActive(false);
            GameOpen();
        }
    }

    public void GameOpen()
    {
        if (HeartRechargerManager.instance.m_HeartAmount >= heartCunsumeNumb)
        {
            //HeartRechargerManager.instance.m_HeartAmount = HeartRechargerManager.instance.m_HeartAmount - 5;
            HeartRechargerManager.instance.UseHeartNumb(heartCunsumeNumb);
            HeartRechargerManager.instance.OnClickUseHeart();
            dentistBtn.SetActive(false);
            Time.timeScale = 1;
            dentalClinic.SetActive(false);
            pausePn.SetActive(false);
            toiletBtn.SetActive(false);
            victoryPn.SetActive(false);
            landMap.SetActive(false);
            pauseBtn.SetActive(true);
            starterPn.SetActive(false);
            GameObject bgm = GameObject.Find("BGM_Manager");
            bgm.GetComponent<BGM_Manager>().PlayBGM(bgmName);
        }
        else
        {
            noHeartPn.SetActive(true);
        }
    }

    
    public void OneMoreGame()
    {
        //GameObject.Find("AdMobManager").GetComponent<AdmobManager>().LoadRewardAdOneShot();
        
    }

    public void OnMoreGameStart()
    {
        dentistBtn.SetActive(false);
        Time.timeScale = 1;
        dentalClinic.SetActive(false);
        pausePn.SetActive(false);
        toiletBtn.SetActive(false);
        victoryPn.SetActive(false);
        landMap.SetActive(false);
        pauseBtn.SetActive(true);
        starterPn.SetActive(false);
        Debug.Log("원모어 게임스타");
        GameObject bgm = GameObject.Find("BGM_Manager");
        bgm.GetComponent<BGM_Manager>().PlayBGM(bgmName);
    }

    public void GotoDailyPresent()
    {
        StatusManager.instance.dailyPresent=true;
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM("gameover");
        //BGM_Manager.instance.PlayBGM("gameover");
        int currentLandNumb = StatusManager.instance.currentLand;
        string[] newArr2 = null;
        if (currentLandNumb == 0)
        {
            newArr2 = StatusManager.instance.land1failed.Split(new char[] { ',' });
        }
        if (currentLandNumb == 1)
        {
            newArr2 = StatusManager.instance.land2failed.Split(new char[] { ',' });
        }

        for (int j = 0; j < newArr2.Length; j++)
        {
            if (j == GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb)
            {
                if (newArr2[j] == "0")
                    newArr2[j] = "1";
                else
                {
                    int level = int.Parse(newArr2[j]);
                    level++;
                    Debug.Log("레벨" + level);
                    newArr2[j] = level.ToString();


                }
            }
        }
        //stageNumb[landNumb] = numb;

        

        if (int.Parse(newArr2[stageNumb[currentLandNumb]]) >= 0 & canSecondChance)
        {
            if (currentLandNumb == 0)
            {
                StatusManager.instance.land1failed = string.Join(",", newArr2);
                StatusManager.instance.stageLevel1 = stageNumb[0];
            }
            if (currentLandNumb == 1)
            {
                StatusManager.instance.land2failed = string.Join(",", newArr2);
                StatusManager.instance.stageLevel2 = stageNumb[1];
            }
            secondChancePN.SetActive(true);
            canSecondChance = false;
            failedNumbTMP.text = (int.Parse(newArr2[stageNumb[currentLandNumb]])+1).ToString() + " 번이나 실패했으면서도 \n 배운게 없다니 말이야";
        }
        else
            RealOver();
    }

    public void TrySecondChance()
    {
        AdmobObj.instance.UserChoseToWatchAd();
        Time.timeScale = 0;

        /*((success) => {
        if (success)
        {
            secondChancePN.SetActive(false);
            Debug.Log("패널닫기");
            SecondChance();
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("광고 실패");
        }
    });*/
    }
    public void SecondChance(int numb)
    {
        /*secondChancePN.SetActive(false);
        Debug.Log("패널닫기");
        ScoreManager.instance.remainTooth = 2;
        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(2);
        GameObject.Find("Mouth").GetComponent<TeethManager>().RestoreTeeth();
        dentistBtn.SetActive(false);
        Time.timeScale = 0;
        dentalClinic.SetActive(false);
        //pausePn.SetActive(true);
        toiletBtn.SetActive(true);
        victoryPn.SetActive(false);
        pauseBtn.SetActive(false);
        GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM(bgmName);
        OpenSecondChanceGo();*/
        //Time.timeScale = 1;
        if (StatusManager.instance.dia >= numb)
        {
            StatusManager.instance.dia = StatusManager.instance.dia - numb;
            StartCoroutine("GiveSecondChance");
        }
        
    }

    IEnumerator GiveSecondChance()
    {
        yield return new WaitForEndOfFrame();
        secondChancePN.SetActive(false);
        Debug.Log("패널닫기");
        ScoreManager.instance.remainTooth = 2;
        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(2);
        GameObject.Find("Mouth").GetComponent<TeethManager>().RestoreTeeth();
        dentistBtn.SetActive(false);
        Time.timeScale = 0;
        dentalClinic.SetActive(false);
        //pausePn.SetActive(true);
        toiletBtn.SetActive(true);
        victoryPn.SetActive(false);
        pauseBtn.SetActive(false);
        GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM(bgmName);
        OpenSecondChanceGo();
    }

    public void OpenSecondChanceGo()
    {
        secondChanceGo.SetActive(true);
        Time.timeScale = 0;
    }

    public void RealOver()
    {
        int currentLandNumb = StatusManager.instance.currentLand;
        string[] newArr2 = null;
        if (currentLandNumb == 0)
        {
            newArr2 = StatusManager.instance.land1failed.Split(new char[] { ',' });
        }
        if (currentLandNumb == 1)
        {
            newArr2 = StatusManager.instance.land2failed.Split(new char[] { ',' });
        }



        //for (int j = 0; j < newArr2.Length; j++)
        //{
        //    if (j == GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb)
        //    {
        //        if (newArr2[j] == "0")
        //            newArr2[j] = "1";
        //        else
        //        {
        //            int level = int.Parse(newArr2[j]);
        //            level++;
        //            Debug.Log("레벨" + level);
        //            newArr2[j] = level.ToString();


        //        }
        //    }
        //}
        ////stageNumb[landNumb] = numb;

        //if (currentLandNumb == 0)
        //{
        //    StatusManager.instance.land1failed = string.Join(",", newArr2);
        //    StatusManager.instance.stageLevel1 = stageNumb[0];
        //}
        //if (currentLandNumb == 1)
        //{
        //    StatusManager.instance.land2failed = string.Join(",", newArr2);
        //    StatusManager.instance.stageLevel2 = stageNumb[1];
        //}

        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();
        gameOverPn.SetActive(true);
        Time.timeScale = 0;
        heal.Invoke();
    }

    

    public void RunAuto()
    {
        auto.SetActive(true);
        autoOn.SetActive(false);
        autoOff.SetActive(true);
    }
    public void StopAuto()
    {
        auto.SetActive(false);
        autoOn.SetActive(true);
        autoOff.SetActive(false);
    }

    


    public void Pause()
    {
        dentistBtn.SetActive(false);
        Time.timeScale = 0;
        dentalClinic.SetActive(false);
        pausePn.SetActive(true);
        toiletBtn.SetActive(true);
        victoryPn.SetActive(false);
        pauseBtn.SetActive(false);
    }
    public void Dentist()
    {
        dentalClinic.SetActive(true);
        GameObject bgm = GameObject.Find("BGM_Manager");
        bgm.GetComponent<BGM_Manager>().PlayBGM("CCC");
    }

    public void CloseDentist()
    {
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();
        dentalClinic.SetActive(false);
        //GameObject bgm = GameObject.Find("BGM_Manager");
        GameObject.Find(currentLand).GetComponentInChildren<LandMap>().PlayBGM();
        //bgm.GetComponent<BGM_Manager>().PlayBGM("town1");
    }

    public void Resume()
    {
        toiletBtn.SetActive(false);
        victoryPn.SetActive(false);
        //dentistBtn.SetActive(true);
        dentalClinic.gameObject.SetActive(false);
        pausePn.SetActive(false);
        pauseBtn.SetActive(true);
        Time.timeScale = 1;
        if (StageManager.instance.currentStage == null)
        {
            StageManager.instance.SettingStage(0);
        }
    }
    


    void KillEmAll()
    {
        killZone.SetTrigger("kill");
        //killZone.gameObject.SetActive(true);
        //killZone.gameObject.SetActive(false);
    }
    void StopKillEmAll()
    {
        killZone.gameObject.SetActive(false);
    }

    public void CameraShake()
    {
        SoundManager.instance.PlaySE(thunder_Sound);
        theCamera.SetTrigger("shake");
        upper.SetTrigger("Hurt");
        under.SetTrigger("Hurt");
    }
    

    void Dead()
    {
        Debug.Log("Ping");
    }
    void Heal()
    {
        Debug.Log("Heal");
    }
    public void Victory(int numb)
    {
        KillEmAll();
        //Time.timeScale = 0.2f;

        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(StatusManager.instance.tootheCount);
        int currentLandNumb = StatusManager.instance.currentLand;
        string[] newArr2 = null;
        if (currentLandNumb == 0)
        {
            newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        }
        if (currentLandNumb == 1)
        {
            newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
        }



        for (int j = 0; j < newArr2.Length; j++)
        {
            if (j == GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb)
            {
                if (newArr2[j] == "0")
                    newArr2[j] = "1";
                else
                {
                    int level = int.Parse(newArr2[j]);
                    level++;
                    //Debug.Log("레벨" + level);
                    newArr2[j] = level.ToString();


                }
            }
        }
        stageNumb[landNumb] = numb;

        if (currentLandNumb == 0)
        {
            StatusManager.instance.stageUnlock0101 = string.Join(",", newArr2);
            StatusManager.instance.stageLevel1 = stageNumb[0];
        }
        if (currentLandNumb == 1)
        {
            StatusManager.instance.stageUnlock0102 = string.Join(",", newArr2);
            StatusManager.instance.stageLevel2 = stageNumb[1];
        }
        GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().Save();

        Invoke("VictoryPanel", 0.5f);
        GameObject bgm = GameObject.Find("BGM_Manager");
        bgm.GetComponent<BGM_Manager>().PlayBGM("victory");

        
        

        //heal.Invoke();
        ScoreManager.instance.remainTooth = StatusManager.instance.tootheCount;
        GameObject.Find("Mouth").GetComponent<TeethManager>().RestoreTeeth();





    }

    public void LandPositionLoad()
    {
        GameObject.Find("MapPosition").GetComponentInChildren<LandMap>().ScrollPositionLoad();
    }
    public void LandPositionSave()
    {
        GameObject.Find("MapPosition").GetComponentInChildren<LandMap>().ScrollPositionSave();
    }

    void VictoryPanel()
    {
        pauseBtn.SetActive(false);
        victoryPn.SetActive(true);
        GameObject.Find("TopScore").GetComponent<TopScoreUpdate>().SetTopScore();
        GameObject.Find("TopScore").GetComponent<TopScoreUpdate>().CheckCanRecord();
        AchievementsManager.instance.CheckUnlocks();
        Time.timeScale = 0;
    }

    public void TBInitialize()
    {
        GameObject tbObject = GameObject.Find("ToothBrush");
        tbObject.GetComponent<ToothBrushMove>().Initialize();
    }
    
}
