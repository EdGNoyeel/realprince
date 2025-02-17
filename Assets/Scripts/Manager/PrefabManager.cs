using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StoryPrefabs
{
    public string name;
    public GameObject storyPrefab;
}

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance = null;

    public StoryPrefabs[] storyAssets;
    

    void Awake()//객체 생성시 최초 실행
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);
    }
    

    public void LoadPrefabs(string _name, GameObject _pos)
    {
        for (int i = 0; i < storyAssets.Length; i++)
        {
            if (_name == storyAssets[i].name)
            {

                GameObject _dialogue = GameObject.Find("StoryBoard");
                Debug.Log(_dialogue.name);
                GameObject avatar = Instantiate(storyAssets[i].storyPrefab);
                avatar.transform.position = _pos.transform.position;
                    //new Vector3(_pos.transform.position.x, _pos.transform.position.y, 0);
                avatar.transform.SetParent(_pos.transform, false);
                avatar.GetComponent<ImgPrefabs>().texts.SetActive(true);

                return;
                //    }
                // }
                // Debug.Log("모든 가용 오디오소스가 사용중입니다");
                // return;
            }
        }
        //Debug.Log(_name + "사운드가 사운드매니져에 등록되지 않았습니다");
    }
}
