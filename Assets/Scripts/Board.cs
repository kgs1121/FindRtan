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

    // ī����� �� ���� ������ �ϴ� �Լ�
    IEnumerator PlaceCards(int[] arr)
    {
        GameObject[] cardObjects = new GameObject[12];
        Vector2[] targetPositions = new Vector2[12];

        // ī�� �ν��Ͻ�ȭ �� ��ǥ ��ġ ���
        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(card, transform);
            go.transform.position = Vector2.zero;  // ���� ��ġ (0,0)
            go.GetComponent<Card>().Setting(arr[i]);

            // ��ǥ ��ġ ���
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 1.6f;
            targetPositions[i] = new Vector2(x, y);

            // �迭�� ī�� �ν��Ͻ� ����
            cardObjects[i] = go;
        }

        // ��� ī�尡 ���ÿ� �̵��ϵ��� �ִϸ��̼�
        float moveDuration = 1f;  // �̵��� �ɸ��� �ð�
        float timeElapsed = 0f;

        // �� ���� ��� ī�尡 ��ǥ ��ġ�� �̵�
        while (timeElapsed < moveDuration)
        {
            for (int i = 0; i < 12; i++)
            {
                // �� ī���� ���� ��ġ�� ��ǥ ��ġ ���̸� ����
                cardObjects[i].transform.position = Vector2.Lerp(cardObjects[i].transform.position, targetPositions[i], timeElapsed / moveDuration);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ��� ī�带 ��Ȯ�� ��ǥ ��ġ�� ��ġ
        for (int i = 0; i < 12; i++)
        {
            cardObjects[i].transform.position = targetPositions[i];
        }

        GameManager.Instance.cardCount = arr.Length;
    }
}
