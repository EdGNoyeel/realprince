using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTooth : MonoBehaviour
{
    Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Blink();

    }    

    void Blink()
    {
        float a = Random.Range(1f, 3f);
        Invoke("ActionBlink", a);
    }

    void ActionBlink()
    {
        anim.SetTrigger("act");
        Blink();
    }
}
