using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class ExchangeRateFetcher : MonoBehaviour
{
    public TextMeshProUGUI exchangeRateText; // UI에 표시할 Text
    private string apiUrl = "https://api.exchangerate-api.com/v4/latest/USD"; // API URL

    void Start()
    {

        StartCoroutine(GetExchangeRate());
    }

    IEnumerator GetExchangeRate()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("API 요청 실패: " + request.error);
                exchangeRateText.text = "환율 정보를 가져올 수 없습니다.";
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log("응답 데이터: " + json);

                var data = JSON.Parse(json); // SimpleJSON을 사용하여 파싱

                if (data == null || !data.HasKey("rates") || !data["rates"].HasKey("KRW"))
                {
                    Debug.LogError("JSON 파싱 실패: rates 또는 KRW 데이터 없음");
                    exchangeRateText.text = "환율 데이터를 불러올 수 없습니다.";
                }
                else
                {
                    double rate = data["rates"]["KRW"].AsDouble;
                    exchangeRateText.text = $"1 USD = {rate} KRW";
                    Debug.Log($"환율 업데이트 완료: 1 USD = {rate} KRW");
                }
            }
        }
    }
}
