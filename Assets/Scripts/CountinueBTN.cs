using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountinueBTN : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    Button button;

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
            button.interactable = true;
        else
            button.interactable = false;
    }
}
