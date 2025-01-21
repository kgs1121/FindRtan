using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RetryBtn : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeSpeed = 1.0f; 
    public void Retry()
    {
    
        StartCoroutine(FadeInAndRetry());
    }
    IEnumerator FadeInAndRetry()
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
        SceneManager.LoadScene("MainScene");
    }
}
