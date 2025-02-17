using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievements
{
    //public string _code;
    //public string _name;
    
    public GameObject achievePrefab;
    public bool unlock = false;
    public float progress;
    //[SerializeField]
    //AchievePrefab achieve;
    

}

[System.Serializable]
public class Unlocks
{
    public string _code;
    public string _name;
    public bool unlock = false;
}

public class AchievementsManager : MonoBehaviour
{
    static public AchievementsManager instance;
    public Achievements[] achievements;
    string[] achievenmentString;
    public Unlocks[] unlocks;
    string[] unlockString;
    public string[] condi;
    public List<Achievements> achievementsList;
    public string fullList;
    public List<string> condiList;

    void Awake()
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
    void Start()
    {
        achievenmentString = StatusManager.instance.achievements.Split(new char[] { ',' });
        unlockString = StatusManager.instance.unlocks.Split(new char[] { ',' });
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i].unlock = false;            
        }

        for (int i = 0; i < unlocks.Length; i++)
        {
            if (unlockString[i] == "1")
            {
                unlocks[i].unlock = true;
            }
        }
        
    }
    public void CheckUnlocks()
    {
        achievenmentString = StatusManager.instance.achievements.Split(new char[] { ',' });
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievenmentString[i] == "0")
            {
                achievements[i].unlock = false;
                achievements[i].achievePrefab.GetComponent<AchievePrefab>().canUnlock = true;
;           }
            else
            {
                achievements[i].unlock = true;
                achievements[i].achievePrefab.GetComponent<AchievePrefab>().canUnlock = false;
            }

            //if (!achievements[i].unlock)
            //{
                achievements[i].achievePrefab.GetComponent<AchievePrefab>().CheckUnlock(i);
            //}
        }

        achievementsList = new List<Achievements>();
        

        for (int k = 0; k < achievements.Length; k++)
        {
            achievementsList.Add(achievements[k]);
        }

        for (int j = 0; j < achievementsList.Count; j++)
        {
            achievements[j].progress = achievements[j].achievePrefab.GetComponent<AchievePrefab>().progress;
        }
        MakeCondiList();
        
    }

    public void MakeCondiList()
    {
        //var dic = new Dictionary<float, string>();

        /*achievementsList.Sort(delegate (Achievements A, Achievements B)
        {
            if (A.progress < B.progress) return 1;
            else if (A.progress > B.progress) return -1;
            return 0; //동일한 값일 경우
        });*/
        achievementsList.Sort((x, y) =>
        {
            return x.progress.CompareTo(y.progress);
        });

        achievementsList.Reverse();

        


        

        for (int i = 0; i < achievements.Length; i++)
        {
            //dic.Add(achievements[i].achievePrefab.GetComponent<AchievePrefab>().progress, achievements[i].achievePrefab.GetComponent<AchievePrefab>().condi);
            condi[i] = achievements[i].achievePrefab.GetComponent<AchievePrefab>().condi;
        }

        condiList.Clear();

        for (int j = 0; j < achievementsList.Count; j++)
        {
            //int k = 0;
            if(achievementsList[j].achievePrefab.GetComponent<AchievePrefab>().condi != null && !achievementsList[j].unlock)
            {
                condiList.Add(achievementsList[j].achievePrefab.GetComponent<AchievePrefab>().condi);
            }
            
        }
        

       /* for (int j = 0; j < achievementsList.Count; j++)
        {
            if(condiList != null)
            {
                condiList.Add(achievementsList[j].achievePrefab.GetComponent<AchievePrefab>().condi);
            }
            
        }*/
        fullList = string.Join("\r\n", condiList);
    }
}
