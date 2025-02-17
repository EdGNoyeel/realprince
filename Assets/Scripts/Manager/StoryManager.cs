using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Storys
{
    public string name;
    //public TextAsset story;
    public GameObject storyEvent;
}

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance = null;    
    //public string[] storyName;
    public Storys[] storyAssets;
    //[SerializeField]
    //GameObject theDialogue;
    //public GameObject dialogue;

    void Awake()//객체 생성시 최초 실행
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
        //dialogue = GameObject.Find ("Dialogue");
    }

    public void RunStory(string _name)
    {
        for (int i = 0; i < storyAssets.Length; i++)
        {
            Debug.Log(_name);
            /*if (_name == storyAssets[i].name)
            {

                GameObject makeStory = Instantiate(theDialogue);
                makeStory.transform.SetParent(GameObject.Find("StoryBoard").transform, false);

            
                makeStory.GetComponent<Dialogue>().dialogue = storyAssets[i].story;
                makeStory.SetActive(true);
                return;
                //    }
                // }
                // Debug.Log("모든 가용 오디오소스가 사용중입니다");
                // return;
            }*/
            if (_name == storyAssets[i].name)
            {
                Debug.Log(_name);
                GameObject makeStory = Instantiate(storyAssets[i].storyEvent);
                makeStory.transform.SetParent(GameObject.Find("StoryBoard").transform, false);
                makeStory.SetActive(true);
                return;
            }
        }
        //Debug.Log(_name + "사운드가 사운드매니져에 등록되지 않았습니다");
    }
}
