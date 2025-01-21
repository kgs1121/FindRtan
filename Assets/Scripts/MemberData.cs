using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberData : MonoBehaviour
{
    public string[] memberName = new string[6] { "�����", "�����", "��μ�", "��ȣ��", "������", "???" };


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
        // ���н� ������ ��� ���� ��� ?��ũ ���� ��


        Text txt = GetComponentInChildren<Text>();
        txt.text = "<b>" + memberName[i] + "</b>";
    }



}
