using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandChangeButton : MonoBehaviour
{
    public void ChangeLand(int numb)
    {
        GameObject.Find("MapPosition").GetComponent<LandManager>().LoadLand(numb);
        StatusManager.instance.currentLand = numb;
        SoundManager.instance.PlaySE("bus");
    }
}
