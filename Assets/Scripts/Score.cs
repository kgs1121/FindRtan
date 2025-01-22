using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   
    public GameObject score;
    public ScoreRecord scoreRecord;

    public void openScore()
    {
        score.SetActive(true);
    }
    public void closeScore()
    {
        score.SetActive(false);
    }
    public void deleteScore()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetFloat($"level{i}HighScore",0f);
        }
        scoreRecord.updateScore();
    }
    
}
