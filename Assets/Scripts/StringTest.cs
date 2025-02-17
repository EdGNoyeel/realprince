using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringTest : MonoBehaviour
{
    string test;
    string[] result;
    // Start is called before the first frame update
    void Start()
    {
        test = "1,2,3,4,5,6,7";
        result = test.Split(new char[] { ',' });
        for (int i = 0; i < result.Length; i++)
        {
            if (i == 2)
            {
                result[i] = "9";
            }
        }
        test = string.Join(",", result);
        Debug.Log(test);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
