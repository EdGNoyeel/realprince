using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpLine : MonoBehaviour
{
    float StartTime;
    float sec;
    
    TextMeshProUGUI helpText;
    public string lines;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("도움말생성");
        StartTime = Time.realtimeSinceStartup;

        helpText = GetComponentInChildren<TextMeshProUGUI>();
        helpText.text = lines;
        Invoke("Delete", 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        sec = Time.realtimeSinceStartup - StartTime;
        StartTime = Time.realtimeSinceStartup;
        
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
