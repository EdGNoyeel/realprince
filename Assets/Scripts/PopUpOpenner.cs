using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOpenner : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PopUp(GameObject popUp)
    {
        if (popUp.activeSelf == true)
        {
            popUp.SetActive(false);
            StatusManager.instance.canPopUpInStarterPN = true;
        }
        else
        {
            if (StatusManager.instance.canPopUpInStarterPN)
            {
                popUp.SetActive(true);
                StatusManager.instance.canPopUpInStarterPN = false;
            }
            
        }
    }

    // Update is called once per frame
    
}
