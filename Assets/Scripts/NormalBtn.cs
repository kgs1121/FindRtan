using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NormalBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image fadeImage; 
    public float fadeSpeed = 1.0f;
    public GameObject explains;

    int BtnIdx;
    string nameNum;

    private void Start()
    {
        nameNum = this.name[0].ToString();
        BtnIdx=int.Parse(nameNum);
    }
    public void Retry(int n)
    {
    
        StartCoroutine(FadeInAndRetry(n));
    }
    IEnumerator FadeInAndRetry(int n)
    {
 
        
        Color color = fadeImage.color;
        fadeImage.color = color;

        float alpha = 0f;

        while (alpha <fadeSpeed)
        {
            float deltaAlpha = Mathf.Max(Time.deltaTime * fadeSpeed, 0.003f);
            alpha += deltaAlpha;
            fadeImage.color = new Color(0,0,0,alpha) ;
            yield return null;
        }


        // 씬 로드
        PlayerPrefs.SetInt("Diff", n);
        SceneManager.LoadScene("MainScene");
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject explain = explains.transform.GetChild(BtnIdx).gameObject;
        explain.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject explain = explains.transform.GetChild(BtnIdx).gameObject;
        explain.SetActive(false);
    }

}
