using UnityEngine;

public class AdRemovePrefab : MonoBehaviour
{
    public GameObject buttonObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if(StatusManager.instance.adRemoved==1){
            buttonObj.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
