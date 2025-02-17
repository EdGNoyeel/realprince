using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void RunStory(string story)
    {        
        GameObject.Find("StoryManager").GetComponent<StoryManager>().RunStory(story);
    }
}
