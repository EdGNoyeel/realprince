using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameStory : MonoBehaviour
{
    public string nameString;
    public string[] lines;
    public TextMeshProUGUI line;
    public TextMeshProUGUI _name;
    int lineNumb = 1;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        line.text = lines[0];
        _name.text = nameString;

    }

    // Update is called once per frame
    public void NextLine()
    {
        
        if (lineNumb < lines.Length)
        {
            line.text= lines[lineNumb];
            lineNumb++;
        }
        else
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

    }
}
