using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Help : MonoBehaviour
{
    [SerializeField]
    GameObject help;
    public string lineText;
    
    public void HelpMe(Vector2 position)
    {
        Debug.Log(lineText);
        GameObject linesText = Instantiate(help); // 생성할 텍스트 오브젝트
        linesText.transform.position = position; // 표시될 위치
        linesText.GetComponent<HelpLine>().lines = lineText;
        linesText.transform.SetParent(this.transform, false);
    }
}
