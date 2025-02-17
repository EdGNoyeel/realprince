using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class FireStoreManager : MonoBehaviour
{
    [SerializeField]
    string thisVersion;
    [SerializeField]
    bool test;
    [SerializeField]
    bool iOS;
    [SerializeField]
    public Button loginBTN;
    public bool updateChecked=false;
    public string testVersion;
    public string currentVersion;
    


    Dictionary<string, object> user;
    Dictionary<string, object> topScore;
    void Start()
    {
        //loginBTN=GameObject.Find("LoginBTN").GetComponent<Button>();

    }

    void Update()
    {
        /*if (loginBTN != null)
        {
            if (loginBTN.interactable)
            {
                if (!updateChecked)
                {
                    CheckUpdate();
                    updateChecked=true;
                }
            }
        }*/
    }

    public async void CheckUpdate()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("version");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            StatusManager.instance.currentVersion= documentDictionary["currentversion"].ToString();
            StatusManager.instance.testVersion = documentDictionary["testversion"].ToString();

        }
    }
    public async void CheckAnnouncement()
    {
        //Debug.Log("공지채크");
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("announcement");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            //Debug.Log("SnapShot have");
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
            //Debug.Log(documentDictionary["0"].ToString());

            StatusManager.instance.announcement = documentDictionary["0"].ToString();
            //StatusManager.instance.testVersion = documentDictionary["testversion"].ToString();

        }
        // else{
        //     Debug.Log("no SnapShot");
        // }
    }

    public void Login()
    {
        
        //Debug.Log("파이어스토어 스타트");
        LoadHomework();
        Load();
        LoadLand1TopScore();
        LoadLand2TopScore();
        CheckUpdate();
        CheckAnnouncement();


    }
    public void Save()
    {
        if (StatusManager.instance.uid != "pIMlZURo4DMZhCTrwKwqIFAVG962")
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("users").Document(StatusManager.instance.uid);
            user = new Dictionary<string, object>
{
        { "uid", StatusManager.instance.uid },
        { "email", StatusManager.instance.email },
        { "gold", StatusManager.instance.score },
        { "dia", StatusManager.instance.dia },
        { "heartCount", StatusManager.instance.heartCount },
        { "tbDamageLv", StatusManager.instance.tbDamageLv },
        { "stageLevel1", StatusManager.instance.stageLevel1 },
        { "stageLevel2", StatusManager.instance.stageLevel2 },
        { "killNumberA", StatusManager.instance.killNumberA },
        { "killNumberB", StatusManager.instance.killNumberB },
        { "killNumberC", StatusManager.instance.killNumberC },
        { "killNumberD", StatusManager.instance.killNumberD },
        { "stageUnlock0101", StatusManager.instance.stageUnlock0101 },
        { "stageUnlock0102", StatusManager.instance.stageUnlock0102 },
        { "stageUnlock0103", StatusManager.instance.stageUnlock0103 },
        { "exp", StatusManager.instance.exp },
        { "killNumberE", StatusManager.instance.killNumberE },
        { "killNumberF", StatusManager.instance.killNumberF },
        { "killNumberG", StatusManager.instance.killNumberG },
        { "killNumberH", StatusManager.instance.killNumberH },
        { "killNumberI", StatusManager.instance.killNumberI },
        { "killNumberJ", StatusManager.instance.killNumberJ },
        { "killNumberK", StatusManager.instance.killNumberK },
        { "killNumberL", StatusManager.instance.killNumberL },
        { "killNumberM", StatusManager.instance.killNumberM },
        { "killNumberN", StatusManager.instance.killNumberN },
        { "killNumberO", StatusManager.instance.killNumberO },
        { "killNumberP", StatusManager.instance.killNumberP },
        { "killNumberQ", StatusManager.instance.killNumberQ },
        { "killNumberR", StatusManager.instance.killNumberR },
        { "killNumberS", StatusManager.instance.killNumberS },
        { "killNumberT", StatusManager.instance.killNumberT },
        { "killNumberU", StatusManager.instance.killNumberU },
        { "killNumberV", StatusManager.instance.killNumberV },
        { "killNumberW", StatusManager.instance.killNumberW },
        { "killNumberX", StatusManager.instance.killNumberX },
        { "killNumberY", StatusManager.instance.killNumberY },
        { "killNumberZ", StatusManager.instance.killNumberZ },
        { "userName", StatusManager.instance.userName },
        { "currentLand", StatusManager.instance.currentLand },
        { "tbCriDamLv", StatusManager.instance.tbCriDamLv },
        { "tbCriRateLv", StatusManager.instance.tbCriRateLv },
        { "currentFairy", StatusManager.instance.currentFairy },
        { "currentFairy1", StatusManager.instance.currentFairy1 },
        { "fairySkillSGP", StatusManager.instance.fairySkillSGP },
        { "fairySkillCCC", StatusManager.instance.fairySkillCCC },
        { "fairyUnLock", StatusManager.instance.fairyUnLock },
        { "additionalSlotUnlock", StatusManager.instance.additionalSlotUnlock },
        { "achievements", StatusManager.instance.achievements },
        { "canNameChange" , StatusManager.instance.canNameChange },
        { "land1failed" , StatusManager.instance.land1failed },
        { "land2failed" , StatusManager.instance.land2failed },
        //{ "tootheCount", StatusManager.instance.tootheCount },
        { "teethUpgrade", StatusManager.instance.teethUpgrade },
        { "avatarUnlock", StatusManager.instance.avatarUnlock },
        { "myWord", StatusManager.instance.myWord },
        { "canNameReset", StatusManager.instance.canNameReset },
        { "avatar", StatusManager.instance.avatar },
        { "homeworkDone", StatusManager.instance.homeworkDone },
        { "today", StatusManager.instance.today },
        { "homeworkReward", StatusManager.instance.homeworkReward },
        { "homeworkTarget", StatusManager.instance.homeworkTarget },
        { "events", StatusManager.instance.events },
        { "adRepeated", StatusManager.instance.adRepeated },
        { "firstBuy", StatusManager.instance.firstBuy }


        };



            docRef.SetAsync(user);

            docRef.SetAsync(user).ContinueWithOnMainThread(task => {
                Debug.Log("스테이터스 업데이트완료");
            });
        }
        

        
    }

    public async void LoadHomework()
    {
        string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
        StatusManager.instance.homeworkString.Clear();
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Debug.Log("숙제로딩");
        DocumentReference docRef = db.Collection("homework").Document(dayOfWeek);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            StatusManager.instance.homeworkString.Clear();

            StatusManager.instance.homeworkString.Add(documentDictionary["1"].ToString());
            StatusManager.instance.homeworkString.Add(documentDictionary["2"].ToString());
            StatusManager.instance.homeworkString.Add(documentDictionary["3"].ToString());
            StatusManager.instance.homeworkString.Add(documentDictionary["4"].ToString());
            StatusManager.instance.homeworkString.Add(documentDictionary["5"].ToString());

            //HomeWorkManager.instance.UpdateHomework();
        }

        //HomeWorkManager.instance.UpdateHomework();
    }

    public async void Load()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Load Status"+StatusManager.instance.uid);
        DocumentReference docRef = db.Collection("users").Document(StatusManager.instance.uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {

            Debug.Log("스냅샷로딩됨");
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
            Debug.Log(snapshot);
            Debug.Log(string.Format(snapshot.Id));
            Debug.Log("docs_"+documentDictionary["uid"]);
            //Debug.Log(user["email"]);
            if(documentDictionary.ContainsKey("uid"))
                StatusManager.instance.uid = documentDictionary["uid"].ToString();
            if (documentDictionary.ContainsKey("email"))
                StatusManager.instance.email = documentDictionary["email"].ToString();
            if (documentDictionary.ContainsKey("gold"))
                StatusManager.instance.score = int.Parse(documentDictionary["gold"].ToString());
            if (documentDictionary.ContainsKey("dia"))
                StatusManager.instance.dia = int.Parse(documentDictionary["dia"].ToString());
            if (documentDictionary.ContainsKey("heartCount"))
                StatusManager.instance.heartCount = int.Parse(documentDictionary["heartCount"].ToString());
            if (documentDictionary.ContainsKey("tbDamageLv"))
                StatusManager.instance.tbDamageLv = int.Parse(documentDictionary["tbDamageLv"].ToString());
            if (documentDictionary.ContainsKey("stageLevel1"))
                StatusManager.instance.stageLevel1 = int.Parse(documentDictionary["stageLevel1"].ToString());
            if (documentDictionary.ContainsKey("stageLevel2"))
                StatusManager.instance.stageLevel2 = int.Parse(documentDictionary["stageLevel2"].ToString());
            if (documentDictionary.ContainsKey("killNumberA"))
                StatusManager.instance.killNumberA = int.Parse(documentDictionary["killNumberA"].ToString());
            if (documentDictionary.ContainsKey("killNumberB"))
                StatusManager.instance.killNumberB = int.Parse(documentDictionary["killNumberB"].ToString());
            if (documentDictionary.ContainsKey("killNumberC"))
                StatusManager.instance.killNumberC = int.Parse(documentDictionary["killNumberC"].ToString());
            if (documentDictionary.ContainsKey("killNumberD"))
                StatusManager.instance.killNumberD = int.Parse(documentDictionary["killNumberD"].ToString());
            if (documentDictionary.ContainsKey("stageUnlock0101"))
                StatusManager.instance.stageUnlock0101 = documentDictionary["stageUnlock0101"].ToString();
            if (documentDictionary.ContainsKey("stageUnlock0102"))
                StatusManager.instance.stageUnlock0102 = documentDictionary["stageUnlock0102"].ToString();
            if (documentDictionary.ContainsKey("uidstageUnlock0103"))
                StatusManager.instance.stageUnlock0103 = documentDictionary["stageUnlock0103"].ToString();
            if (documentDictionary.ContainsKey("exp"))
                StatusManager.instance.exp = int.Parse(documentDictionary["exp"].ToString());
            if (documentDictionary.ContainsKey("killNumberE"))
                StatusManager.instance.killNumberE = int.Parse(documentDictionary["killNumberE"].ToString());
            if (documentDictionary.ContainsKey("killNumberF"))
                StatusManager.instance.killNumberF = int.Parse(documentDictionary["killNumberF"].ToString());
            if (documentDictionary.ContainsKey("killNumberG"))
                StatusManager.instance.killNumberG = int.Parse(documentDictionary["killNumberG"].ToString());
            if (documentDictionary.ContainsKey("killNumberH"))
                StatusManager.instance.killNumberH = int.Parse(documentDictionary["killNumberH"].ToString());
            if (documentDictionary.ContainsKey("killNumberI"))
                StatusManager.instance.killNumberI = int.Parse(documentDictionary["killNumberI"].ToString());
            if (documentDictionary.ContainsKey("killNumberJ"))
                StatusManager.instance.killNumberJ = int.Parse(documentDictionary["killNumberJ"].ToString());
            if (documentDictionary.ContainsKey("killNumberK"))
                StatusManager.instance.killNumberK = int.Parse(documentDictionary["killNumberK"].ToString());
            if (documentDictionary.ContainsKey("killNumberL"))
                StatusManager.instance.killNumberL = int.Parse(documentDictionary["killNumberL"].ToString());
            if (documentDictionary.ContainsKey("killNumberM"))
                StatusManager.instance.killNumberM = int.Parse(documentDictionary["killNumberM"].ToString());
            if (documentDictionary.ContainsKey("killNumberN"))
                StatusManager.instance.killNumberN = int.Parse(documentDictionary["killNumberN"].ToString());
            if (documentDictionary.ContainsKey("killNumberO"))
                StatusManager.instance.killNumberO = int.Parse(documentDictionary["killNumberO"].ToString());
            if (documentDictionary.ContainsKey("killNumberP"))
                StatusManager.instance.killNumberP = int.Parse(documentDictionary["killNumberP"].ToString());
            if (documentDictionary.ContainsKey("killNumberQ"))
                StatusManager.instance.killNumberQ = int.Parse(documentDictionary["killNumberQ"].ToString());
            if (documentDictionary.ContainsKey("killNumberR"))
                StatusManager.instance.killNumberR = int.Parse(documentDictionary["killNumberR"].ToString());
            if (documentDictionary.ContainsKey("killNumberS"))
                StatusManager.instance.killNumberS = int.Parse(documentDictionary["killNumberS"].ToString());
            if (documentDictionary.ContainsKey("killNumberT"))
                StatusManager.instance.killNumberT = int.Parse(documentDictionary["killNumberT"].ToString());
            if (documentDictionary.ContainsKey("killNumberU"))
                StatusManager.instance.killNumberU = int.Parse(documentDictionary["killNumberU"].ToString());
            if (documentDictionary.ContainsKey("killNumberV"))
                StatusManager.instance.killNumberV = int.Parse(documentDictionary["killNumberV"].ToString());
            if (documentDictionary.ContainsKey("killNumberW"))
                StatusManager.instance.killNumberW = int.Parse(documentDictionary["killNumberW"].ToString());
            if (documentDictionary.ContainsKey("killNumberX"))
                StatusManager.instance.killNumberX = int.Parse(documentDictionary["killNumberX"].ToString());
            if (documentDictionary.ContainsKey("killNumberY"))
                StatusManager.instance.killNumberY = int.Parse(documentDictionary["killNumberY"].ToString());
            if (documentDictionary.ContainsKey("killNumberZ"))
                StatusManager.instance.killNumberZ = int.Parse(documentDictionary["killNumberZ"].ToString());
            if (documentDictionary.ContainsKey("userName"))
                StatusManager.instance.userName= documentDictionary["userName"].ToString();
            if (documentDictionary.ContainsKey("currentLand"))
                StatusManager.instance.currentLand = int.Parse(documentDictionary["currentLand"].ToString());
            if (documentDictionary.ContainsKey("tbCriDamLv"))
                StatusManager.instance.tbCriDamLv = int.Parse(documentDictionary["tbCriDamLv"].ToString());
            if (documentDictionary.ContainsKey("tbCriRateLv"))
                StatusManager.instance.tbCriRateLv = int.Parse(documentDictionary["tbCriRateLv"].ToString());
            if (documentDictionary.ContainsKey("achievements"))
                StatusManager.instance.achievements = documentDictionary["achievements"].ToString();
            if (documentDictionary.ContainsKey("canNameChange"))
                StatusManager.instance.canNameChange = documentDictionary["canNameChange"].ToString();
            if (documentDictionary.ContainsKey("land1failed"))
                StatusManager.instance.land1failed = documentDictionary["land1failed"].ToString();
            if (documentDictionary.ContainsKey("land2failed"))
                StatusManager.instance.land2failed = documentDictionary["land2failed"].ToString();
            if (documentDictionary.ContainsKey("additionalSlotUnlock"))
                StatusManager.instance.additionalSlotUnlock = int.Parse(documentDictionary["additionalSlotUnlock"].ToString());
            if (documentDictionary.ContainsKey("fairyUnLock"))
                StatusManager.instance.fairyUnLock = documentDictionary["fairyUnLock"].ToString();
            if (documentDictionary.ContainsKey("currentFairy"))
                StatusManager.instance.currentFairy = int.Parse(documentDictionary["currentFairy"].ToString());
            if (documentDictionary.ContainsKey("currentFairy1"))
                StatusManager.instance.currentFairy1= int.Parse(documentDictionary["currentFairy1"].ToString());
            if (documentDictionary.ContainsKey("fairySkillSGP"))
                StatusManager.instance.fairySkillSGP = documentDictionary["fairySkillSGP"].ToString();
            if (documentDictionary.ContainsKey("fairySkillCCC"))
                StatusManager.instance.fairySkillCCC = documentDictionary["fairySkillCCC"].ToString();
            if (documentDictionary.ContainsKey("teethUpgrade"))
                StatusManager.instance.teethUpgrade = documentDictionary["teethUpgrade"].ToString();
            if (documentDictionary.ContainsKey("avatarUnlock"))
                StatusManager.instance.avatarUnlock=documentDictionary["avatarUnlock"].ToString();
            if (documentDictionary.ContainsKey("myWord"))
                StatusManager.instance.myWord = documentDictionary["myWord"].ToString();
            if (documentDictionary.ContainsKey("canNameReset"))
                StatusManager.instance.canNameReset = documentDictionary["canNameReset"].ToString();
            if (documentDictionary.ContainsKey("avatar"))
                StatusManager.instance.avatar = documentDictionary["avatar"].ToString();
            if (documentDictionary.ContainsKey("homeworkDone"))
                StatusManager.instance.homeworkDone=documentDictionary["homeworkDone"].ToString();
            if (documentDictionary.ContainsKey("today"))
                StatusManager.instance.today = documentDictionary["today"].ToString();
            if (documentDictionary.ContainsKey("homeworkReward"))
                StatusManager.instance.homeworkReward = documentDictionary["homeworkReward"].ToString();
            if (documentDictionary.ContainsKey("homeworkTarget"))
                StatusManager.instance.homeworkTarget = documentDictionary["homeworkTarget"].ToString();
            if (documentDictionary.ContainsKey("events"))
                StatusManager.instance.events = documentDictionary["events"].ToString();
            if (documentDictionary.ContainsKey("adReapeated"))
                StatusManager.instance.adRepeated = int.Parse(documentDictionary["adReapeated"].ToString());
            if (documentDictionary.ContainsKey("firstBuy"))
                StatusManager.instance.firstBuy = documentDictionary["firstBuy"].ToString();

            //    //if (documentDictionary["uid"] != null)

            StatusManager.instance.fireStoreLoaded = true;

            ;            //Debug.Log("스냅샷직후 하트:" + StatusManager.instance.heartCount);
            //HeartRechargerManager.instance.Init();
            //GameObject.Find("TagPlate").GetComponent<TagPlateManager>().CheckUpdate();

            


        }
        else
            Debug.Log("Documentdoes not exist!");

        StatusManager.instance.fireStoreLoaded = true;
    }

    public async void LoadLand1TopScore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land1");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            StatusManager.instance.land1Record = documentDictionary["land1Record"].ToString();
            
            for (int i = 0; i < StatusManager.instance.land1Topscore.Length; i++)
            {
                StatusManager.instance.land1Topscore[i] = documentDictionary[i.ToString()].ToString();
            }
            
        }
    }
    public async void LoadLand2TopScore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land2");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            StatusManager.instance.land2Record = documentDictionary["land2Record"].ToString();
            
            for (int i = 0; i < StatusManager.instance.land2Topscore.Length; i++)
            {
                StatusManager.instance.land2Topscore[i] = documentDictionary[i.ToString()].ToString();
            }
            
        }
    }

    public void SaveLand1TopScore(string key, string value)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land1");


        topScore = new Dictionary<string, object>
        {
            { key, value }
            
        };
        
        docRef.UpdateAsync(topScore).ContinueWithOnMainThread(task => {
            Debug.Log("탑스코어 업데이트");
        });
    }

    public void SaveLand2TopScore(string key, string value)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land2");


        topScore = new Dictionary<string, object>
        {
            { key, value }

        };

        docRef.UpdateAsync(topScore).ContinueWithOnMainThread(task => {
            Debug.Log("탑스코어 업데이트");
        });
    }

    public void SaveLand1Record(string value)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land1");

        topScore = new Dictionary<string, object>
        {
            { "land1Record", value }

        };

        docRef.UpdateAsync(topScore).ContinueWithOnMainThread(task => {
            Debug.Log("탑레벨 업데이트");
        });
    }

    public void SaveLand2Record(string value)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("topscore").Document("land2");

        topScore = new Dictionary<string, object>
        {
            { "land2Record", value }

        };

        docRef.UpdateAsync(topScore).ContinueWithOnMainThread(task => {
            Debug.Log("탑레벨 업데이트");
        });
    }



}