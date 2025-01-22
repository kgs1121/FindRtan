using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecord : MonoBehaviour
{
    public Text ScoreTxt0;
    public Text ScoreTxt1;
    public Text ScoreTxt2;
    public Text ScoreTxt3;

    private void Start()
    {
        updateScore();
    }
    public void updateScore()
    {
        ScoreTxt0.text = PlayerPrefs.GetFloat($"level0HighScore").ToString("N0");
        ScoreTxt1.text = PlayerPrefs.GetFloat($"level1HighScore").ToString("N0");
        ScoreTxt2.text = PlayerPrefs.GetFloat($"level2HighScore").ToString("N0");
        ScoreTxt3.text = PlayerPrefs.GetFloat($"level3HighScore").ToString("N0");
    }
}
