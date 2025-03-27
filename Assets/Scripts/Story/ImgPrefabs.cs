using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImgPrefabs : MonoBehaviour
{
    [SerializeField]
    GameObject[] HL;
    [SerializeField]
    GameObject[] LL;
    [SerializeField]
    GameObject animated;
    [SerializeField]
    public GameObject texts;
    [SerializeField]
    TextMeshProUGUI name1;
    [SerializeField]
    TextMeshProUGUI name2;
    [SerializeField]
    public TextMeshProUGUI word;
    [SerializeField]
    TextMeshProUGUI level;
    // Start is called before the first frame update
    void Awake()
    {
        name1.text = "";
        name2.text = "";
        word.text = "";
        level.text = "";
        /*Debug.Log(name+"재등장");
        for (int i = 0; i < HL.Length; i++)
        {

            HL[i].GetComponent<Image>().enabled = true;
            LL[i].GetComponent<Image>().enabled = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    public void HighLight()
    {
        
        Debug.Log("HighLighted");
        for (int i = 0; i < HL.Length; i++)
        {

            HL[i].SetActive(true);
            var image = LL[i]?.GetComponent<Image>();
            if (image == null)
            {
                Debug.LogWarning($"LL[{i}]의 Image 컴포넌트를 찾지 못했습니다.");
            }
            else
            {
                image.enabled = false;
                Debug.Log($"LL[{i}] 이미지 비활성화 완료");
            }
            
        }
        if (animated != null)
        {
            animated.SetActive(false);
        }
        
    }
    public void Name1(string _string)
    {
        name1.text= _string;
    }
    public void Name2(string _string)
    {
        name2.text = _string;
    }
    public void Word(string _string)
    {
        word.text = _string;
    }
    public void Level(string _string)
    {
        level.text = _string;
    }



    public void LowLight()
    {

        for (int i = 0; i < HL.Length; i++)
        {
            HL[i].SetActive(false);
            LL[i].SetActive(true);
            
        }
        if (animated != null)
        {
            animated.SetActive(false);
        }
    }

    

    public void OffLight()
    {
        for (int i = 0; i < HL.Length; i++)
        {
            HL[i].SetActive(false);
            LL[i].SetActive(false);
            
        }

        if (animated != null)
        {
            animated.SetActive(false);
        }
    }

    public void SpeacialAnimate(){
        for (int i = 0; i < HL.Length; i++)
        {
            HL[i].SetActive(false);
            LL[i].SetActive(false);
            
        }

        if (animated != null)
        {
            animated.SetActive(true);
            Debug.Log("animationPlayed");
        }
    }

    
}
