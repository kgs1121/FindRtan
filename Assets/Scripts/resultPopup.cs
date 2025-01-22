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

        if (isFirst)
        {
            isFirst = false;
            SetPopupText();
        }
        newMark = transform.Find("HighScoreImage").GetChild(2).gameObject;
        newMark.SetActive(false);
        SetMemberInfo();
        ScoreCheck();
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

        
        
    }

    private void ScoreCheck()
    {
        if (GameManager.Instance.tryFlip == 0)
        {
            nowScore = 0;
        }
        else
        {
            nowScore = GameManager.Instance.time / GameManager.Instance.tryFlip * 100;
        }
        switch (GameManager.Instance.difficulty)
        {
            case 0:
                {
                    if (PlayerPrefs.HasKey("normalHighScore")) //기존의 노멀 하이스코어가 존재한다면
                    {
                        highScore = PlayerPrefs.GetFloat("normalHighScore");  //하이스코어 값을 노멀하이스코어값으로 지정
                        if (nowScore > highScore) // 현재점수가 기존 최고점수보다 높다면 최고점수 갱신
                        {
                            newMark.SetActive(true);
                            PlayerPrefs.SetFloat("normalHighScore", nowScore);
                            highScore = nowScore;
                        }
                        else
                        {
                            highScore = PlayerPrefs.GetFloat("normalHighScore");
                        }
                    }
                    else                                    //기존의 노멀 하이스코어가 없다면
                    {
                        newMark.SetActive(true);
                        PlayerPrefs.SetFloat("normalHighScore", nowScore);  //현 스코어를 하이스코어에 저장
                        highScore = nowScore;
                    }
                    break;
                }
            case 1:
                {
                    if (PlayerPrefs.HasKey("hardHighScore"))
                    {
                        if (nowScore > PlayerPrefs.GetFloat("hardHighScore"))
                        {
                            newMark.SetActive(true);
                            PlayerPrefs.SetFloat("hardHighScore", nowScore);
                            highScore = nowScore;
                        }
                        else
                        {
                            highScore = PlayerPrefs.GetFloat("hardHighScore");
                        }
                    }
                    else
                    {
                        newMark.SetActive(true);
                        PlayerPrefs.SetFloat("hardHighScore", nowScore);  //현 스코어를 하이스코어에 저장
                        highScore = nowScore;
                    }
                break ;
                }
            default: break;
        }
        

        nowScoreText.text = "현재 점수 : ";
        nowScoreText.text += nowScore.ToString("N0");
        highScoreText.text = "최고 점수 : ";
        highScoreText.text += highScore.ToString("N0");
    
    }





    private void Restart()
    {
        // 재시작 기능 작성 후 restart 버튼에 연결
    }

    private void MoveMainScene()
    {
        // 메인 씬으로 이동 기능 작성 후 toMain 버튼에 연결
    }

}
