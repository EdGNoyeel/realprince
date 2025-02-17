using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBTN : MonoBehaviour
{
    [SerializeField]
    GameObject nextFloor;

    public void ClickBTN()
    {
        GameObject lsh_head = GameObject.Find("LSH_Head_prefab");
        Debug.Log(lsh_head.transform.position);
        lsh_head.GetComponent<LSH_head>().justMove = true;
        lsh_head.GetComponent<LSH_head>().SetTarget(nextFloor.transform.position);

    }
}
