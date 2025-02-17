using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialEvents
{
    [SerializeField]
    string name;
    [SerializeField]
    public GameObject tutorialPN;
    [SerializeField]
    public int land;
    [SerializeField]
    public int stage;
}
public class TutorialOpener : MonoBehaviour
{
    [SerializeField]
    public TutorialEvents[] tutorialEvents;
    // Start is called before the first frame update
    void OnEnable()
    {
        int currentLand = StatusManager.instance.currentLand;
        int currentStage = GameManager.instance.stageNumb[currentLand];
        string clear = "0";
        if (currentLand == 0)
        {
            string[] arr0 = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            clear = arr0[currentStage];
        }
        else if (currentStage == 1)
        {
            string[] arr0 = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            clear = arr0[currentStage];
        }
        for (int i = 0; i < tutorialEvents.Length; i++)
        {
            if (tutorialEvents[i].land == currentLand && tutorialEvents[i].stage == currentStage)
            {
                if (clear == "1")
                {
                    tutorialEvents[i].tutorialPN.SetActive(true);
                }
                
            }
        }
    }
}
