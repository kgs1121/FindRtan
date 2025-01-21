using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;

public class StartImgname : MonoBehaviour
{
    public Image imageComponent;    // UI Image ������Ʈ
    public Text textComponent;      // UI Text ������Ʈ
    public string[] members;  // �̹��� �̸� �迭
    public string[] texts;    // �ؽ�Ʈ �迭
    private int membernum;
    public float changeInterval;  // �̹����� �ؽ�Ʈ ���� ����
    private int currentIndex = 0;  // ���� �̹����� �ؽ�Ʈ�� �ε���

    private List<(string imageName, string text)> shuffledPairs;  // Ʃ�÷� �̹����� �ؽ�Ʈ �� ����


    // Start is called before the first frame update
    void Start()
    {
        membernum = 6;
        changeInterval = 1f;
        memberarr();
        StartCoroutine(ChangeImage());
    }

    

    void memberarr()
    {
        members = new string[membernum];  // "member0"���� "member5"���� 6���� ��ҷ� �迭 ũ�� ����

        for (int i = 0; i < members.Length; i++)
        {
            members[i] = $"member{i}";
        }
        texts = new string[] { "�����", "�����", "��μ�", "��ȣ��", "������", "��ź"};
    }


    void ShuffleArray()
    {
        shuffledPairs = new List<(string, string)>();  // Ʃ�� ����Ʈ �ʱ�ȭ

        for (int i = 0; i < members.Length; i++)
        {
            shuffledPairs.Add((members[i], texts[i]));
        }

        for (int i = shuffledPairs.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffledPairs[i];  // ���� �ε����� �ӽ÷� ����
            shuffledPairs[i] = shuffledPairs[j];  // ���� �ε����� ���� �ε����� ��ȯ
            shuffledPairs[j] = temp;  // ���� �ε����� �ӽ÷� ������ ������ ��ȯ
        }
    }





    IEnumerator ChangeImage()
    {
        int changecard = 0;

        while (true)
        {
            if (changecard % membernum == 0 || changecard == 0)
            {
                ShuffleArray();  // �迭 ����

            }
            var pair = shuffledPairs[currentIndex];

            // �̹��� �ε�
            Sprite sprite = Resources.Load<Sprite>(pair.imageName);  // �̹��� �ε�

            if (sprite != null)
            {
                imageComponent.sprite = sprite;  // �̹��� ����
                textComponent.text = pair.text;  // �ؽ�Ʈ�� �Բ� ����
                changecard++;
            }
            else
            {
                Debug.LogWarning($"�̹��� �ε� ����: {members[currentIndex]}");
            }

            currentIndex = (currentIndex + 1) % shuffledPairs.Count;  // ��ȯ�ϵ��� ����


            // ���� ���ݸ�ŭ ���
            yield return new WaitForSeconds(changeInterval);
        }
    }





}
