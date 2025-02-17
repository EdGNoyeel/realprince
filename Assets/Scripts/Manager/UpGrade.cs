using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpGrade : MonoBehaviour
{

    //데미지 업그레이드파트
    [SerializeField]
    UnityEngine.UI.Text damageCostTxt;
    [SerializeField]
    UnityEngine.UI.Text damageUpTxt;
    [SerializeField]
    UnityEngine.UI.Text currentDamageTxt;
    [SerializeField]
    UnityEngine.UI.Text currentlevelTxt;
    [SerializeField]
    UnityEngine.UI.Text NextlevelTxt;
    float originTbDamage;
    public float tbDamageRatio;
    public float upDamage;
    int coin;
    public int tbDamageCost;
    public TextAsset damagetxt;
    string[,] Sentence;
    int lineSize, rowSize;
    string damageUpCostString;
    string damageUpString;
    string damageUpCostStringBtn;
    string damageUpStringBtn;
    public string currentDamageTxtString;
    int level = 1;
    int nextLevel = 2;



    //청구서
    [SerializeField]
    TextMeshProUGUI bill;
    int billCost=0;
    //치명타 배율 업그레이드 파트
    [SerializeField]
    UnityEngine.UI.Text criticalDamCostTxt;
    [SerializeField]
    UnityEngine.UI.Text criticalDamUpTxt;
    [SerializeField]
    UnityEngine.UI.Text currentCritcalDamTxt;
    [SerializeField]
    UnityEngine.UI.Text currentCritcalDamLevelTxt;
    [SerializeField]
    UnityEngine.UI.Text nextCritcalDamLevelTxt;
    float originCriDamage;
    public float upCriDamage;
    public int upCriCost;
    public float upCriMultiply = 0.02f;
    string upCriCostString;
    string upCriString;
    string upCriCostStringBtn;
    string upCriStringBtn;
    public string currentCriTxtString;
    int criLevel = 1;
    int criNextLevel = 2;

    //치명타확율 업그레이드 파트
    [SerializeField]
    UnityEngine.UI.Text criRateCostTxt;
    [SerializeField]
    UnityEngine.UI.Text criRateUptTxt;
    [SerializeField]
    UnityEngine.UI.Text currentCriRatetTxt;
    [SerializeField]
    UnityEngine.UI.Text currentCriRatetLevelTxt;
    [SerializeField]
    UnityEngine.UI.Text nextCriRateLevelTxt;
    public float upCriRate;
    public int upCriRateCost;
    public float upCriRateConstant=0.99f;
    int criRateLevel = 1;
    int criRateNextLevel = 2;


    void Start()
    {
        // 엔터단위와 탭으로 나눠서 배열의 크기 조정
        string currentText = damagetxt.text.Substring(0, damagetxt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        //bill = GameObject.Find("Bill").GetComponent<TextMeshProUGUI>();

        // 한 줄에서 탭으로 나눔
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++) Sentence[i, j] = row[j];
        }

        //데미지 업그레이드 초기화
        level = StatusManager.instance.tbDamageLv;

        damageUpCostString = Sentence[level, 1];
        damageUpString = Sentence[level, 2];
        damageCostTxt.text = damageUpCostString;
        damageUpTxt.text = damageUpString;
        currentDamageTxtString = Sentence[level - 1, 2];
        currentDamageTxt.text = currentDamageTxtString;

        GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().damage = float.Parse(currentDamageTxtString);

        currentlevelTxt.text = "lv"+ level.ToString();
        nextLevel = level + 1;
        NextlevelTxt.text = "lv"+ nextLevel.ToString();
        //크리티컬 배율 업그레이드 초기화
        criLevel = StatusManager.instance.tbCriDamLv;
        upCriCostString = Sentence[criLevel, 1];
        upCriString = (1 + upCriMultiply * criLevel).ToString();
        criticalDamUpTxt.text = upCriString;
        criticalDamCostTxt.text = upCriCostString;
        currentCritcalDamTxt.text = (1 + upCriMultiply * (criLevel - 1)).ToString();
        currentCritcalDamLevelTxt.text = "lv" + criLevel.ToString();
        nextCritcalDamLevelTxt.text = "lv" + (criLevel + 1).ToString();

        currentCriTxtString = (1+upCriMultiply * (criLevel-1)).ToString();

        GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().critical = 1 + upCriMultiply * criLevel;
        //크리티컬 확율 업그레이드 초기화
        criRateLevel = StatusManager.instance.tbCriRateLv;
        //Debug.Log("크리테스트"+Mathf.Pow(upCriRateConstant, criRateLevel));

        criRateUptTxt.text = string.Format("{0:P}",(1 - Mathf.Pow(upCriRateConstant, criRateLevel)));
        criRateCostTxt.text = Sentence[criRateLevel, 1];
        currentCriRatetTxt.text = string.Format("{0:P}", (1 - Mathf.Pow(upCriRateConstant, criRateLevel-1)));
        currentCriRatetLevelTxt.text = "lv" + criRateLevel.ToString();
        nextCriRateLevelTxt.text = "lv" + (criRateLevel + 1).ToString();

        GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().criticalRate = 1 - Mathf.Pow(upCriRateConstant, criRateLevel);

    }

    public void CriRateUpgrade()
    {
        upCriRateCost = int.Parse(Sentence[criRateLevel, 1]);        
        coin = ScoreManager.instance.currentScore;

        if (coin >= upCriRateCost)
        {
            upCriRate = 1 - Mathf.Pow(upCriRateConstant, criRateLevel);
            criRateLevel++;
            ScoreManager.instance.currentScore = coin - upCriRateCost;
            StatusManager.instance.score = ScoreManager.instance.currentScore;
            StatusManager.instance.tbCriRateLv = criRateLevel;

            GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().criticalRate = upCriRate;
            billCost = billCost + upCriRateCost;
            bill.text = billCost.ToString();
            criRateCostTxt.text = billCost.ToString();
            criRateUptTxt.text = string.Format("{0:P}", upCriRate);

            currentCriRatetTxt.text = string.Format("{0:P}", (1 - Mathf.Pow(upCriRateConstant, criRateLevel - 1)));
            currentCriRatetLevelTxt.text = "lv" + criRateLevel.ToString();
            nextCriRateLevelTxt.text = "lv" + (criRateLevel + 1).ToString();
            
            SoundManager.instance.PlaySE("atm");
        }
        else
        {
            SoundManager.instance.PlaySE("noMoney");
        }
    }
    
    public void CriDamUpgrade()
    {
        upCriCost = int.Parse(upCriCostString);        
        coin = ScoreManager.instance.currentScore;

        if (coin >= upCriCost)
        {
            upCriDamage = (1 + upCriMultiply * criLevel);
            criLevel++;
            ScoreManager.instance.currentScore = coin - upCriCost;
            StatusManager.instance.score=ScoreManager.instance.currentScore;
            StatusManager.instance.tbCriDamLv = criLevel;

            upCriCostString = Sentence[criLevel, 1];
            upCriString = (1 + upCriMultiply * criLevel).ToString();

            GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().critical = upCriDamage;

            billCost = billCost + upCriCost;
            bill.text = billCost.ToString();
            criticalDamCostTxt.text = upCriCostString;
            criticalDamUpTxt.text = upCriString;

            currentCritcalDamTxt.text = (1 + upCriMultiply * (criLevel-1)).ToString();
            currentCritcalDamLevelTxt.text = "lv" + criLevel.ToString();
            nextCritcalDamLevelTxt.text= "lv"+(criLevel+1).ToString();

            SoundManager.instance.PlaySE("atm");
        }
        else
        {
            SoundManager.instance.PlaySE("noMoney");
        }
    }

    public void TbDamageUpgrade()
    {        
        tbDamageCost = int.Parse(damageUpCostString);
        upDamage = float.Parse(damageUpString);        
        coin = ScoreManager.instance.currentScore;

        if (coin >= tbDamageCost)
        {
            level = level + 1;
            ScoreManager.instance.currentScore = coin - tbDamageCost;            
            StatusManager.instance.score = ScoreManager.instance.currentScore;
            StatusManager.instance.tbDamageLv = level;
            originTbDamage = ScoreManager.instance.tbDamage;
            ScoreManager.instance.tbDamage = upDamage;
            /*damageUpCostStringBtn = Sentence[level+1, 1];
            damageUpStringBtn = Sentence[level+1, 2];*/
            damageUpCostString = Sentence[level, 1];
            damageUpString = Sentence[level, 2];

            GameObject.Find("ToothBrush").GetComponent<ToothBrushMove>().damage = float.Parse(currentDamageTxtString);

            billCost = billCost + int.Parse(damageUpCostString);
            bill.text = billCost.ToString();
            damageCostTxt.text = damageUpCostString;
            damageUpTxt.text = damageUpString;

            currentDamageTxtString = Sentence[level - 1, 2];
            currentDamageTxt.text = currentDamageTxtString;
            currentlevelTxt.text = "lv" + level.ToString();
            nextLevel = level + 1;
            NextlevelTxt.text = "lv" + nextLevel.ToString();

            SoundManager.instance.PlaySE("atm");
        }
        else
        {
            SoundManager.instance.PlaySE("noMoney");
        }
        

        
    }
}
