using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MEC;

public class EnemyLines : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    //private float destroyTime;
    [SerializeField]
    TextMeshPro text;
    Color alpha;
    public string lines;

    [SerializeField]
    GameObject mySelf;
    [SerializeField]
    int myNumb;

    // Start is called before the first frame update
    /*void Start()
    {
        moveSpeed = 1.0f;
        alphaSpeed = 2.0f;
        destroyTime = 1.0f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        //text.text = lines;
        Timing.RunCoroutine(EnQ().CancelWith(gameObject));
        //Invoke("DestroyObject", destroyTime);
    }*/
    
    // Start is called before the first frame update
    
    
    void OnEnable()
    {
        moveSpeed = 1.0f;
        alphaSpeed = 2.0f;
        //destroyTime = 1.0f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        //text.text = lines;
        Timing.RunCoroutine(EnQ().CancelWith(gameObject));
        
    }

    IEnumerator<float> EnQ()
    {
        yield return Timing.WaitForSeconds(1);
        mySelf.transform.SetParent(GameObject.Find("ObjPuller").transform, false);
        ObjPuller.instance.objectPoolList[myNumb].Enqueue(mySelf);
        mySelf.SetActive(false);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
