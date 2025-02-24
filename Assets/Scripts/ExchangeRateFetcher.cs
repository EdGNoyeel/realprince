using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // TextMeshPro 사용 시 필요

public class ExchangeRateFetcher : MonoBehaviour
{
    public TextMeshProUGUI exchangeRateText; // UI에 표시할 텍스트

    private string apiUrl = "https://api.exchangerate-api.com/v4/latest/USD"; // 예제 API (USD 기준 환율)

    void Start()
    {
        StartCoroutine(GetExchangeRate());
    }

    IEnumerator GetExchangeRate()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                ExchangeRateData data = JsonUtility.FromJson<ExchangeRateData>(json);

                if (data != null && data.rates != null)
                {
                    float rate = data.rates["KRW"]; // 원화(KRW) 환율 가져오기
                    exchangeRateText.text = $"USD to KRW: {rate:F2}";
                }
            }
            else
            {
                Debug.LogError("API 호출 실패: " + request.error);
            }
        }
    }
}

// JSON 데이터를 매핑할 클래스
[System.Serializable]
public class ExchangeRateData
{
    public string base_currency;
    public System.Collections.Generic.Dictionary<string, float> rates;
}