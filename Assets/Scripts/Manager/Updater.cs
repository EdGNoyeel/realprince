using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    //public static Updater instance = null;
    public bool test;
    public bool ios;
    public bool needUpdate=false;
    public int thisVersion;

    [SerializeField]
    private string iosURL;
    [SerializeField]
    private string androidURL;
    [SerializeField]
    GameObject updatePN;
    private bool opened=false;
    // Start is called before the first frame update
    /*private void Awake()
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
    }*/

    /*void Update()
    {
        if (needUpdate)
        {
            if (!opened)
            {
                updatePN.SetActive(true);
                opened = true;
                Debug.Log("버전테스트");
            }
        }
        
    }*/
    void Start()
    {
        if (test)
        {
            if(thisVersion < int.Parse(StatusManager.instance.testVersion))
            {
                updatePN.SetActive(true);
            }

        }
        else
        {
            if (thisVersion < int.Parse(StatusManager.instance.currentVersion))
            {
                updatePN.SetActive(true);
            }
        }
    }
    public void GotoUpdate()
    {
        if (ios)
        {
            Application.OpenURL(iosURL);
        }
        else
            Application.OpenURL(androidURL);
    }

    // Update is called once per frame
    
}
