using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;

    public static bool canOpen = true;// 카드 열려있을때 못열게 막기


    void Start()
    {
        canOpen = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"member{idx}");
    }

    public void OpenCard()
    {
        

        if (canOpen)
        {
            if (GameManager.Instance.secondCard != null) return;

            audioSource.PlayOneShot(clip);
            anim.SetBool("isOpen", true);
            front.SetActive(true);
            back.SetActive(false);

            // firstCard가 비었다면
            if (GameManager.Instance.firstCard == null)
            {
                // firstCard에 내 정보를 념겨준다.
                GameManager.Instance.firstCard = this;
                
            }
            // firstCard가 비어있지 않다면 
            else
            {
                //secondCard에 내 정보를 념겨준다.
                GameManager.Instance.secondCard = this;
                

                //Matched 함수를 호출해 준다.
                GameManager.Instance.third();
                canOpen = false;
            }
        }
    }

    public void DestroyCard()
    {
        //Debug.Log($"canOpen check: {canOpen}");
        Invoke("DestroyCardInvoke", 0.8f);
        
       
    } 

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
        canOpen = true;
    }

    public void CloseCard()
    {
        //Debug.Log($"canOpen check: {canOpen}");
        Invoke("CloseCardInvoke", 0.8f);
        
    }

    void CloseCardInvoke()
    {
        
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        canOpen = true;

    }


    
}

