using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Announcement : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI announcement;

    // Start is called before the first frame update
    void Start()
    {
        announcement.text = StatusManager.instance.announcement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
