using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
    public Text trynum;
    public Text waitingTimeTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;

    public List<int> lefts = new List<int> { 0, 1, 2, 3, 4, 5 };

    public int cardCount = 0;
    public float waitingTime;
    public float time = 0.0f;
    private float timeLimit = 0f;

    public Canvas mainCanvas;
    public GameObject resultPopup;

    public float normalScore = 0f;
    public float hardScore = 0f;
    public int tryFlip;

    public Transform board;


    private void Awake()
    {
       
        if (Instance == null) Instance = this;
        difficulty = PlayerPrefs.GetInt("Diff");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (difficulty == 0)
        {
            foreach(Transform Card in board)
            {
                if (Card.name.Contains("Card"))
                {
                    Transform front = Card.Find("Front");
                    front.gameObject.SetActive(true);
                    Transform back = Card.Find("Back");
                    back.gameObject.SetActive(false);
                }
            }
        }
        waitingTimeTxt.gameObject.SetActive(true);
        tryFlip = 0;
        trynum.text = tryFlip.ToString();
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        waitingTime = 3f;
        time = 30f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingTime >= 0)
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            if (waitingTimeTxt.gameObject.activeSelf)
            {
                foreach (Transform Card in board)
                {
                    if (Card.name.Contains("Card"))
                    {
                        Transform front = Card.Find("Front");
                        front.gameObject.SetActive(false);
                        Transform back = Card.Find("Back");
                        back.gameObject.SetActive(true);
                    }
                }
                waitingTimeTxt.gameObject.SetActive(false);
                Card.canOpen = true;
            }
            time -= Time.deltaTime;
        }
        if (waitingTime <= 0)
        {
            waitingTimeTxt.text = "0";
        }
        else
        {
            waitingTimeTxt.text = Mathf.Ceil(waitingTime).ToString();
        }
        
        timeTxt.text = time.ToString("N2");

        if (time < timeLimit)
        {
            Time.timeScale = 0f;
            time = timeLimit;
            //endTxt.SetActive(true);
            Card.canOpen = false;
            Collection.canCollect = false;
            Instantiate(resultPopup,mainCanvas.transform);
        }
    }

    public void third()
    {
        if (firstCard.idx == secondCard.idx)
        {
            changeColor(1f);
          
            Collection.canCollect = true;

        }
        else
        {
            GameManager.Instance.tryFlip++;
            //Debug.Log(tryFlip);
            GameManager.Instance.trynum.text = GameManager.Instance.tryFlip.ToString();
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

            //파괴
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            lefts.Remove(thirdCard.idex);

            cardCount -= 2;
            if(cardCount == 0)
            {
                Time.timeScale = 0f;
                Instantiate(resultPopup, mainCanvas.transform);

            }

        }
        else
        {
            if (difficulty == 3)
            {
                time -= 1f;
            }
            //닫아
            audioSource.PlayOneShot(clip2);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        thirdCard = null;

        changeColor(-1f);
    }
    public void changeColor(float n)  //선택가능한 활성화 카드들은 흰색  선택불가능한 비활성 카드들은 회색으로 바꾸는 매서드 (1이면 하단카드 활성, -1이면 상단카드 활성)
    {
            foreach (Transform all in board)  
            {
                if (all.name.Contains("Card"))
                {
                    Transform back = all.Find("Back");
                    SpriteRenderer cardSprite = back.GetComponent<SpriteRenderer>();
                    cardSprite.color = new Color(0.8f-0.2f*n, 0.8f - 0.2f * n, 0.8f - 0.2f * n);
                }
                else
                {
                    Transform back = all.Find("Back");
                    SpriteRenderer cardSprite = back.GetComponent<SpriteRenderer>();
                    cardSprite.color = new Color(0.8f + 0.2f * n, 0.8f + 0.2f * n, 0.8f + 0.2f * n);
                }
            }
       
        
        
    }



    public float GetLimitTime()
    {
        return timeLimit;
    }

    public void Matched_Normal()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
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
            lefts.Remove(secondCard.idx);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            lefts.Remove(firstCard.idx);
            cardCount -= 2;
            if (cardCount == 0)
            {
                Time.timeScale = 0f;
                Instantiate(resultPopup, mainCanvas.transform);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2);
          
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
}
