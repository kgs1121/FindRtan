using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberData : MonoBehaviour
{
    public string[] memberName = new string[6] { "진희원", "강기수", "김민성", "박호준", "유재혁", "???" };


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
        // 실패시 못맞춘 멤버 사진 대신 ?마크 띄우는 곳


        Text txt = GetComponentInChildren<Text>();
        txt.text = "<b>" + memberName[i] + "</b>";
    }



}
