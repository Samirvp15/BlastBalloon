using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText_GameOver;
    public TextMeshProUGUI highScoreText_GameOver;

    //CUENTA DE GLOBOS REVENTADOS EN GAME OVER SCREEN
    private static List<GameObject> PoppedBalloonsCountIconList = new List<GameObject>();
    public GameObject[] PoppedBalloonsCountIcon;
    private static TextMeshProUGUI PoppedBalloonsCountText;

    //SCORE GENERAL
    public static int scoreCount;
    public static int highScoreCount;

    public void ResetValues()
    {
        PoppedBalloonsCountIconList.Clear();
    }


    void Start()
    {
        ResetValues();

        PoppedBalloonsCountIconList.AddRange(PoppedBalloonsCountIcon);

        if (PlayerPrefs.HasKey("Key_HighScore"))
        {
            highScoreCount = PlayerPrefs.GetInt("Key_HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetInt("Key_HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + scoreCount;
        highScoreText.text = "HighScore: " + highScoreCount;


        //GAME OVER SCREEN
        scoreText_GameOver.text = "Score: " + scoreCount;
        highScoreText_GameOver.text = "HighScore: " + highScoreCount;

        

    }
    public static void PoppedBallonCountText(string ballonType, int countBalloonType)
    {
        foreach (var item in PoppedBalloonsCountIconList)
        {
            if (item.name.Equals(ballonType))
            {
                PoppedBalloonsCountText = item.GetComponentInChildren<TextMeshProUGUI>();
                PoppedBalloonsCountText.text = countBalloonType.ToString();
            }
            
        }
    }
  
    
}
