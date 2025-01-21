using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchedCard : MonoBehaviour
{
    public SpriteRenderer image;
    public Text name;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        name = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
