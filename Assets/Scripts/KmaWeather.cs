using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class KmaWeather : MonoBehaviour
{
    private string kmaUrl = "https://www.kma.go.kr/weather/forecast/mid-term-rss3.jsp?stnId=109"; // 서울 및 경기 지역
    public TextMeshProUGUI weatherText; // UI에 표시할 TMP 텍스트
    public Image weatherIcon; // 날씨 아이콘을 표시할 UI Image
    public Sprite clearSprite, fewCloudsSprite, cloudySprite, rainSprite, snowSprite, sleetSprite, showerSprite; // 날씨별 스프라이트

    private Dictionary<string, Sprite> weatherSprites;

    void Start()
    {
        weatherSprites = new Dictionary<string, Sprite>()
        {
            { "맑음", clearSprite },
            { "구름조금", fewCloudsSprite },
            { "구름많음", cloudySprite },
            { "흐림", cloudySprite },
            { "비", rainSprite },
            { "소나기", showerSprite },
            { "눈", snowSprite },
            { "비/눈", sleetSprite },
            { "소낙눈", snowSprite },
            { "흐리고 비", rainSprite },
            { "흐리고 눈", snowSprite },
            { "흐리고 비/눈", sleetSprite },
            { "흐리고 소나기", showerSprite },
            { "흐리고 소낙눈", snowSprite },
            { "구름많고 비", rainSprite },
            { "구름많고 눈", snowSprite },
            { "구름많고 비/눈", sleetSprite },
            { "구름많고 소나기", showerSprite },
            { "구름많고 소낙눈", snowSprite }
        };
        StartCoroutine(GetWeatherFromKMA());
    }

    IEnumerator GetWeatherFromKMA()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(kmaUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("날씨 데이터를 가져오는데 실패했습니다: " + request.error);
                weatherText.text = "날씨 정보를 불러올 수 없습니다.";
            }
            else
            {
                ParseWeatherData(request.downloadHandler.text);
            }
        }
    }

    void ParseWeatherData(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);

        XmlNodeList itemList = xmlDoc.GetElementsByTagName("data");
        if (itemList.Count > 0)
        {
            XmlNode firstItem = itemList[0]; // 첫 번째 예보 정보
            string weather = firstItem["wf"].InnerText; // 날씨 상태 (맑음, 흐림 등)
            string temp = firstItem["tmn"].InnerText + "°C ~ " + firstItem["tmx"].InnerText + "°C"; // 최저~최고 기온

            weatherText.text = $"현재 날씨: {weather}, {temp}";

            // 아이콘 변경
            if (weatherSprites.ContainsKey(weather))
            {
                weatherIcon.sprite = weatherSprites[weather];
            }
            else
            {
                Debug.LogWarning($"[{weather}]에 해당하는 아이콘이 없습니다.");
            }
        }
        else
        {
            weatherText.text = "날씨 정보를 불러올 수 없습니다.";
        }
    }
}