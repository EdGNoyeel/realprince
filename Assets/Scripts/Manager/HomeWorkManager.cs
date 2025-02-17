using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Homework
{
    //public string _code;
    //public string _name;
    public string subject;
    public int targetPoint;
    public int currnetPoint;
    public int initialPoint;
    public int realTargetPoint;
    public int rewardNumb;
    public String _req;
    public String _rew;
    
    
    public bool did = false;
    public bool done = false;
    public bool rewarded = false;

}
public class HomeWorkManager : MonoBehaviour
{
    static public HomeWorkManager instance;
    string today;
    public DateTime dt;
    string dayOfWeek;
    int _currentPoint;
    public List<string> homeworkString;
    public Homework model;
    public List<Homework> homeworks;
    [SerializeField]
    int homeworkNumb=0;
    string targetName;
    public string[] homeworkSentence;
    string[] realTartgerPoint;
    bool setNewTargetPoint = false;
    //string homeworkDone;
    // Start is called before the first frame update
    void Awake()
    {      

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);

        //CheckAttendance();

    }

    void Start()
    {
        today = DateTime.Now.Date.ToString();
        if (today != StatusManager.instance.today)
        {
            Debug.Log("오늘의 숙r");
            StatusManager.instance.today = today;
            StatusManager.instance.attendanceCheck = "1";
            StatusManager.instance.homeworkDone = "0,0,0,0,0,0";
            StatusManager.instance.homeworkReward = "0,0,0,0,0,0";
            setNewTargetPoint = true;
            DownLoadHomeWork();
        }
        else
            DownLoadHomeWork();
    }

    void CheckWin(int numb)
    {
        if (homeworkNumb < homeworks.Count)
        {
            string[] arr = StatusManager.instance.homeworkDone.Split(new char[] { ',' });

            if(arr[numb] == "1")
            {
                homeworkSentence[numb] = targetName + homeworks[numb].targetPoint.ToString() + "마리 제거 (" + homeworks[numb].targetPoint.ToString() + "/" + homeworks[numb].targetPoint.ToString() + ")";
                homeworks[numb].done = true;
            }
            else
            {
                if (homeworks[numb].realTargetPoint <= homeworks[numb].currnetPoint)
                {
                    homeworkSentence[numb] = targetName + homeworks[numb].targetPoint.ToString() + "마리 제거 (" + homeworks[numb].targetPoint.ToString() + "/" + homeworks[numb].targetPoint.ToString() + ")";
                    homeworks[numb].done = true;

                    arr[numb] = "1";
                    StatusManager.instance.homeworkDone = string.Join(",", arr);
                }
                else
                {
                    homeworkSentence[numb] = targetName + homeworks[numb].targetPoint.ToString() + "마리 제거 (" + (homeworks[numb].currnetPoint - homeworks[numb].realTargetPoint + homeworks[numb].targetPoint).ToString() + "/" + homeworks[numb].targetPoint.ToString() + ")";
                    //Debug.Log(homeworks[numb].currnetPoint - homeworks[numb].initialPoint);
                    //Debug.Log(homeworks[numb].currnetPoint);
                    //Debug.Log(homeworks[numb].initialPoint);
                    homeworks[numb].done = false;
                }
            }
            homeworkNumb++;
            if (homeworkNumb < homeworks.Count)
                Invoke(homeworks[homeworkNumb].subject, 0);

            else
            {
                realTartgerPoint = StatusManager.instance.homeworkTarget.Split(new char[] { ',' }); 
                for (int i = 0; i < homeworks.Count; i++)
                {
                    realTartgerPoint[i] = homeworks[i].realTargetPoint.ToString();
                }
                homeworkNumb = 0;
                StatusManager.instance.homeworkTarget = String.Join(",", realTartgerPoint);

            }
                
        }
        /*else
            homeworkNumb = 0;*/
    }

    public void DownLoadHomeWork()
    {
        //dayOfWeek=DateTime.Now.DayOfWeek.ToString();
        //GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().LoadHomework(dayOfWeek);
        //Invoke(dayOfWeek, 0);

        homeworkString.Clear();
        for (int i = 0; i < StatusManager.instance.homeworkString.Count; i++)
        {
            homeworkString.Add(StatusManager.instance.homeworkString[i]);
        }
        UpdateHomework();
    }

    public void UpdateHomework()
    {
        /*homeworks.Clear();

        for (int i = 0; i < homeworkString.Count; i++)
        {
            homeworks.Add(model);
        }*/

        for (int i = 0; i < homeworkString.Count; i++)
        {
            string[] arr = homeworkString[i].Split(new char[] { '/' });
            /*model.subject=arr[0];
            model.targetPoint = int.Parse(arr[1]);
            model.rewardNumb = int.Parse(arr[2]);
            homeworks.Add(model);*/
            homeworks[i].subject = arr[0];
            homeworks[i].targetPoint = int.Parse(arr[1]);
            homeworks[i].rewardNumb = int.Parse(arr[2]);
            string[] arr2 = StatusManager.instance.homeworkReward.Split(new char[] { ',' });
            if (arr2[i]=="1")
            {
                homeworks[i].rewarded = true;
            }

        }
        realTartgerPoint = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });



        Invoke(homeworks[0].subject, 0);

            
        


    }

    public void CheckHomeWork()
    {
        homeworkNumb = 0;
        Invoke(homeworks[0].subject, 0);

     
    }
    
    void KillNumberA()
    {
        
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberA;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }
                
            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberA;
        targetName = "포도사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberB()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberB;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberB;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberC()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberC;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberC;
        targetName = "메론사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberD()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberD;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberD;
        targetName = "복숭아사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberE()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberE;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberE;
        targetName = "인삼";

        CheckWin(homeworkNumb);
    }
    void KillNumberF()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberF;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberF;
        targetName = "고기만두";

        CheckWin(homeworkNumb);
    }
    void KillNumberG()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberG;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberG;
        targetName = "김치만두";

        CheckWin(homeworkNumb);
    }
    void KillNumberH()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberH;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberH;
        targetName = "새우튀김";

        CheckWin(homeworkNumb);
    }
    void KillNumberI()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberI;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberI;
        targetName = "계란";

        CheckWin(homeworkNumb);
    }
    void KillNumberJ()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberJ;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberJ;
        targetName = "문어";

        CheckWin(homeworkNumb);
    }
    void KillNumberK()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberK;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberK;
        targetName = "수박";

        CheckWin(homeworkNumb);
    }
    void KillNumberL()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberL;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberL;
        targetName = "멸치";

        CheckWin(homeworkNumb);
    }
    void KillNumberM()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberM;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberM;
        targetName = "돈까스";

        CheckWin(homeworkNumb);
    }
    void KillNumberN()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberN;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberN;
        targetName = "붕어빵";

        CheckWin(homeworkNumb);
    }
    void KillNumberO()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberO;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberO;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberP()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberP;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberP;
        targetName = "떡볶이";

        CheckWin(homeworkNumb);
    }
    void KillNumberQ()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberQ;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberQ;
        targetName = "치킨";

        CheckWin(homeworkNumb);
    }
    void KillNumberR()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberR;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberR;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberS()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberS;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberS;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberT()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberT;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberT;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberU()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberU;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberU;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberV()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberV;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberV;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberW()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberW;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberW;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberX()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberX;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberX;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberY()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberY;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberY;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }
    void KillNumberZ()
    {
        if (!homeworks[homeworkNumb].did)
        {
            homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberZ;
            homeworks[homeworkNumb].initialPoint = homeworks[homeworkNumb].currnetPoint;
            string[] arr = StatusManager.instance.homeworkTarget.Split(new char[] { ',' });
            if (setNewTargetPoint)
            {
                homeworks[homeworkNumb].realTargetPoint = homeworks[homeworkNumb].currnetPoint + homeworks[homeworkNumb].targetPoint;
                arr[homeworkNumb] = homeworks[homeworkNumb].realTargetPoint.ToString();
                StatusManager.instance.homeworkTarget = String.Join(",", arr);
            }

            else
                homeworks[homeworkNumb].realTargetPoint = int.Parse(arr[homeworkNumb]);
            homeworks[homeworkNumb].did = true;
        }

        homeworks[homeworkNumb].currnetPoint = StatusManager.instance.killNumberZ;
        targetName = "레몬사탕";

        CheckWin(homeworkNumb);
    }


}
