using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class OWM_Coord
{
    public float lon;
    public float lat;
}

[System.Serializable]
public class OWM_Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class OWM_Main
{
    public int temp;
    public float feels_like;
    public int temp_min;
    public int temp_max;
    public int pressure;
    public int humidity;
}

[System.Serializable]
public class OWM_Wind
{
    public float speed;
    public int deg;
}

[System.Serializable]
public class OWM_Clouds
{
    public int all;
}

[System.Serializable]
public class OWM_Sys
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}

[System.Serializable]
public class WeatherData
{
    public OWM_Coord coord;
    public OWM_Weather[] weather;
    public string basem;
    public OWM_Main main;
    public int visibility;
    public OWM_Wind wind;
    public OWM_Clouds clouds;
    public int dt;
    public OWM_Sys sys;
    public int timezone;
    public int id;
    public string name;
    public int cod;

}
public class Weather : MonoBehaviour
{
    public string APP_ID;
    public Text weatherText;
    public WeatherData weatherInfo;

    // Start is called before the first frame update
    void Start()
    {
        CheckCityWeather("Seoul");
    }

    public void CheckCityWeather(string city)
    {
        StartCoroutine(GetWeather(city));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetWeather(string city)
    {
        city = UnityWebRequest.EscapeURL(city);
        string url = "http://api.openweathermap.org/data/2.5/weather?q=Seoul&units=metric&appid=b241c860295fd1babd96feb805eb16e3 api key";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        json = json.Replace("\"base\":", "\"basem\":");
        weatherInfo = JsonUtility.FromJson<WeatherData>(json);

        if (weatherInfo.weather.Length > 0)
        {
            weatherText.text = weatherInfo.weather[0].main;
        }

        Debug.Log(weatherInfo + weatherText.ToString());

    }
}
