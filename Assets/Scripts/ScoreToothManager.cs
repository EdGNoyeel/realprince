using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToothManager : MonoBehaviour
{
    public GameObject[] toothUI;
    [SerializeField]
    GameObject[] upgradeBTN;
    string upgradeCost="100,5000,30000,100000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";

    // Start is called before the first frame update
    void Start()
    {
        SetButtons();
        CheckUpGrade();
        ToothCount(StatusManager.instance.tootheCount);
    }

    void SetButtons()
    {
        string[] arr = StatusManager.instance.teethUpgrade.Split(new char[] { ',' });

        for (int j = 0; j < upgradeBTN.Length; j++)
        {
            Image[] images = upgradeBTN[j].GetComponentsInChildren<Image>();
            upgradeBTN[j].GetComponent<Button>().interactable = false;
            images[images.Length - 1].enabled = false;
        }

        for (int k = 0; k < upgradeBTN.Length - 1; k++)
        {
            if (arr[k] == "1")
            {
                upgradeBTN[k + 1].GetComponent<Button>().interactable = true;
            }
        }

        upgradeBTN[0].GetComponent<Button>().interactable = true;


        for (int i = 0; i < upgradeBTN.Length; i++)
        {
            if (arr[i] == "1")
            {
                upgradeBTN[i].GetComponent<Button>().interactable = false;
                Image[] images = upgradeBTN[i].GetComponentsInChildren<Image>();
                images[images.Length - 1].enabled = true;
            }
        }
    }

    void CheckUpGrade()
    {
        string[] arr = StatusManager.instance.teethUpgrade.Split(new char[] { ',' });

        if (arr[0] == "1")
        {
            StatusManager.instance.tootheCount = 3;
            ToothCount(StatusManager.instance.tootheCount);
        }
        else
        {
            StatusManager.instance.tootheCount = 2;
            ToothCount(StatusManager.instance.tootheCount);
        }

        if (arr[1] == "1")
        {
            StatusManager.instance.tootheCount = 4;
            ToothCount(StatusManager.instance.tootheCount);
        }
        else
        {
            
        }

        if (arr[2] == "1")
        {
            StatusManager.instance.tootheCount = 5;
            ToothCount(StatusManager.instance.tootheCount);
        }
        else
        {
           // speed = 1f;
        }

        if (arr[3] == "1")
        {
           // bulletNumb = 3;
        }
        else
        {
           // bulletNumb = 1;
        }

        if (arr[4] == "1")
        {
           // damageMultiply = 4;
        }
        else
        {
            //damageMultiply = 2;
        }

        if (arr[5] == "1")
        {
          //  speed = 3f;
        }
        else
        {

        }

        if (arr[6] == "1")
        {
           // bulletNumb = 4;
        }
        else
        {
           // bulletNumb = 3;
        }

        if (arr[7] == "1")
        {

        }
        else
        {

        }

        if (arr[8] == "1")
        {

        }
        else
        {

        }

    }

    public void UpGradeSkill(int numb)
    {
        string[] arr2 = upgradeCost.Split(',');
        int dia = int.Parse(arr2[numb]);
        if (dia <= StatusManager.instance.dia)
        {
            StatusManager.instance.dia = StatusManager.instance.dia - dia;

            //upGrade = StatusManager.instance.fairySkillSGP;
            string[] arr = StatusManager.instance.teethUpgrade.Split(new char[] { ',' });

            arr[numb] = "1";

            StatusManager.instance.teethUpgrade = string.Join(",", arr);
            //StatusManager.instance.fairySkillSGP = upGrade;
            CheckUpGrade();
            SetButtons();
        }





    }

    // Update is called once per frame
    public void ToothCount(int numb)
    {
        for (int i = 0; i < toothUI.Length; i++)
        {
            toothUI[i].SetActive(false);
        }
        for (int j = 0; j < numb; j++)
        {
            toothUI[j].SetActive(true);
        }

        if (numb > 2)
        {
            for (int k = 0; k < numb; k++)
            {
                toothUI[k].GetComponentInChildren<ScoreToothController>().Good();
            }
        }
        else if(numb == 2)
        {
            for (int l = 0; l < numb; l++)
            {
                toothUI[l].GetComponentInChildren<ScoreToothController>().Bad();
            }
        }
        else
        {
            for (int m = 0; m < numb; m++)
            {
                toothUI[m].GetComponentInChildren<ScoreToothController>().Worst();
            }
        }
    }
}
