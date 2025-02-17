using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckDiaTXT : MonoBehaviour
{
    TextMeshProUGUI diaTMP;
    // Start is called before the first frame update
    void Start()
    {
        diaTMP = GetComponent<TextMeshProUGUI>();
        diaTMP.text = StatusManager.instance.dia.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        diaTMP.text = StatusManager.instance.dia.ToString();
    }
}
