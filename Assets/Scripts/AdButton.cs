using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
    [SerializeField]
    Button button;
    public bool canEnable;
    // Update is called once per frame
    void Update()
    {
        if (AdmobObj.instance.loaded && canEnable)
        {
            button.interactable = true;
        }
        else
            button.interactable = false;
    }
    public void UnEnable()
    {
        canEnable = false;
    }
}
