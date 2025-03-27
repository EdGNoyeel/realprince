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

            Dictionary<string, object> userData = new Dictionary<string, object>();
            var status = StatusManager.instance;

            // reflection을 사용해 StatusManager의 모든 int와 string 필드 가져오기
            var fields = typeof(StatusManager).GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(int) || field.FieldType == typeof(string))
                {
                    userData[field.Name] = field.GetValue(status);
                }
            }

            // Firestore에 저장
            docRef.SetAsync(userData, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("StatusManager 저장 완료");
                }
                else
                {
                    Debug.LogError("Firestore 저장 실패: " + task.Exception);
                }
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
        Debug.Log("Load Status: " + StatusManager.instance.uid);
        DocumentReference docRef = db.Collection("users").Document(StatusManager.instance.uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Debug.Log("스냅샷 로딩됨");
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            var status = StatusManager.instance;
            var fields = typeof(StatusManager).GetFields();

            foreach (var field in fields)
            {
                if (documentDictionary.ContainsKey(field.Name))
                {
                    try
                    {
                        if (field.FieldType == typeof(int))
                        {
                            field.SetValue(status, Convert.ToInt32(documentDictionary[field.Name]));
                        }
                        else if (field.FieldType == typeof(string))
                        {
                            field.SetValue(status, documentDictionary[field.Name].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"필드 {field.Name} 로드 실패: {ex.Message}");
                    }
                }
            }
        }
        else
        {
            Debug.Log("Document does not exist!");
        }

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