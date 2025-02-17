using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopScoreUpdate : MonoBehaviour
{

    public TMP_InputField newLineTMP;
    public Button recordBTN;
    [SerializeField]
    TextMeshProUGUI myNameTMP;
    [SerializeField]
    TextMeshProUGUI topNameTMP;
    [SerializeField]
    TextMeshProUGUI topLineTMP;
    [SerializeField]
    TextMeshProUGUI topDifficultyTMP;
    [SerializeField]
    TextMeshProUGUI yourLevelTMP;
    [SerializeField]
    TextMeshProUGUI vicMent;
    [SerializeField]
    GameObject avatar;

    void OnEnable()
    {
        
    }

    public void SetTopScore()
    {
        int currentLand = StatusManager.instance.currentLand;
        myNameTMP.text = StatusManager.instance.userName;
        string[] newArr = null;
        string[] newArr1 = null;
        string[] newArr3 = null;
        string topScore = null;

        if (currentLand == 0)
        {
            newArr1 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            newArr3 = StatusManager.instance.land1Record.Split(new char[] { ',' });
            topScore = StatusManager.instance.land1Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];

        }
        if (currentLand == 1)
        {
            newArr1 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            newArr3 = StatusManager.instance.land2Record.Split(new char[] { ',' });
            topScore = StatusManager.instance.land2Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];

        }


        int newDifficuly = int.Parse(newArr3[GameManager.instance.landStageNumb]);
        topDifficultyTMP.text = newDifficuly.ToString();
        yourLevelTMP.text = "너는 지금" + newArr1[GameManager.instance.landStageNumb];
        vicMent.text = GameObject.Find("StageManager").GetComponentInChildren<EnemyCreater>().vicMent;
        newArr = topScore.Split(new char[] { ':' });
        topNameTMP.text = "[" + newArr[0] + "]";
        topLineTMP.text = newArr[1];
        PrefabManager.instance.LoadPrefabs(newArr[2], avatar);
        //Debug.Log(newArr[1]);

        avatar.GetComponentInChildren<ImgPrefabs>().Name1(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Name2(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Word(newArr[1]);
        avatar.GetComponentInChildren<ImgPrefabs>().Level(newArr3[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb]);
        Debug.Log(avatar.GetComponentInChildren<ImgPrefabs>().word.text);

        StartCoroutine("CheckAvatar");
    }

    IEnumerator CheckAvatar()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        int currentLand = StatusManager.instance.currentLand;
        myNameTMP.text = StatusManager.instance.userName;
        string[] newArr = null;
        string[] newArr1 = null;
        string[] newArr3 = null;
        string topScore = null;

        if (currentLand == 0)
        {
            newArr1 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            newArr3 = StatusManager.instance.land1Record.Split(new char[] { ',' });
            topScore = StatusManager.instance.land1Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];

        }
        if (currentLand == 1)
        {
            newArr1 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            newArr3 = StatusManager.instance.land2Record.Split(new char[] { ',' });
            topScore = StatusManager.instance.land2Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];

        }
        newArr = topScore.Split(new char[] { ':' });
        avatar.GetComponentInChildren<ImgPrefabs>().Name1(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Name2(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Word(newArr[1]);
        avatar.GetComponentInChildren<ImgPrefabs>().Level(newArr3[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb]);
        Debug.Log(avatar.GetComponentInChildren<ImgPrefabs>().word.text);

    }

    public void CheckCanRecord()
    {

        string[] newArr = null;
        int currentLand = StatusManager.instance.currentLand;

        string[] newArr2=null;

        if (currentLand == 0)
        {
            newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().LoadLand1TopScore();
            newArr = StatusManager.instance.land1Record.Split(new char[] { ',' });
        }

        if (currentLand == 1)
        {
            newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().LoadLand2TopScore();
            newArr = StatusManager.instance.land2Record.Split(new char[] { ',' });
        }

        int stageNumb = GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb;

        int topLevel = int.Parse(newArr[stageNumb]);



        int currentLevel = int.Parse(newArr2[stageNumb]);

        if (currentLevel <= topLevel)
        {
            CanNotRecordTopScore();
        }
        if (currentLevel > topLevel)
        {
            CanRecordTopscore();
        }
    }

    void OnDisable()
    {
        Transform[] children = avatar.GetComponentsInChildren<Transform>();

        if (children != null)
        {
            for (int i = 1; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }
    }

    void CanRecordTopscore()
    {
        newLineTMP.text = StatusManager.instance.myWord;
        newLineTMP.interactable = true;
        recordBTN.interactable = true;
    }

    void CanNotRecordTopScore()
    {
        newLineTMP.text = "더 높은 단계를 클리어 해야합니다";
        newLineTMP.interactable = false;
        recordBTN.interactable = false;
    }

    public void UpdateTopScore()
    {
        int currentLand = StatusManager.instance.currentLand;
        string[] newArr = null;
        string[] newArr2 = null;

        if (currentLand == 0)
        {
            GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().LoadLand1TopScore();
            newArr = StatusManager.instance.land1Record.Split(new char[] { ',' });
            newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
        }

        if (currentLand == 1)
        {
            GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().LoadLand2TopScore();
            newArr = StatusManager.instance.land2Record.Split(new char[] { ',' });
            newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
        }



        int stageNumb = GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb;
        int topLevel = int.Parse(newArr[stageNumb]);
        int currentLevel= int.Parse(newArr2[stageNumb]);

        




        if (currentLevel > topLevel)
        {
            string newLine = StatusManager.instance.userName + ":" + newLineTMP.text+":"+StatusManager.instance.avatar+":"+StatusManager.instance.uid;

            if (currentLand == 0)
            {                
                GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().SaveLand1TopScore(
                    GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb.ToString(), newLine);

                StatusManager.instance.land1Topscore[stageNumb] = newLine;
                newArr[stageNumb] = (currentLevel).ToString();

                StatusManager.instance.land1Record = string.Join(",", newArr);
                GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().SaveLand1Record(StatusManager.instance.land1Record);
            }
            if (currentLand == 1)
            {
                GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().SaveLand2TopScore(
                    GameObject.FindWithTag("stage").GetComponent<EnemyCreater>().stageNumb.ToString(), newLine);

                StatusManager.instance.land2Topscore[stageNumb] = newLine;
                newArr[stageNumb] = (currentLevel).ToString();

                StatusManager.instance.land2Record = string.Join(",", newArr);
                GameObject.Find("FireStoreManager").GetComponent<FireStoreManager>().SaveLand2Record(StatusManager.instance.land2Record);
            }
        }

        Transform[] children = avatar.GetComponentsInChildren<Transform>();

        if (children != null)
        {
            for (int i = 1; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }

        /*topNameTMP.text = StatusManager.instance.userName;
        topLineTMP.text = newLineTMP.text;
        topDifficultyTMP.text = "x" + currentLevel.ToString();
        PrefabManager.instance.LoadPrefabs(StatusManager.instance.avatar, avatar);*/

        SetTopScore();
    }

}
