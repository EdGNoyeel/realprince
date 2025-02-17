using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageNumbBtn : MonoBehaviour
{
    public string stageName;
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        stageName = GameManager.instance.currentStageName;
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = stageName;
        
    }

    public void CheckTitle()
    {
        stageName = GameManager.instance.currentStageName;
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = stageName;
    }

    // Update is called once per frame
    void Update()
    {
        if (stageName != GameManager.instance.currentStageName)
            CheckTitle();
    }
}
