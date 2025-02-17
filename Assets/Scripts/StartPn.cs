using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartPn : MonoBehaviour
{
    [SerializeField]
    GameObject[] slot;
    [SerializeField]
    TextMeshProUGUI titleTMP;
    [SerializeField]
    TextMeshProUGUI storyTMP;
    [SerializeField]
    TextMeshProUGUI vicCondyTMP;
    //[SerializeField]
    //TextMeshProUGUI recomPowerTMP;
    //[SerializeField]
    //TextMeshProUGUI myPowerTMP;
    [SerializeField]
    GameObject[] monsterThumbPos;
    [SerializeField]
    GameObject[] monsterThumbNailPrefabs;
    public GameObject[] monsterThums;
    string title;
    string story;
    string vicCondi;
    string recomPower="123456";
    [SerializeField]
    TextMeshProUGUI nickName;
    [SerializeField]
    TextMeshProUGUI line;
    [SerializeField]
    TextMeshProUGUI difficulty;
    [SerializeField]
    TextMeshProUGUI difficultyUI;
    [SerializeField]
    TextMeshProUGUI topLevel;
    [SerializeField]
    GameObject avatar;
    //string myPower ="123456";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {

        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].SetActive(false);
        }
        for (int i = 0; i < StatusManager.instance.additionalSlotUnlock; i++)
        {
            slot[i].SetActive(true);
        }

        title = GameObject.Find("StageManager").GetComponentInChildren<EnemyCreater>().stageName;
        titleTMP.text = title;
        vicCondi= GameObject.Find("StageManager").GetComponentInChildren<EnemyCreater>().vicCondi;
        vicCondyTMP.text = "승리조건:"+ vicCondi;
        story= GameObject.Find("StageManager").GetComponentInChildren<EnemyCreater>().story;
        storyTMP.text = story;

        int currentLand = StatusManager.instance.currentLand;

        string[] newArr2 = null;
        string[] newArr = null;
        string[] newArr3 = null;
        string topScore = null;

        if (currentLand == 0)
        {
            newArr2 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            topScore = StatusManager.instance.land1Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];
            newArr3 = StatusManager.instance.land1Record.Split(new char[] { ',' });
        }
        if (currentLand == 1)
        {
            newArr2 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            topScore = StatusManager.instance.land2Topscore[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];
            newArr3 = StatusManager.instance.land2Record.Split(new char[] { ',' });
        }


        int newDifficuly = int.Parse(newArr2[GameManager.instance.landStageNumb]) + 1;
        difficulty.text = " x " + newDifficuly.ToString();
        difficultyUI.text=" x "+newDifficuly.ToString();

        
        newArr= topScore.Split(new char[] { ':' });
        nickName.text = "[" + newArr[0] + "]";
        line.text = newArr[1];
        PrefabManager.instance.LoadPrefabs(newArr[2], avatar);
        for (int i = 0; i < monsterThums.Length; i++)
        {
            if(monsterThums[i] != null)
            {
                Destroy(monsterThums[i]);
            }
        }
        
        topLevel.text = newArr3[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb];

        avatar.GetComponentInChildren<ImgPrefabs>().Name1(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Name2(newArr[0]);
        avatar.GetComponentInChildren<ImgPrefabs>().Word(newArr[1]);
        avatar.GetComponentInChildren<ImgPrefabs>().Level(newArr3[GameObject.FindGameObjectWithTag("stage").GetComponent<EnemyCreater>().stageNumb]);



        bool[] presentMonster = GameObject.Find("StageManager").GetComponentInChildren<EnemyCreater>().presentMonster;

        int j = 0;
        //int h = 0;
        for (int i = 0; i < presentMonster.Length; i++)
        {
            
            if (presentMonster[i])
            {
                monsterThums[i] = Instantiate(monsterThumbNailPrefabs[i]);
                monsterThums[i].transform.position = new Vector3(0, 0, 0);
                monsterThums[i].transform.SetParent(monsterThumbPos[j].transform, false);
                j++;
            }
        }


    }

    /*void CheckTopAvatarTexts()
    {
        avatar.GetComponentInChildren<ImgPrefabs>().Name1(StatusManager.instance.userName);
        avatar.GetComponentInChildren<ImgPrefabs>().Name2(StatusManager.instance.userName);
        avatar.GetComponentInChildren<ImgPrefabs>().Word(StatusManager.instance.myWord);
        avatar.GetComponentInChildren<ImgPrefabs>().Level("");
    }*/
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
