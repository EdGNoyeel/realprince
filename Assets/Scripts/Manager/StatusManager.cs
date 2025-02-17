using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class MoneyUnitString
{
    static string[] unitSymbol = new string[] { "만", "억", "조", "경", "해" };

    // long 보다 double이 최대 값이 커서 double  사용
    public static string ToString(double value)
    {
        if (value == 0) { return "0"; }

        int unitID = 0;

        string number = string.Format("{0:# #### #### #### #### ####}", value).TrimStart();
        string[] splits = number.Split(' ');

        StringBuilder sb = new StringBuilder();

        for (int i = splits.Length; i > 0; i--)
        {
            int digits = 0;
            if (int.TryParse(splits[i - 1], out digits))
            {
                // 앞자리가 0이 아닐때
                if (digits != 0)
                {
                    sb.Insert(0, $"{ digits}{ unitSymbol[unitID] }");
                }
            }
            else
            {
                // 마이너스나 숫자외 문자
                sb.Insert(0, $"{ splits[i - 1] }");
            }
            unitID++;
        }
        return sb.ToString();
    }
}

public static class ExtensionNumber
{
    public static string ToMoneyString(this long target)
    {
        return MoneyUnitString.ToString(target);
    }
}

public class StatusManager : MonoBehaviour
{
    public string firstBuy;
    public bool firstLogin = true;
    public int adRepeated = 0;
    public string announcement;
    public string testVersion;
    public string currentVersion;
    public bool fireStoreLoaded = false;
    public bool canPopUpInStarterPN = true;
    static public StatusManager instance;
    public int tbDamageLv = 1;
    public int tbCriDamLv = 1;
    public int tbCriRateLv = 1;
    //이빨 업그레이드
    public string teethUpgrade = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public int tootheCount = 3;
    public int score = 0;
    public int dia = 0;
    public int stageLevel1 = 0;
    public int stageLevel2 = 0;
    public int killNumberA = 0;
    public int killNumberB = 0;
    public int killNumberC = 0;
    public int killNumberD = 0;
    public int killNumberE = 0;
    public int killNumberF = 0;
    public int killNumberG = 0;
    public int killNumberH = 0;
    public int killNumberI = 0;
    public int killNumberJ = 0;
    public int killNumberK = 0;
    public int killNumberL = 0;
    public int killNumberM = 0;
    public int killNumberN = 0;
    public int killNumberO = 0;
    public int killNumberP = 0;
    public int killNumberQ = 0;
    public int killNumberR = 0;
    public int killNumberS = 0;
    public int killNumberT = 0;
    public int killNumberU = 0;
    public int killNumberV = 0;
    public int killNumberW = 0;
    public int killNumberX = 0;
    public int killNumberY = 0;
    public int killNumberZ = 0;
    public int heartCount;
    public string uid;
    public string email;
    public string stageUnlock0101;
    public string stageUnlock0102;
    public string stageUnlock0103;
    public int currentLand = 1;
    public int exp = 0;
    public string userName="????";
    public string canNameChange = "1";
    public string canNameReset = "0";
    

    public string land1Record = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string land1failed = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string land2Record = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string land2failed = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    
    public string achievements = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string unlocks = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public bool[] sGP_Item;
    public int[] stageNumb;
    public string[] land1Topscore;
    public string[] land2Topscore;
    public int killedByNPC=0;
    //요정관련
    public string fairyUnLock = "0.0.0.0.0.0.0.0.0.0.1.1.0";
    public int currentFairy = 10;
    public int currentFairy1 = 11;
    public int additionalSlotUnlock = 0;
    public string fairySkillCCC = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string fairySkillSGP = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";

    public string currentStory = "0";
    public string avatar = "LSH";
    public string avatarUnlock = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public string myWord = "친구들 만나서 반가워!";

    public bool dailyPresent=false;
    public bool myPage=false;
    
    public int presentedTime=1501;
    //public bool canPresent = true;
    //public float musicVol;
    //public float effectVol;

    //출석관련
    public string today;
    public string attendanceCheck="0";
    public string homeworkDone = "0,0,0,0,0,0";
    public string homeworkReward = "0,0,0,0,0,0";
    public string homeworkTarget = "0,0,0,0,0,0,0,0,0,0";
    public string events = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    public List<string> homeworkString;

    void Awake()//객체 생성시 최초 실행
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);
    }
    // Start is called before the first frame update

    private void Start()
    {
        today= today = DateTime.Now.Date.ToString();
    }

}
