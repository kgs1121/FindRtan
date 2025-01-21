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
         Debug.Log("Retry 버튼 클릭됨");
        StartCoroutine(FadeInAndRetry());
    }
    IEnumerator FadeInAndRetry()
    {
        Debug.Log("FadeInAndRetry 코루틴 시작");
        
        Color color = fadeImage.color;
        fadeImage.color = color;

        float alpha = 0f;

        while (alpha <fadeSpeed)
        {
            float deltaAlpha = Mathf.Max(Time.deltaTime * fadeSpeed, 0.003f);
            alpha += deltaAlpha;
            Debug.Log($"alpha 값: {alpha}");
            fadeImage.color = new Color(0,0,0,alpha) ;
            yield return null;
        }
        Debug.Log("씬 로드 중...");

        // 씬 로드
        SceneManager.LoadScene("MainScene");
    }
}
