using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public Text nameTxt;
    public SpriteRenderer FrontImage;
    public int idex = 0;
  

    public static bool canCollect=false;
    
    string[] names = {"ÁøÈñ¿ø","°­±â¼ö","±è¹Î¼º","¹ÚÈ£ÁØ","À¯ÀçÇõ","¸£Åº"};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CollectSet(int num)
    {
        idex = num;
        FrontImage.sprite = Resources.Load<Sprite>($"member{num}");
        nameTxt.text = names[num];
    }

    public void openCard()
    {
        if (canCollect)
        {
            GameManager.Instance.thirdCard = this;
            GameManager.Instance.Matched();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
