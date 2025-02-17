using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    [SerializeField] GameObject[] stageArray = null;
    public GameObject currentStage;
    public int currentStageNumb;
    [SerializeField] GameObject[] stageArray1 = null;

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
        //currentStage = Instantiate(stageArray[StatusManager.instance.stageLevel]);
    }
    public void RemoveStage()
    {
        //.Log("스테이지제거");
        if (currentStage != null)
            Destroy(currentStage);
    }

    public void SettingStage(int stageNum)
    {
        int currentLand = StatusManager.instance.currentLand;
        RemoveStage();
        if (currentLand == 0)
        {
            if (stageNum < stageArray.Length)
            {
                currentStageNumb = stageNum;
                currentStage = Instantiate(stageArray[currentStageNumb]);
                currentStage.transform.SetParent(this.transform, false);
                //currentStage.gameObject.SetActive(true);
                //currentStage.transform.parent = this.transform;
                //Debug.Log(currentStageNumb);
            }
        }

        if (currentLand == 1)
        {
            if (stageNum < stageArray1.Length)
            {
                currentStageNumb = stageNum;
                currentStage = Instantiate(stageArray1[currentStageNumb]);
                currentStage.transform.SetParent(this.transform, false);
                //currentStage.gameObject.SetActive(true);
                //currentStage.transform.parent = this.transform;
                //Debug.Log(currentStageNumb);
            }

        }
        
        
    }

    public void RetryStage()
    {
        TextMeshProUGUI difficultUITMP=GameObject.Find("DifficultyTMP").GetComponent<TextMeshProUGUI>();
        int currentLand = StatusManager.instance.currentLand;

        string[] newArr2 = null;
        //string[] newArr = null;
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
        difficultUITMP.text = "x" + newDifficuly.ToString();
        SettingStage(currentStageNumb);
        /*RemoveStage();
        if(St)
        currentStage= Instantiate(stageArray[currentStageNumb]);*/

              
    }



}
