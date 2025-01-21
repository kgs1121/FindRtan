using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class resultPopup : MonoBehaviour
{
    private GameManager manager;

    private float takenTime=0f;

    public Text popupName;

    public GameObject memberInfo;
    private GameObject[] member = new GameObject[6];

    private int level = 0;

    private int nowScore = 0;
    private int highScore = 0;

    private bool isFirst=true;


    private Text nowScoreText;
    private Text highScoreText;

    private Button restart;
    private Button toMain;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
            manager = GameManager.Instance;



        takenTime = manager.time;
        nowScore = 0;

        if (isFirst)
        {
            isFirst = false;
            SetPopupText();
            SetMemberInfo();
        }
    }

    private void SetMemberInfo()
    {
        Transform grid = transform.Find("MemberInfoGrid");

        for (int i = 0; i < member.Length; i++)
        {
            GameObject go = Instantiate(memberInfo, grid);
            go.AddComponent<LayoutElement>();
            Image[] img = go.transform.GetComponentsInChildren<Image>();

            //img[0].sprite= Resources.Load<Sprite>($"");
            img[1].sprite = Resources.Load<Sprite>($"member{i}");

            Text txt = go.GetComponentInChildren<Text>();
            txt.text = manager.memberName[i];
        }
    }


    private void SetPopupText()
    {
        popupName.text = "<b>게임 결과</b>";
    }

    private void ScoreCheck()
    {
        highScore = level == 0 ? manager.normalScore : manager.hardScore;


        if (false)    //ī�� ��Ī ���н�
        {
            nowScore = 0;
            return;
        }


        // ���� = �Ҹ�ð� / �õ�Ƚ��
        nowScore = (int)manager.time / manager.cardCount;

        if(nowScore>highScore)
        {
            // ���� �ְ��������� ���� ������ ������ ����
            highScore = nowScore;
            if(level == 0 )
            {
                manager.normalScore = highScore;
            }
            else
            {
                manager.hardScore = highScore;
            }
        }

        nowScoreText.text = "현재 점수 : ";
        nowScoreText.text += nowScore.ToString();
        highScoreText.text = "최고 점수 : ";
        highScoreText.text += highScore.ToString();
    
    }





    private void Restart()
    {
        // ����� ��� �ۼ��ؼ� restart ��ư�� ����
    }

    private void MoveMainScene()
    {
        // ���� ������ �̵� ��� �ۼ��ؼ� toMain ��ư�� ����
    }

}
