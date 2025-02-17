using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class particle : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject mySelf;
    [SerializeField]
    int myNumb;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("DestroyPF",1);
        Timing.RunCoroutine(EnQ().CancelWith(gameObject));
    }
    void OnEnable()
    {
        Timing.RunCoroutine(EnQ().CancelWith(gameObject));
        if(animator != null)
            animator.Rebind();
    }

    IEnumerator<float> EnQ()
    {
        yield return Timing.WaitForSeconds(1);
        ObjPuller.instance.objectPoolList[myNumb].Enqueue(mySelf);
        mySelf.SetActive(false);
    }


    void DestroyPF()
    {
        Destroy(mySelf);
    }
    
}
