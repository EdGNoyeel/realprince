using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillAchive : AchievePrefab
{
    [SerializeField]
    public string killnumber;
    [SerializeField]
    int _rewardGold;
    int _numb;

    public override void CheckUnlock(int numb)
    {
        _numb = numb;
        Invoke(killnumber, 0);
    }

    void CheckWin()
    {
        MakeCondi();
        if (_currentPoint >= _targetPoint&&canUnlock)
        {
            StatusManager.instance.score = StatusManager.instance.score + _rewardGold;
            GameObject scoreManager = GameObject.Find("ScoreManager");
            if(scoreManager != null)
            {
                scoreManager.GetComponent<ScoreManager>().IncreaseScore(_rewardGold);
            }

            //ScoreManager.instance.IncreaseScore(_rewardGold);
            //Debug.Log("업적달	");
            UpdateUnlock(_numb);
        }
        
    }

    void killNumberA()
    {
        _currentPoint = StatusManager.instance.killNumberA;
        CheckWin();
    }
    void killNumberB()
    {
        _currentPoint = StatusManager.instance.killNumberB;
        CheckWin();
    }
    void killNumberC()
    {
        _currentPoint = StatusManager.instance.killNumberC;
        CheckWin();
    }
    void killNumberD()
    {
        _currentPoint = StatusManager.instance.killNumberD;
        CheckWin();
    }
    void killNumberE()
    {
        _currentPoint = StatusManager.instance.killNumberE;
        CheckWin();
    }
    void killNumberF()
    {
        _currentPoint = StatusManager.instance.killNumberF;
        CheckWin();
    }
    void killNumberG()
    {
        _currentPoint = StatusManager.instance.killNumberG;
        CheckWin();
    }
    void killNumberH()
    {
        _currentPoint = StatusManager.instance.killNumberH;
        CheckWin();
    }
    void killNumberI()
    {
        _currentPoint = StatusManager.instance.killNumberI;
        CheckWin();
    }
    void killNumberJ()
    {
        _currentPoint = StatusManager.instance.killNumberJ;
        CheckWin();
    }
    void killNumberK()
    {
        _currentPoint = StatusManager.instance.killNumberK;
        CheckWin();
    }
    void killNumberL()
    {
        _currentPoint = StatusManager.instance.killNumberL;
        CheckWin();
    }
    void killNumberM()
    {
        _currentPoint = StatusManager.instance.killNumberM;
        CheckWin();
    }
    void killNumberN()
    {
        _currentPoint = StatusManager.instance.killNumberN;
        CheckWin();
    }
    void killNumberO()
    {
        _currentPoint = StatusManager.instance.killNumberO;
        CheckWin();
    }
    void killNumberP()
    {
        _currentPoint = StatusManager.instance.killNumberP;
        CheckWin();
    }
    void killNumberQ()
    {
        _currentPoint = StatusManager.instance.killNumberQ;
        CheckWin();
    }
    void killNumberR()
    {
        _currentPoint = StatusManager.instance.killNumberR;
        CheckWin();
    }
    void killNumberS()
    {
        _currentPoint = StatusManager.instance.killNumberS;
        CheckWin();
    }
    void killNumberT()
    {
        _currentPoint = StatusManager.instance.killNumberT;
        CheckWin();
    }
    void killNumberU()
    {
        _currentPoint = StatusManager.instance.killNumberU;
        CheckWin();
    }
    void killNumberV()
    {
        _currentPoint = StatusManager.instance.killNumberV;
        CheckWin();
    }
    void killNumberW()
    {
        _currentPoint = StatusManager.instance.killNumberW;
        CheckWin();
    }
    void killNumberX()
    {
        _currentPoint = StatusManager.instance.killNumberX;
        CheckWin();
    }
    void killNumberY()
    {
        _currentPoint = StatusManager.instance.killNumberY; 
        CheckWin();
    }
    void killNumberZ()
    {
        _currentPoint = StatusManager.instance.killNumberZ;
        CheckWin();
    }
}
