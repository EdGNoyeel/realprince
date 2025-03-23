using UnityEngine;

public class AdShow : MonoBehaviour
{
    public void ShowAd(){
        AdmobObj.instance.ShowInterstitialAd();
    }
}
