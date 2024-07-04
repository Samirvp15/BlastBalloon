using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIExtraLevel : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pointsText;
    public static int livesCount;
    public static int pointsCount;

    private int bestPoints;
    private float timeElapsed;
    private float highScore;
    private bool isRunning = true;


    public TextMeshProUGUI score_GameOver;
    public TextMeshProUGUI highScore_GameOver;
    public TextMeshProUGUI points_GameOver;
    public TextMeshProUGUI bestPoints_GameOver;
    //AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {

        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        pointsText = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        livesCount = 3;
        pointsCount = 0;

        timeElapsed = 0f;

        if (PlayerPrefs.HasKey("Key_HighScoreExtraGame"))
        {
            highScore = PlayerPrefs.GetFloat("Key_HighScoreExtraGame",0.0f);
        }
        
        if (PlayerPrefs.HasKey("Key_BestPointsExtraGame"))
        {
            bestPoints = PlayerPrefs.GetInt("Key_BestPointsExtraGame",0);
        }

    }


    // Update is called once per frame
    void Update()
    {


        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {

                if (livesCount <= 0)
                {
                    isRunning = false;
                }
                else
                {
                    isRunning = true;
                }

                //LIVES
                livesText.text = "" + livesCount;
                //POINTS
                pointsText.text = "" + pointsCount;

                if (pointsCount > bestPoints)
                {
                    bestPoints = pointsCount;
                    PlayerPrefs.SetInt("Key_BestPointsExtraGame", bestPoints);
                }

                if (timeElapsed > highScore)
                {
                    highScore = timeElapsed;
                    PlayerPrefs.SetFloat("Key_HighScoreExtraGame", highScore);
                }



                //GAME OVER SCREEN
                score_GameOver.text = "" + Mathf.FloorToInt(timeElapsed);
                highScore_GameOver.text = "" + Mathf.FloorToInt(highScore);
                points_GameOver.text = "" + pointsCount;
                bestPoints_GameOver.text = "" + bestPoints;



                //SCORE
                if (isRunning)
                {
                    timeElapsed += Time.deltaTime;
                    UpdateTimerUI();
                }
            }

        }


    }
    private void UpdateTimerUI()
    {
        // Formatear el tiempo como segundos
        int seconds = Mathf.FloorToInt(timeElapsed);
        scoreText.text = "" + seconds;
    }


}
