using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;

    private float membersize = 1f; 


    public static bool canOpen = true;


    void Start()
    {
        membercardsize(frontImage);
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
            GameManager.Instance.tryFlip++;
            if (GameManager.Instance.secondCard != null) return;

            audioSource.PlayOneShot(clip);
            anim.SetBool("isOpen", true);
            front.SetActive(true);
            back.SetActive(false);

     
            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = this;
            }
          
            else
            {
                GameManager.Instance.secondCard = this;
               
                if (GameManager.Instance.difficulty == 0) GameManager.Instance.Matched_Normal();
                if (GameManager.Instance.difficulty == 1) GameManager.Instance.third();
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


    public void membercardsize(SpriteRenderer sprite)
    {
        float originalWidth = sprite.sprite.bounds.size.x;
        float originalHeight = sprite.sprite.bounds.size.y;

        float scaleX = membersize / originalWidth;
        float scaleY = membersize / originalHeight;

        sprite.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }





}

