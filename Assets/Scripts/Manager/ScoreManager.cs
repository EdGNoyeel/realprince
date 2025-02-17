using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;
        
    //[SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] public TextMeshProUGUI theScore = null;
    //[SerializeField] TextMeshProUGUI theRemain = null;
    [SerializeField] TextMeshProUGUI vicCon = null;
    //[SerializeField] TextMeshProUGUI vicMent = null;
    [SerializeField] Animator scoreAnim;
    //[SerializeField] GameObject getScorePrefab;
    [SerializeField] GameObject getScorePosition;
    //[SerializeField] UnityEngine.UI.Text toothCount = null;
    public int currentScore;
    public GameObject[] tooth;
    public int remainTooth=0;
    public float tbDamage = 1;
    public string vicConTxt;
    public string vicMentTxT = null;
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때 
        {
            instance = this; //내자신을 instance로 넣어줍니다. 
            //DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지 
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미 
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = StatusManager.instance.score;
        //txtScore.text = "0";
        tooth = GameObject.FindGameObjectsWithTag("Tooth");
        remainTooth = StatusManager.instance.tootheCount;
        //vicMent.text = vicMentTxT;
    }

    // Update is called once per frame
    void Update()
    {
        theScore.text = ExtensionNumber.ToMoneyString(StatusManager.instance.score);
        //txtScore.text = string.Format("{0:#,##0}", currentScore);
        //theRemain.text = remainTooth.ToString();
        vicCon.text = vicConTxt;
    }

    /*public void updateVicMent()
    {
        vicMent.text = vicMentTxT;
    }*/
    public void IncreaseScore(int _score)
    {
        currentScore += _score;
        scoreAnim.SetTrigger("GetScore");
        StatusManager.instance.score = currentScore;
        //GameObject _obj = Instantiate(getScorePrefab);

        GameObject _obj = ObjPuller.instance.objectPoolList[8].Dequeue();

        //newGameObject1.transform.position = targetTooth.transform.position;
        _obj.SetActive(true);

        _obj.GetComponent<TextMeshProUGUI>().text="+"+_score.ToString();
        _obj.transform.SetParent(getScorePosition.transform, false);
        //_obj.transform.position = getScorePosition.transform.position;
    }
    public void DecreaseToothCount()
    {
        
        remainTooth--;
        GameObject stage = GameObject.Find("StageManager");
        GameObject.Find("ToothScore").GetComponent<ScoreToothManager>().ToothCount(remainTooth);
        //stage.GetComponentInChildren<EnemyCreater>().CalStars();
        //StatusManager.instance.tootheCount = remainTooth;
        if (remainTooth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
