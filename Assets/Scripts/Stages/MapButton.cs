using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapButton : MonoBehaviour
{
    GameObject stageManager;
    public int stageNumb = 0;
    public string bgmName = "Dracula";
    public string stageName = "0";
    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager");
        if(stageName !=null)
            GetComponentInChildren<TextMeshProUGUI>().text = stageName;
    }


    public void ClickButton()
    {
        stageManager.GetComponent<StageManager>().SettingStage(stageNumb);
        GameManager.instance.landStageNumb = stageNumb;
        GameManager.instance.bgmName = bgmName;
        StatusManager.instance.stageNumb[StatusManager.instance.currentLand] = stageNumb;
        GameManager.instance.LandPositionSave();
        //GameManager.instance.GameOpen
        GameObject lsh_head = GameObject.Find("LSH_Head_prefab");
        lsh_head.GetComponent<LSH_head>().justMove = false;
        //Debug.Log(lsh_head.transform.position);
        lsh_head.GetComponent<LSH_head>().SetTarget(this.transform.position);
        

    }




}
