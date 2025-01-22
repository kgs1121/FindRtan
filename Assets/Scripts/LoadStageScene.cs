using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadStageScene : MonoBehaviour
{
    // Update is called once per frame
    public void LoadStage()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void LoadStar()
    {
        SceneManager.LoadScene("StarScene");
    }
    public void LoadMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
