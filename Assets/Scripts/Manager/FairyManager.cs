using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyManager : MonoBehaviour
{
    [SerializeField] GameObject[] fairy;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fairy.Length; i++)
        {
            if(fairy[i] != null)
            {
                fairy[i].gameObject.SetActive(false);
            }
        }
        CheckFairy();
    }

    // Update is called once per frame
    public void CheckFairy()
    {
        string[] arr = StatusManager.instance.fairyUnLock.Split(new char[] { ',' });

        for (int i = 0; i < fairy.Length; i++)
        {
            if(fairy[i] !=null)
                fairy[i].SetActive(false);
        }

        if (arr[StatusManager.instance.currentFairy] == "1")
        {
            if (fairy[StatusManager.instance.currentFairy] != null)
            {
                fairy[StatusManager.instance.currentFairy].SetActive(true);
            }
                
        }
        
        if(arr[StatusManager.instance.currentFairy1] == "1")
        {
            if(fairy[StatusManager.instance.currentFairy1] != null)
            {
                fairy[StatusManager.instance.currentFairy1].SetActive(true);
            }
            
        }
        
    }

}
