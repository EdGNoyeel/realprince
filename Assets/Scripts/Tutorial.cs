using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Skip()
    {
        Time.timeScale = 1;
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
