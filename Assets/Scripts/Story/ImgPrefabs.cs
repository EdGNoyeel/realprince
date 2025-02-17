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
        
        for (int i = 0; i < HL.Length; i++)
        {

            HL[i].GetComponent<Image>().enabled = true;
            LL[i].GetComponent<Image>().enabled = false;
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
            HL[i].GetComponent<Image>().enabled = false;
            LL[i].GetComponent<Image>().enabled = true;
        }
    }

    

    public void OffLight()
    {
        for (int i = 0; i < HL.Length; i++)
        {
            HL[i].GetComponent<Image>().enabled = false;
            LL[i].GetComponent<Image>().enabled = false;
        }
    }

    
}
