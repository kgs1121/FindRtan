using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    public int cardCount = 0;
    float time = 0.0f;

    public List<int> lefts = new List<int> { 0, 1, 2, 3, 4, 5 };

    public Transform board;

    private void Awake()
    {
       
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            Card.canOpen = false;
            Collection.canCollect = false;
            
            int[] leftCards = lefts.ToArray();
            
        }
    }
    
   
    public void third()
    {
        if (firstCard.idx == secondCard.idx)
        {
            changeColor(1);
            Collection.canCollect = true;
           
        }
        else
        {
            //닫아
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
                endTxt.SetActive(true);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2);
            //닫아
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        thirdCard = null;

        changeColor(-1);
    }
    public void changeColor(float n)
    {
            foreach (Transform all in board)     // 이름 목록이 활성화 되는동안 비활성화 되는 Card오브젝트의 색깔 어둡게하기
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

}
