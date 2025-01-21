using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int difficulty;

    public Card firstCard;
    public Card secondCard;
    public Collection thirdCard;

    public Text timeTxt;
    public GameObject endTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;

    public int cardCount = 0;
    float time = 0.0f;

    public Transform board;

    private void Awake()
    {
       
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= 30.00)
        {
            Time.timeScale = 0f;
            endTxt.SetActive(true);
        }
    }

    public void third()
    {
        if (firstCard.idx == secondCard.idx)
        {
            foreach (Transform Card in board)     // �̸� ����� Ȱ��ȭ �Ǵµ��� ��Ȱ��ȭ �Ǵ� Card������Ʈ�� ���� ��Ӱ��ϱ�
            {
                Transform back = Card.Find("Back");
                SpriteRenderer cardSprite = back.GetComponent<SpriteRenderer>();
                cardSprite.color = new Color(100f, 100f, 100f);
            }
            Collection.canCollect = true;
           
        }
        else
        {
            //�ݾ�
            firstCard.CloseCard();
            secondCard.CloseCard();

            firstCard = null;
            secondCard = null;

        }
    }
    public void Matched()
    {
        
        if (thirdCard.idex == secondCard.idx)
        {
            
            thirdCard.front.SetActive(true);
            thirdCard.back.SetActive(false);
            //�ı�
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                Time.timeScale = 0f;
                endTxt.SetActive(true);
            }

        }
        else
        {
            audioSource.PlayOneShot(clip2);
            //�ݾ�
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        thirdCard = null;
        

    }

    public void Matched_Normal()
    {
        if (firstCard.idx == secondCard.idx)
        {
            var collectionObject = board.Cast<Transform>()
                .Where(child => child.name == "Collection(Clone)")
                .Select(child => child.gameObject)
                .ToList();
            foreach (var v in collectionObject)
            {
                GameObject find = v.transform.Find("Front").gameObject;
                if (!find.activeSelf)
                {
                    find.SetActive(true);
                    v.transform.Find("Back").gameObject.SetActive(false);
                    find.GetComponent<SpriteRenderer>().sprite = firstCard.frontImage.sprite;
                    v.GetComponent<Collection>().nameTxt.text = v.GetComponent<Collection>().names[firstCard.idx];
                    break;
                }
            }
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                Time.timeScale = 0f;
                endTxt.SetActive(true);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2);
            //�ݾ�
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
}
