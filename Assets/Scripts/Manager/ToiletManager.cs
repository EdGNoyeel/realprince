using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ToiletManager : MonoBehaviour
{
    static public ToiletManager instance;
    #region singleton

    
    void Awake()//객체 생성시 최초 실행1
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);
    }
    #endregion singleton
    // Start is called before the first frame update

    /*[SerializeField]
    GameObject MyPage;*/

    [SerializeField]
    GameObject DailyPresent;
    [SerializeField]
    GameObject SetNamePN;
    [SerializeField]
    GameObject myPage;
    [SerializeField]
    GameObject announcement;
    void Start()
    {
        if (StatusManager.instance.firstLogin)
        {
            announcement.SetActive(true);
            StatusManager.instance.firstLogin = false;
        }
        Time.timeScale = 1;
        //MyPage.SetActive(false);
        GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM("Country");
        HeartRechargerManager.instance.Init();
        GameObject.Find("TagPlate").GetComponent<TagPlateManager>().CheckUpdate();

        //Invoke("SetNamePNOpen", 0.2f);
        SetNamePNOpen();

    }

    public void SetNamePNOpen()
    {
        if (StatusManager.instance.canNameChange == "1")
        {
            StoryManager.instance.RunStory("intro");
            Invoke("RealOpenSetNamePN", 0.5f);
        }
        else
            SetNamePN.SetActive(false);
        
    }

    void RealOpenSetNamePN()
    {
        SetNamePN.SetActive(true);
    }

    void OnEnable()
    {
        if (StatusManager.instance.dailyPresent == true)
        {
            DailyPresent.SetActive(true);
        }

        if (StatusManager.instance.myPage == true)
        {
            myPage.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("MainStage");
    }
}
