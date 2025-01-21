using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject matchedCard;
    
    void Start()
    {
        //멤버 5명 + 1명
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 1.1f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount = arr.Length;

        // 카드 놓을 위치

        //int[] arr2 = { 0, 1, 2, 3, 4, 5 };
        for (int i = 0; i < 6; i++)
        {
            GameObject go = Instantiate(matchedCard, transform);
            float x = i - 2.1f;
            float y = -3.6f;

            go.transform.position = new Vector2( x, y );

            GameManager.Instance.matchedCards.Append(go);
        }
    }
}
