using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdButton : MonoBehaviour
{
    [SerializeField]
    Button button;
    public bool canEnable;
    [SerializeField]
    TextMeshProUGUI buttonTXT;

    // Update is called once per frame

    void Onable()
    {
        Debug.Log("buttonTXT.text1");
        
    }
    void Update()
    {
        if(StatusManager.instance.adRemoved !=0){
            buttonTXT.text="한번 더 기회를 주세요\n제발(무료)";
            //Debug.Log("buttonTXT.text");
        }

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
