using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        items[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
