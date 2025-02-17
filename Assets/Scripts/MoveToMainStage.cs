using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToMainStage : MonoBehaviour
{
    //[SerializeField]
    //GameObject loading;
    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeGameScene()
    {
        //loading.SetActive(true);
        SceneManager.LoadScene("MainStage");

    }
}

