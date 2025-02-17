using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRaycast : MonoBehaviour
{
    public bool canHelp = true;
    [SerializeField]
    GameObject toothBrush;
    


    // Start is called before the first frame update
    void Start()
    {
        canHelp = true;
        
    }

    // Update is called once per frame
    void Update()
    {


        /*if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Input.mousePosition;
            Ray2D ray = new Ray2D(clickPosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);

                hit.transform.gameObject.GetComponent<Help>().HelpMe(clickPosition);
                canHelp = false;

            }

            *//*if (hit.transform.tag == "Help")
            {
                Debug.Log("도움말파괴");
                hit.transform.gameObject.GetComponent<HelpLine>().Delete();
                canHelp = true;
            }*//*
        }*/
        //Touch touch = Input.GetTouch(0);
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (Input.GetTouch(i).phase==TouchPhase.Ended)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                Debug.Log(touchPosition);
                toothBrush.GetComponent<ToothBrushMove>().BoostDown();

                if (hit.collider != null )
                {
                    Debug.Log(hit.transform.name);
                    
                    hit.transform.gameObject.GetComponent<Help>().HelpMe(touchPosition);
                    canHelp = false;

                }

                if (hit.transform.tag == "Help")
                {
                    Debug.Log("도움말파괴");
                    hit.transform.gameObject.GetComponent<HelpLine>().Delete();
                    canHelp = true;
                }
                
                
                    
                

            }
            



        
            if (Input.GetTouch(i).phase==TouchPhase.Began)
            {
                
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {

                    if (hit.transform.name == "BoosterOn")
                    {
                        Debug.Log("Boost!!");
                        toothBrush.GetComponent<ToothBrushMove>().BoostOn();
                    }
                    else
                        toothBrush.GetComponent<ToothBrushMove>().BoostDown();

                }
            }
            

        }
    }
}
