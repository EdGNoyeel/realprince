using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairySelectionPrefab : MonoBehaviour
{
    [SerializeField]
    int myNum;
    [SerializeField]
    Image btnImage;

    void OnEnable()
    {
        string[] arr=StatusManager.instance.fairyUnLock.Split(new char[] { ',' });
        if(arr[myNum] == "0" ||StatusManager.instance.currentFairy1==myNum)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
            gameObject.GetComponent<Button>().interactable = true;
    }
    public void Click()
    {
        StatusManager.instance.currentFairy = myNum;
        GameObject.Find("FairyManager").GetComponent<FairyManager>().CheckFairy();
        btnImage.sprite = gameObject.GetComponent<Image>().sprite;
    }
   
}
