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
    
    public string[] names = {"�����","�����","��μ�","��ȣ��","������","��ź"};
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
            GameManager.Instance.tryFlip++;
            GameManager.Instance.thirdCard = this;
            GameManager.Instance.Matched();
            GameManager.Instance.tryFlip++;
            //Debug.Log(tryFlip);
            GameManager.Instance.trynum.text = GameManager.Instance.tryFlip.ToString();
            canCollect = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
