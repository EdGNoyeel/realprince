using UnityEngine;
using UnityEngine.SceneManagement;

public class GuestBlocker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!StatusManager.instance.guest){
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void GetBack(){
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("부모가 없습니다.");
        }
    }

    public void GoToJoin(){
        SceneManager.LoadScene("Openning");
    }
}
