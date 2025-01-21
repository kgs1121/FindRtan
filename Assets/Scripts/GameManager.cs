using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;

    AudioSource audioSource;
    public AudioClip clip;

    public int cardCount = 0;
    public float time = 0.0f;

    public Canvas mainCanvas;
    public GameObject resultPopup;

    public int normalScore = 0;
    public int hardScore = 0;



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
            //endTxt.SetActive(true);
            Instantiate(resultPopup,mainCanvas.transform);
            time = 0;
        }
    }

    public void Matched()
    {
        
        if (firstCard.idx == secondCard.idx)
        {
            //ÆÄ±«
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
            //´Ý¾Æ
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        
        firstCard = null;
        secondCard = null;
        

    }


}
