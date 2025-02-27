using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class RSSNewsFetcher : MonoBehaviour
{
    public TextMeshProUGUI newsText; // 뉴스 제목을 표시할 UI Text
    private string rssUrl = "https://www.chosun.com/arc/outboundfeeds/rss/?outputType=xml"; // 네이버 뉴스 RSS

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
                    string headline = items[0]["title"].InnerText;
                    newsText.text = $"충격 실화! {headline}";
                    Debug.Log($"헤드라인 뉴스: {headline}");
                }
                else
                {
                    newsText.text = "헤드라인을 찾을 수 없습니다.";
                }
            }
        }
    }
}