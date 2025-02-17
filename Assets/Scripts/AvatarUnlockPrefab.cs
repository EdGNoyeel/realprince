using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvatarUnlockPrefab : MonoBehaviour
{

    [SerializeField]
    GameObject prefabPos;

    [SerializeField]
    GameObject myself;
    string newAvatarName;
    // Start is called before the first frame update
    

    public void LoadAvatarPrefab(string name)
    {
        PrefabManager.instance.LoadPrefabs(name, prefabPos);
        newAvatarName = name;
        prefabPos.GetComponentInChildren<ImgPrefabs>().Name1(StatusManager.instance.userName);
        prefabPos.GetComponentInChildren<ImgPrefabs>().Name2(StatusManager.instance.userName);
        prefabPos.GetComponentInChildren<ImgPrefabs>().Word(StatusManager.instance.myWord);
    }

    public void ChangeMyAvatar()
    {
        StatusManager.instance.avatar = newAvatarName;
    }

    public void GoToMypage()
    {
        StatusManager.instance.myPage = true;
    }

    void OnDisable()
    {
        Transform[] trash=prefabPos.GetComponentsInChildren<Transform>();
        if(trash != null)
        {
            for(int i = 1; i < trash.Length; i++)
            {
                if(trash[i] != transform)
                {
                    Destroy(trash[i].gameObject);
                }
            }
        }
    }

    /*public void UnlockAvatar(int numb)
    {
        string[] arr= StatusManager.instance.avatarUnlock.Split(new char[] { ',' });
        arr[numb] = "1";
        StatusManager.instance.avatarUnlock= string.Join(",", arr);
    }*/

    public void KillMyself()
    {
        Destroy(myself);
    }
}
