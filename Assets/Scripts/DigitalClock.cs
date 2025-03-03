using TMPro;
using UnityEngine;
using System;

public class DigitalClock : MonoBehaviour
{
    public TMP_Text clockText; // TextMeshPro UI 오브젝트 연결

    void Start()
    {
        InvokeRepeating("UpdateClock", 0f, 1f); // 1초마다 실행
    }

    void UpdateClock()
    {
        // 현재 시간을 12시간 형식 + AM/PM으로 변환
        string currentTime = DateTime.Now.ToString("hh:mm:ss tt");

        // TextMeshPro UI에 적용
        clockText.text = currentTime;
    }
}