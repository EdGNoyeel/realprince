using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 10f, Color.green);

        RaycastHit hit;
        if (Input.GetMouseButtonUp(0))
        {
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("클릭");

                if (hit.transform.gameObject.name=="ByunKi")
                {
                    /*if (hit.transform.gameObject.GetComponent<ByunGi>()?.open == false)
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Open");
                        hit.transform.gameObject.GetComponent<ByunGi>().open = true;*/
                        StoryManager.instance.RunStory("intro");
                    /*}
                    else
                    {
                        hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Close");
                        hit.transform.gameObject.GetComponent<ByunGi>().open = false;
                    }*/
                    
                    
                }
            }
        }
    }
}

