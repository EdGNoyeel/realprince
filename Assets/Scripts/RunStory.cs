using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStory : MonoBehaviour
{
    public void StartStory(string name)
    {
        StoryManager.instance.RunStory(name);
    }
    
}
