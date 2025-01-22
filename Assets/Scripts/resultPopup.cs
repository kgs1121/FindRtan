using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class resultPopup : MonoBehaviour
{
    private GameManager manager;

    private float takenTime=0f;

    public Text popupName;
    public Text nowScoreText;
    public Text highScoreText;

    public GameObject memberInfo;
    private GameObject[] member = new GameObject[6];
    private GameObject newMark;

    private int level = 0;

    private float nowScore = 0f;
    private float highScore = 0f;

    private bool isFirst=true;

    public Button restart;
    public Button toMain;
    private Text restartText;
    private Text toMainText;

    private List<int> leftList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
            manager = GameManager.Instance;

        takenTime = manager.time;
        manager.time = 0;
        nowScore = 0;

        if (isFirst)
        {
            isFirst = false;
            SetPopupText();
        }

        SetMemberInfo();
        ScoreCheck();

        //버튼 연결
        restart.onClick.AddListener(Restart);
        toMain.onClick.AddListener(MoveMainScene);
    }

    private void SetMemberInfo()
    {
        Transform grid = transform.Find("MemberInfoGrid");
        leftList = manager.lefts;
        for (int i = 0; i < member.Length; i++)
        {
            GameObject go = Instantiate(memberInfo, grid);
            go.AddComponent<LayoutElement>();
            MemberData data = go.GetComponent<MemberData>();

            data.SetMemberData(i, leftList.Contains(i));

        }
    }


    private void SetPopupText()
    {
        popupName.text = "<b>게임 결과</b>";

        restartText = restart.GetComponentInChildren<Text>();
        toMainText = toMain.GetComponentInChildren<Text>();

        restartText.text = "다시하기";
        toMainText.text = "처음으로";

        newMark = transform.Find("HighScoreImage").GetChild(2).gameObject;
        newMark.SetActive(false);
    }

    private void ScoreCheck()
    {
        highScore = level == 0 ? manager.normalScore : manager.hardScore;


        if (manager.cardCount>0)    //매칭 실패시
        {
            nowScore = 0;
        }
        else
        {
            if (manager.tryFlip == 0)
                manager.tryFlip = 1;
            // 현재점수 = 남은시간 / 시도횟수 * 100
            nowScore = (manager.GetLimitTime()-takenTime) / manager.tryFlip * 100;

            if (nowScore < 0)
                nowScore = 0;

            if (nowScore > highScore)
            {
                newMark.SetActive(true);
                // 현재점수가 기존 최고점수보다 높다면 최고점수 갱신
                highScore = nowScore;
                if (level == 0)
                {
                    manager.normalScore = highScore;
                }
                else
                {
                    manager.hardScore = highScore;
                }
            }
        }

        nowScoreText.text = "현재 점수 : ";
        nowScoreText.text += nowScore.ToString("N0");
        highScoreText.text = "최고 점수 : ";
        highScoreText.text += highScore.ToString("N0");
    
    }





    private void Restart()
    {
        PlayerPrefs.SetInt("Diff", manager.difficulty); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MoveMainScene()
    {
        SceneManager.LoadScene("StarScene");
    }

}
