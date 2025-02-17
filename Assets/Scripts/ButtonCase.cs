using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCase : MonoBehaviour
{
    [SerializeField]
    int land;
    [SerializeField]
    int stage;
    [SerializeField]
    GameObject button;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (land == 0)
        {
            string[] arr = StatusManager.instance.stageUnlock0101.Split(new char[] { ',' });
            if(arr[stage] != "0")
            {
                button.SetActive(true);
            }
            
        }
        if (land == 1)
        {
            string[] arr = StatusManager.instance.stageUnlock0102.Split(new char[] { ',' });
            if (arr[stage] != "0")
            {
                button.SetActive(true);
            }
        }
    }
}
