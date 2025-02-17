using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchevementPN : MonoBehaviour
{
    public TextMeshProUGUI AchievementListTMP;
    // Start is called before the first frame update
    public void UpdateAchievement()
    {
        AchievementsManager.instance.CheckUnlocks();
        Invoke("CheckFullList", 0.2f);
    }

    void CheckFullList()
    {
        AchievementListTMP.text = AchievementsManager.instance.fullList;
    }
}
