using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StoryLine
{
    public string name;
    public string line;
    public GameObject prefabA;
    public GameObject prefabB;
    public GameObject prefabC;
    public GameObject eventPrefab;
    public Image backGround;
    public string bgm="0";
    public bool highLightA = true;
    public bool highLightB = true;
    public bool highLightC = true;
    public bool haselection = false;

}

[System.Serializable]
public class Selections
{
    public string[] selectionLine;
}

public class StoryEvent : MonoBehaviour
{
    public int currentLine=0;
    public int currentSelection = 0;
    public GameObject selectionWindow;
    public StoryLine[] storyLine;
    public Selections[] selections;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI lineTMP;
    public TextMeshProUGUI[] selectionTMP;
    public Image backGround;
    public GameObject prefabApos;
    public GameObject prefabBpos;
    public GameObject prefabCpos;
    float originTimeScale;
    /*public GameObject prefabA;
    public GameObject prefabB;
    public GameObject prefabC;*/
    GameObject objA;
    GameObject objB;
    GameObject objC;
    public GameObject eventPrefabPos;
    [SerializeField]
    GameObject mySelf;
    AudioClip originBGM;
    // Start is called before the first frame update
    void Start()
    {
        
        NextLine();
        originBGM = GameObject.Find("BGM_Manager").GetComponent<AudioSource>().clip;
        selectionWindow.SetActive(false);
        originTimeScale=Time.timeScale;
        Time.timeScale = 0;
    }

    public void ChangeLineNumb(int numb)
    {
        currentLine = numb;
    }

    public void NextLine()
    {
        selectionWindow.SetActive(false);
        int numb = currentLine;
        currentLine++;
        if (numb == storyLine.Length)
        {
            if(GameObject.Find("BGM_Manager").GetComponent<AudioSource>().clip != originBGM)
            {
                if(originBGM != null)
                {
                    GameObject.Find("BGM_Manager").GetComponent<AudioSource>().clip = originBGM;
                    GameObject.Find("BGM_Manager").GetComponent<AudioSource>().Play();
                }
                
            }
            
            Time.timeScale = originTimeScale;
            StatusManager.instance.currentStory = "0";
            Destroy(mySelf);
            
        }
        else
        {
            if (storyLine[numb].name != null)
            {
                nameTMP.text = storyLine[numb].name;
            }
            if (storyLine[numb].line != null)
            {
                lineTMP.text = storyLine[numb].line;
            }
            if (storyLine[(numb)].prefabA != null)
            {
                Transform[] childListA = prefabApos.GetComponentsInChildren<Transform>();
                if (childListA != null)
                {
                    for (int i = 1; i < childListA.Length; i++)
                    {
                        if (childListA[i] != transform)
                            Destroy(childListA[i].gameObject);
                    }
                    objA = Instantiate(storyLine[(numb)].prefabA);
                    objA.transform.parent = prefabApos.transform;
                    objA.transform.position = prefabApos.transform.position;
                    objA.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (storyLine[(numb)].prefabB != null)
            {
                Transform[] childListB = prefabBpos.GetComponentsInChildren<Transform>();
                if (childListB != null)
                {
                    for (int i = 1; i < childListB.Length; i++)
                    {
                        if (childListB[i] != transform)
                            Destroy(childListB[i].gameObject);
                    }
                    objB = Instantiate(storyLine[(numb)].prefabB);
                    objB.transform.parent = prefabBpos.transform;
                    objB.transform.position = prefabBpos.transform.position;
                    objB.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (storyLine[(numb)].prefabC != null)
            {
                Transform[] childListC = prefabCpos.GetComponentsInChildren<Transform>();
                if (childListC != null)
                {
                    for (int i = 1; i < childListC.Length; i++)
                    {
                        if (childListC[i] != transform)
                            Destroy(childListC[i].gameObject);
                    }
                    objC = Instantiate(storyLine[(numb)].prefabC);
                    objC.transform.parent = prefabCpos.transform;
                    objC.transform.position = prefabCpos.transform.position;
                    objC.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            if (storyLine[numb].eventPrefab != null)
            {
                Transform[] childListE = eventPrefabPos.GetComponentsInChildren<Transform>();
                if (childListE != null)
                {
                    for (int i = 1; i < childListE.Length; i++)
                    {
                        if (childListE[i] != transform)
                            Destroy(childListE[i].gameObject);
                    }
                    GameObject eVent = Instantiate(storyLine[(numb)].eventPrefab);
                    eVent.transform.parent = eventPrefabPos.transform;
                    eVent.transform.position = eventPrefabPos.transform.position;
                    eVent.transform.localScale = new Vector3(1, 1, 1);
                }
            }

            if (storyLine[numb].backGround != null)
            {
                backGround.sprite = storyLine[numb].backGround.sprite;
            }
            if (storyLine[numb].bgm != "0")
            {
                GameObject.Find("BGM_Manager").GetComponent<BGM_Manager>().PlayBGM(storyLine[numb].bgm);
            }
            if (objA != null)
            {
                if (storyLine[numb].highLightA == true)
                    objA.GetComponent<ImgPrefabs>().HighLight();
                else
                    objA.GetComponent<ImgPrefabs>().LowLight();
            }
            if (objB != null)
            {
                if (storyLine[numb].highLightB == true)
                    objB.GetComponent<ImgPrefabs>().HighLight();
                else
                    objB.GetComponent<ImgPrefabs>().LowLight();
            }
            if (objC != null)
            {
                if (storyLine[numb].highLightC == true)
                    objC.GetComponent<ImgPrefabs>().HighLight();
                else
                    objC.GetComponent<ImgPrefabs>().LowLight();
            }
            if (storyLine[numb].haselection)
            {
                selectionWindow.SetActive(true);
                selectionTMP[0].text = selections[currentSelection].selectionLine[0];
                selectionTMP[1].text = selections[currentSelection].selectionLine[1];
            }
        }
    }
}
