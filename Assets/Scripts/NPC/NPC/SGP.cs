using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGP : NPC
{

    public GameObject sad;
    // Start is called before the first frame update
    void Awake()
    {
        sad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        

        for (int i = 0; i < items.Length; i++)
        {
            if (StatusManager.instance.sGP_Item[i] == true)
            {
                items[i].SetActive(true);
            }
            if (StatusManager.instance.sGP_Item[i] == false)
            {
                items[i].SetActive(false);
            }
        }
    }
    public void Sad()
    {
        sad.SetActive(true);
    }

    public void UnSad()
    {
        sad.SetActive(false);
    }
}
