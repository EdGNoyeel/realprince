using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class CheckGold : MonoBehaviour
{
    TextMeshProUGUI GoldNumbTMP;
    // Start is called before the first frame update
    void Start()
    {
        GoldNumbTMP = GetComponent<TextMeshProUGUI>();

        GoldNumbTMP.text = ExtensionNumber.ToMoneyString(StatusManager.instance.score);

    }

    // Update is called once per frame
    void Update()
    {
        GoldNumbTMP.text = ExtensionNumber.ToMoneyString(StatusManager.instance.score);
    }
}
