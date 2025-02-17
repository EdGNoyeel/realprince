using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievePrefab : MonoBehaviour
{
    public int _targetPoint;
    public int _currentPoint;
    public string requirement;
    public string rewardment;
    public string condi;
    public bool canUnlock;
    public float progress;
    

    [SerializeField]
    protected GameObject rewardPrefab;
    // Start is called before the first frame update
    public virtual void CheckUnlock(int numb)
    {

    }

    protected void UpdateUnlock(int numb)
    {
        string[] newArr = StatusManager.instance.achievements.Split(new char[] { ',' });
        newArr[numb] = "1";
        StatusManager.instance.achievements= string.Join(",", newArr);
        if(rewardPrefab != null)
        {
            GameObject _obj = Instantiate(rewardPrefab);
            _obj.transform.SetParent(GameObject.Find("StoryBoard").transform, false);

            TextMeshProUGUI[] rewardString = _obj.GetComponentsInChildren<TextMeshProUGUI>();
            rewardString[1].text = requirement;
            rewardString[2].text = rewardment;


        }

    }

    public void MakeCondi()
    {
        if (_currentPoint > _targetPoint)
        {
            condi = requirement + "(" + _targetPoint.ToString() + "/" + _targetPoint.ToString() + ")";
            progress = 1;
        }
        else
        {
            progress = (float)_currentPoint / (float)_targetPoint;
            //Debug.Log(progress);
            condi = requirement + "(" + _currentPoint.ToString() + "/" + _targetPoint.ToString() + ")";
        }
            


    }
}
