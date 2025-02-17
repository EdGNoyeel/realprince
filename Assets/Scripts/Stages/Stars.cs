using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    // Start is called before the first frame update
    
    public void Initialize()
    {
        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);
    }

    public void Star2()
    {
        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(false);
    }
    public void Star1()
    {
        star1.SetActive(true);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public void Star0()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }
}
