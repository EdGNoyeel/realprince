using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TB_Changer : MonoBehaviour
{
    GameObject selecter;
    // Start is called before the first frame update
    void Start()
    {
        selecter = GameObject.Find("TB_SelecterBTN");
      
    }

    public void SelectionImageChange()
    {
        selecter.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
    }
}
