using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAchieve : AchievePrefab
{
    

    [SerializeField]
    int _rewardGold;
    // Start is called before the first frame update
    public override void CheckUnlock(int numb)
    {
        _currentPoint = StatusManager.instance.score;
        Debug.Log(_currentPoint + "/" + _targetPoint);
        MakeCondi();
        if (_currentPoint > _targetPoint&&canUnlock)
        {
            StatusManager.instance.score = StatusManager.instance.score + _rewardGold;
            ScoreManager.instance.IncreaseScore(_rewardGold);
            Debug.Log("업적달	");
            UpdateUnlock(numb);

            

        }
    }

}
