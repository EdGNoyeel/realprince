using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyPage : MonoBehaviour
{
    [SerializeField]
    GameObject avatar;
    [SerializeField]
    GameObject myNameTMP;
    [SerializeField]
    GameObject myWordTMP;
    [SerializeField]
    GameObject newWord;
    [SerializeField]
    GameObject resetName;
    [SerializeField]
    GameObject mySelf;
    [SerializeField]
    GameObject canNotChangePN;
    [SerializeField]
    GameObject[] avatartButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        PrefabManager.instance.LoadPrefabs(StatusManager.instance.avatar, avatar);
        myNameTMP.GetComponent<TextMeshProUGUI>().text = "내 이름은 " + StatusManager.instance.userName;
        myWordTMP.GetComponent<TextMeshProUGUI>().text = "내 좌우명은 " + StatusManager.instance.myWord;

        string[] arr = StatusManager.instance.avatarUnlock.Split(new char[] { ',' });

        
        for (int i = 0; i < avatartButton.Length; i++)
        {
            if (arr[i] == "1")
            {
                avatartButton[i].GetComponent<Button>().interactable = true;
            }
            else
                avatartButton[i].GetComponent<Button>().interactable = false;
        }
        CheckAvatarTexts();
    }

    public void ChangeNameBuy()
    {
        if (StatusManager.instance.dia >= 10000)
        {
            StatusManager.instance.dia = StatusManager.instance.dia - 10000;
            StatusManager.instance.canNameReset = "1";
        }
    }

    public void ChangeAvatar(string avatarName)
    {
        Transform[] children = avatar.GetComponentsInChildren<Transform>();

        if (children != null)
        {
            for (int i = 1; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }
        StatusManager.instance.avatar = avatarName;
        PrefabManager.instance.LoadPrefabs(StatusManager.instance.avatar, avatar);

        Invoke("CheckAvatarTexts", 0.2f);
    }



    void CheckAvatarTexts()
    {
        avatar.GetComponentInChildren<ImgPrefabs>().Name1(StatusManager.instance.userName);
        avatar.GetComponentInChildren<ImgPrefabs>().Name2(StatusManager.instance.userName);
        avatar.GetComponentInChildren<ImgPrefabs>().Word(StatusManager.instance.myWord);
        avatar.GetComponentInChildren<ImgPrefabs>().Level("");




    }

    public void TryChangeMyWord()
    {
        myWordTMP.SetActive(false);
        newWord.SetActive(true);
      
    }

    public void ChangeMyWord()
    {
        StatusManager.instance.myWord = newWord.GetComponent<TMP_InputField>().text;
        myWordTMP.GetComponent<TextMeshProUGUI>().text = "내 좌우명은 " + StatusManager.instance.myWord;
        avatar.GetComponentInChildren<ImgPrefabs>().Word(StatusManager.instance.myWord);
    }

    void OnDisable()
    {
        Transform[] children=avatar.GetComponentsInChildren<Transform>();
        StatusManager.instance.myPage = false;
        if(children != null)
        {
            for(int i = 1; i < children.Length; i++)
            {
                Destroy(children[i].gameObject);
            }
        }
    }
    public void TryChangeName()
    {
        if (StatusManager.instance.canNameReset == "1")
        {
            resetName.SetActive(true);
            mySelf.SetActive(false);
        }
        else
        {
            canNotChangePN.SetActive(true);
        }
    }
}
