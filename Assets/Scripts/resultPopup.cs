using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class resultPopup : MonoBehaviour
{
    private GameManager manager;


    public Text popupName;
    public Text nowScoreText;
    public Text highScoreText;

    public GameObject memberInfo;
    private GameObject[] member = new GameObject[6];
    private GameObject newMark;
    public GameObject failMark;

    private int level;

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
        level = GameManager.Instance.difficulty;
        if (manager == null)
            manager = GameManager.Instance;

        if (isFirst)
        {
            isFirst = false;
            SetPopupUI();
        }
        newMark = transform.Find("HighScoreImage").GetChild(2).gameObject;
        newMark.SetActive(false);
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
            MemberData data = go.GetComponent<MemberData>();

            data.SetMemberData(i, leftList.Contains(i));

        }
    }


    private void SetPopupUI()
    {
        popupName.text = "<b>게임 결과</b>";

        restartText = restart.GetComponentInChildren<Text>();
        toMainText = toMain.GetComponentInChildren<Text>();

        restartText.text = "다시하기";
        toMainText.text = "처음으로";

        
        
    }

    private void ScoreCheck()
    {
        if (GameManager.Instance.time<=0)
        {
            // 실패시 점수 0점
            nowScore = 0f;
            // 실패 마크 띄우기
           failMark.SetActive(true);
        }
        else
        {
            nowScore = GameManager.Instance.time / GameManager.Instance.tryFlip * 100;
        }

        if (PlayerPrefs.HasKey($"level{GameManager.Instance.difficulty}HighScore")) //기존의 노멀 하이스코어가 존재한다면
        {
            if (nowScore >  PlayerPrefs.GetFloat($"level{GameManager.Instance.difficulty}HighScore")) // 현재점수가 기존 최고점수보다 높다면 최고점수 갱신
            {
                newMark.SetActive(true);
                PlayerPrefs.SetFloat($"level{GameManager.Instance.difficulty}HighScore", nowScore);
                highScore = nowScore;
            }
            else
            {
                highScore = PlayerPrefs.GetFloat($"level{GameManager.Instance.difficulty}HighScore");
            }
        }
        else                                    //기존의 노멀 하이스코어가 없다면
        {
            if (nowScore != 0)
            {
                newMark.SetActive(true);
            }
            PlayerPrefs.SetFloat($"level{GameManager.Instance.difficulty}HighScore", nowScore);  //현 스코어를 하이스코어에 저장
            highScore = nowScore;
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
