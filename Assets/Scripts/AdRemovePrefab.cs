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
    public void BuyAdRemover(){
        StatusManager.instance.adRemoved=1;
        StatusManager.instance.dia +=10000;
        StatusManager.instance.score +=10000;
        this.gameObject.SetActive(false);
    }
}
