using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HomeworkPN : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI year;
    [SerializeField]
    TextMeshProUGUI month;
    [SerializeField]
    TextMeshProUGUI day;
    [SerializeField]
    TextMeshProUGUI dayOfWeek;
    // Start is called before the first frame update
    void OnEnable()
    {
        year.text = DateTime.Now.ToString("yyyy");
        month.text = DateTime.Now.ToString("MM");
        day.text = DateTime.Now.ToString("dd");
        if (DateTime.Now.DayOfWeek.ToString() == "Monday")
        {
            dayOfWeek.text = "월";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Tuesday")
        {
            dayOfWeek.text = "화";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
        {
            dayOfWeek.text = "수";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Thursday")
        {
            dayOfWeek.text = "목";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Friday")
        {
            dayOfWeek.text = "금";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Saturday")
        {
            dayOfWeek.text = "토";
        }
        if (DateTime.Now.DayOfWeek.ToString() == "Sunday")
        {
            dayOfWeek.text = "일";
        }

    }
}
