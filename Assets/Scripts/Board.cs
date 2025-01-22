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

        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 1.6f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount = arr.Length;

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
}
