using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentalClinic : MonoBehaviour
{
    [SerializeField]
    GameObject[] fairyPartsBTN;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject bgm = GameObject.Find("BGM_Manager");
        bgm.GetComponent<BGM_Manager>().PlayBGM("CCC");
    }

    // Update is called once per frame
    private void OnEnable()
    {
        string[] arr = StatusManager.instance.fairyUnLock.Split(new char[] { ',' });
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == "1")
            {
                count++;
            }
        }

        for (int i = 0; i < count-2; i++)
        {
            fairyPartsBTN[i].GetComponent<Button>().interactable = true;
        }
    }
}
