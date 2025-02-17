using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagPlateManager : MonoBehaviour
{
    public GameObject userName;
    
    // Start is called before the first frame update
    void Start()
    {
        CheckUpdate();
    }

    public void CheckUpdate()
    {
        userName.GetComponent<TextMeshProUGUI>().text = StatusManager.instance.userName;
    }   

    
}
