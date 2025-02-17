using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleLine : MonoBehaviour
{
    public TextMeshProUGUI simple;
    // Start is called before the first frame update
    void Start()
    {
        //simple = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLine(string text)
    {
        simple.text = text;
    }

    
}
