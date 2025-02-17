using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.UI;

public class mucmul : MonoBehaviour
{
    Image _spriteRenderer;
    public float fadeTime=1;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<Image>();
        Invoke("StartFade", fadeTime);        

    }

    // Update is called once per frame
    

    void StartFade()
    {
        Timing.RunCoroutine(FadeAway().CancelWith(gameObject));
    }


    IEnumerator<float> FadeAway()
    {
        //yield return new WaitForSeconds(1);
        while (_spriteRenderer.color.a > 0)
        {
            var color = _spriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (0.3f * Time.deltaTime);

            _spriteRenderer.color = color;
            //wait for a frame
            yield return Timing.WaitForOneFrame;
        }
        Destroy(gameObject);
        
    }
}
