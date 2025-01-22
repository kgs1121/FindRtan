using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HardBtn : MonoBehaviour
{
   public Image fadeImage; 
    public float fadeSpeed = 1.0f; 

    public void Hard_Retry()
    {
        PlayerPrefs.SetInt("Diff", 1);
        SceneManager.LoadScene("MainScene");
    }

     /*IEnumerator FadeInAndRetry()
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
        
    }*/
}
