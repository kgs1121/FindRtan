using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberData : MonoBehaviour
{
    private string[] memberName = new string[6] { "�����", "�����", "��μ�", "��ȣ��", "������", "��ź" };


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
