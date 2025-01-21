using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;
    public Collection thirdCard;

    public Text timeTxt;
    public GameObject endTxt;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;

    public List<int> lefts = new List<int> { 0, 1, 2, 3, 4, 5 };

    public int cardCount = 0;
    public float time = 0.0f;
    private float timeLimit = 3f;

    public Canvas mainCanvas;
    public GameObject resultPopup;

    public float normalScore = 0f;
    public float hardScore = 0f;
    public int tryFlip = 0;

    public Transform board;


    private void Awake()
    {
       
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        time = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= timeLimit)
        {
            Time.timeScale = 0f;
            //endTxt.SetActive(true);
            Card.canOpen = false;
            Collection.canCollect = false;

            int[] leftCards = lefts.ToArray();

            Instantiate(resultPopup,mainCanvas.transform);
        }
    }

    public void third()
    {
        if (firstCard.idx == secondCard.idx)
        {
            foreach (Transform Card in board)    
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
        tryFlip++;
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
                //endTxt.SetActive(true);
                Instantiate(resultPopup, mainCanvas.transform);

            }
        }
        else
        {
            //닫아
            audioSource.PlayOneShot(clip2);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        thirdCard = null;
    }


    public float GetLimitTime()
    {
        return timeLimit;
    }

}
