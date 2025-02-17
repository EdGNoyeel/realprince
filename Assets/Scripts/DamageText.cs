using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    [SerializeField]
    TextMeshPro text;
    [SerializeField]
    GameObject mySelf;
    [SerializeField]
    int myNumb;
    Color alpha;
    public int damage;
    Vector3 thisPos;
    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = 1.0f;
        alphaSpeed = 2.0f;
        destroyTime = 1.0f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        alpha.a = 1;
        text.color = alpha;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    void OnEnable()
    {
        moveSpeed = 1.0f;
        alphaSpeed = 2.0f;
        destroyTime = 1.0f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        alpha.a = 1;
        text.color = alpha;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        text.text = damage.ToString();
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        //Destroy(gameObject);
        ObjPuller.instance.objectPoolList[myNumb].Enqueue(mySelf); 
        mySelf.transform.SetParent(ObjPuller.instance.transform, false);
        mySelf.SetActive(false);    
    }
}
