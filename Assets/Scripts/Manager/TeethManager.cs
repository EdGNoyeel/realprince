using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethManager : MonoBehaviour
{
    public GameObject[] teeth;
    // Start is called before the first frame update
    
    public void RestoreTeeth()
    {
        for (int i = 0; i < teeth.Length; i++)
        {
            teeth[i].SetActive(true);
            teeth[i].GetComponentInChildren<NewTooth>().canHeal=true;
            teeth[i].GetComponentInChildren<NewTooth>().Restore(1000);
            teeth[i].GetComponentInChildren<Animator>().SetTrigger("restore");
        }
        
    }
}
