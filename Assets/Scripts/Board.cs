using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject collection;
    
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        StartCoroutine(PlaceCards(arr));

        int[] arr2 = { 0, 1, 2, 3, 4, 5};
        arr2 = arr2.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int j = 0; j < 6; j++)
        {
            GameObject col = Instantiate(collection, this.transform);

            float x2 = j * 1.0f - 2.5f;
        

            col.transform.position = new Vector2(x2, -4);
            if (GameManager.Instance.difficulty >= 2) col.GetComponent<Collection>().CollectSet(arr2[j]);
            if (GameManager.Instance.difficulty <= 1) col.GetComponent<Collection>().nameTxt.text = "";

        }
    }

    // 카드들을 한 번에 퍼지게 하는 함수
    IEnumerator PlaceCards(int[] arr)
    {
        GameObject[] cardObjects = new GameObject[12];
        Vector2[] targetPositions = new Vector2[12];

        // 카드 인스턴스화 및 목표 위치 계산
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(card, transform);
            go.transform.position = Vector2.zero;  // 시작 위치 (0,0)
            go.GetComponent<Card>().Setting(arr[i]);

            // 목표 위치 계산
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 1.6f;
            targetPositions[i] = new Vector2(x, y);

            // 배열에 카드 인스턴스 저장
            cardObjects[i] = go;
        }

        // 모든 카드가 동시에 이동하도록 애니메이션
        float moveDuration = 1f;  // 이동에 걸리는 시간
        float timeElapsed = 0f;

        // 한 번에 모든 카드가 목표 위치로 이동
        while (timeElapsed < moveDuration)
        {
            for (int i = 0; i < 12; i++)
            {
                // 각 카드의 현재 위치와 목표 위치 사이를 보간
                cardObjects[i].transform.position = Vector2.Lerp(cardObjects[i].transform.position, targetPositions[i], timeElapsed / moveDuration);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 모든 카드를 정확히 목표 위치로 배치
        for (int i = 0; i < 12; i++)
        {
            cardObjects[i].transform.position = targetPositions[i];
        }

        GameManager.Instance.cardCount = arr.Length;
    }
}
