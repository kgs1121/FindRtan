using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UI;

public class StartImgname : MonoBehaviour
{
    public Image imageComponent;    // UI Image 컴포넌트
    public Text textComponent;      // UI Text 컴포넌트
    public string[] members;  // 이미지 이름 배열
    public string[] texts;    // 텍스트 배열
    private int membernum;
    public float changeInterval;  // 이미지와 텍스트 변경 간격
    private int currentIndex = 0;  // 현재 이미지와 텍스트의 인덱스

    private List<(string imageName, string text)> shuffledPairs;  // 튜플로 이미지와 텍스트 쌍 저장


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
        members = new string[membernum];  // "member0"부터 "member5"까지 6개의 요소로 배열 크기 설정

        for (int i = 0; i < members.Length; i++)
        {
            members[i] = $"member{i}";
        }
        texts = new string[] { "진희원", "강기수", "김민성", "박호준", "유재혁", "르탄"};
    }


    void ShuffleArray()
    {
        shuffledPairs = new List<(string, string)>();  // 튜플 리스트 초기화

        for (int i = 0; i < members.Length; i++)
        {
            shuffledPairs.Add((members[i], texts[i]));
        }

        for (int i = shuffledPairs.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffledPairs[i];  // 현재 인덱스를 임시로 저장
            shuffledPairs[i] = shuffledPairs[j];  // 랜덤 인덱스를 현재 인덱스로 교환
            shuffledPairs[j] = temp;  // 랜덤 인덱스를 임시로 저장한 값으로 교환
        }
    }





    IEnumerator ChangeImage()
    {
        int changecard = 0;

        while (true)
        {
            if (changecard % membernum == 0 || changecard == 0)
            {
                ShuffleArray();  // 배열 섞기

            }
            var pair = shuffledPairs[currentIndex];

            // 이미지 로드
            Sprite sprite = Resources.Load<Sprite>(pair.imageName);  // 이미지 로드

            if (sprite != null)
            {
                imageComponent.sprite = sprite;  // 이미지 변경
                textComponent.text = pair.text;  // 텍스트도 함께 변경
                changecard++;
            }
            else
            {
                Debug.LogWarning($"이미지 로드 실패: {members[currentIndex]}");
            }

            currentIndex = (currentIndex + 1) % shuffledPairs.Count;  // 순환하도록 설정


            // 변경 간격만큼 대기
            yield return new WaitForSeconds(changeInterval);
        }
    }





}
