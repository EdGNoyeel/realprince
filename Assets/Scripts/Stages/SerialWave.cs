using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialWave : MonoBehaviour
{
    [SerializeField]
    GameObject[] smallWave;
    public float waveRate=0.5f;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Waving", waveRate, waveRate);
    }

    void Waving()
    {
        if(smallWave.Length>i)
        {
            smallWave[i].SetActive(true);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
