using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberData : MonoBehaviour
{
    public string[] memberName = new string[6] { "�����", "�����", "��μ�", "��ȣ��", "������", "???" };


    public void SetMemberData(int i)
    {
        Image[] img = transform.GetComponentsInChildren<Image>();

        foreach (var index in img)
        {
            if (index.name == "Photo")
            {
                index.sprite = Resources.Load<Sprite>($"member{i}");
                break;
            }
        }

        // ���н� ������ ��� ���� ��� ?��ũ ���� ��


        Text txt = GetComponentInChildren<Text>();
        txt.text = "<b>" + memberName[i] + "</b>";
    }



}
