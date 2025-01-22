using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberData : MonoBehaviour
{
    private string[] memberName = new string[6] { "ÁøÈñ¿ø", "°­±â¼ö", "±è¹Î¼º", "¹ÚÈ£ÁØ", "À¯ÀçÇõ", "¸£Åº" };


    public void SetMemberData(int i,bool isLeft)
    {
        Image[] img = transform.GetComponentsInChildren<Image>(true);

        foreach (var index in img)
        {
            if (index.name == "Failed")
            {
                index.gameObject.SetActive(isLeft);
            }
            if (index.name == "Photo")
            {
                index.sprite = Resources.Load<Sprite>($"member{i}");
            }
        }
        // ½ÇÆÐ½Ã ¸ø¸ÂÃá ¸â¹ö »çÁø ´ë½Å ?¸¶Å© ¶ç¿ì´Â °÷


        Text txt = GetComponentInChildren<Text>();
        txt.text = "<b>" + memberName[i] + "</b>";
    }



}
