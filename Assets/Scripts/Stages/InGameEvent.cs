using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEvent : MonoBehaviour
{
    public GameObject eventPrepab=null;
    public string bgm = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject position=GameObject.Find("StoryBoard");
        if (eventPrepab != null)
        {
            GameObject inGameStory=GameObject.Instantiate(eventPrepab, position.transform);
            inGameStory.transform.SetParent(position.transform, false);

        }
        if(bgm != null)
        {
            GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM(bgm);
        }
    }
}
