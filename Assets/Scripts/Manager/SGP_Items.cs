using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGP_Items : MonoBehaviour
{
    GameObject statusM;
    // Start is called before the first frame update
    void Start()
    {
        statusM = GameObject.Find("StatusManager");
    }

    // Update is called once per frame

    public void SGP_Codi(int numb)
    {
        //Debug.Log("코디");
        if (statusM.GetComponent<StatusManager>().sGP_Item[numb] == true)
            statusM.GetComponent<StatusManager>().sGP_Item[numb] = false;
        else
            statusM.GetComponent<StatusManager>().sGP_Item[numb] = true;
    }

}
