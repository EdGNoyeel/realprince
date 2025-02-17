using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SetName : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputName;
    [SerializeField]
    TextMeshProUGUI welcomeTMP;
    /*[SerializeField]
    GameObject mySelf;*/
    [SerializeField]
    TextMeshProUGUI direction;
    // Start is called before the first frame update
    /*void Start()
    {
        Invoke("OpenPN", 0.1f);
    }

    void OpenPN()
    {
        if (StatusManager.instance.canNameChange == "1")
        {
            mySelf.SetActive(true);
        }
        else
            mySelf.SetActive(false);
    }*/
    public void Directing()
    {
        direction.text = "정말 [" + inputName.text + "]로 변경하시겠습니까?";
    }
    public void ChangeName()
    {
        StatusManager.instance.userName = inputName.text;
        StatusManager.instance.canNameChange = "0";
        welcomeTMP.text= StatusManager.instance.userName+ " 이/가\n 너의 이름이구나!\n " + StatusManager.instance.userName+ " 은/는\n 좋은 이름이야!";

    }
    public void ResetName()
    {
        StatusManager.instance.userName = inputName.text;
        StatusManager.instance.canNameReset = "0";
        //welcomeTMP.text = StatusManager.instance.userName + " 이/가\n 너의 이름이구나!\n " + StatusManager.instance.userName + " 은/는\n 좋은 이름이야!";
    }
}
