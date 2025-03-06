using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RSSNewsFetcher : MonoBehaviour
{
    public TextMeshProUGUI newsText; // 뉴스 제목을 표시할 UI Text
    public RectTransform newsTextRect; // 텍스트의 RectTransform
    public RectTransform parentRect; // 부모 패널의 RectTransform
    public float scrollSpeed = 100f; // 스크롤 속도

    private string rssUrl = "https://www.chosun.com/arc/outboundfeeds/rss/?outputType=xml";
    private float startX, endX;
    private string newsHeadline;

    void Start()
    {
        StartCoroutine(GetNewsFromRSS());
    }

    IEnumerator GetNewsFromRSS()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(rssUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("RSS 요청 실패: " + request.error);
                newsText.text = "뉴스를 가져올 수 없습니다.";
            }
            else
            {
                string xmlData = request.downloadHandler.text;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);

                XmlNodeList items = xmlDoc.GetElementsByTagName("item");

                if (items.Count > 0)
                {
                    newsHeadline = items[0]["title"].InnerText;
                    Debug.Log($"헤드라인 뉴스: {newsHeadline}");

                    // 뉴스 문장을 두 번 반복하여 여백 없이 이어지도록 설정
                    newsText.text = $"{newsHeadline}    {newsHeadline}    ";

                    // 텍스트 위치 및 이동 범위 설정
                    startX = parentRect.rect.width / 2 + newsTextRect.rect.width / 2;
                    endX = -startX;

                    StartCoroutine(ScrollNews());
                }
                else
                {
                    newsText.text = "헤드라인을 찾을 수 없습니다.";
                }
            }
        }
    }

    IEnumerator ScrollNews()
    {
        while (true)
        {
            // 텍스트의 시작 위치 설정 (오른쪽 끝)
            newsTextRect.anchoredPosition = new Vector2(startX, newsTextRect.anchoredPosition.y);
            float x = startX;

            // 텍스트가 왼쪽으로 이동
            while (x > endX)
            {
                x -= scrollSpeed * Time.deltaTime;
                newsTextRect.anchoredPosition = new Vector2(x, newsTextRect.anchoredPosition.y);
                yield return null;
            }

            // 끝까지 가면 즉시 오른쪽에서 다시 시작 (여백 없이 반복)
        }
    }
}