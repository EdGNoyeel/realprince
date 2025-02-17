using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreToothController : MonoBehaviour
{
    [SerializeField]
    GameObject good;
    [SerializeField]
    GameObject bad;
    [SerializeField]
    GameObject worst;    

    public void Good()
    {
        good.SetActive(true);
        bad.SetActive(false);
        worst.SetActive(false);
    }

    public void Bad()
    {
        good.SetActive(false);
        bad.SetActive(true);
        worst.SetActive(false);
    }

    public void Worst()
    {
        good.SetActive(false);
        bad.SetActive(false);
        worst.SetActive(true);
    }
}
