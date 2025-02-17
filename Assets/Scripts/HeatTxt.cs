using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeatTxt : MonoBehaviour
{
    TextMeshProUGUI heartNumbTMP;
    // Start is called before the first frame update
    void Start()
    {
        heartNumbTMP = GetComponent<TextMeshProUGUI>();
        heartNumbTMP.text = HeartRechargerManager.instance.m_HeartAmount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        heartNumbTMP.text = HeartRechargerManager.instance.m_HeartAmount.ToString();
    }
}
