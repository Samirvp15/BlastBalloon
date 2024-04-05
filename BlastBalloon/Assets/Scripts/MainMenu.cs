using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject GameOverScreen;
    public GameObject GamePausedScreen;
    public GameObject SettingsScreen;
    private GameObject activeScreen;
    public static bool gameOver = false;
    public static bool gamePaused = false;
    //private ScoreManager scoreManager;
    // Start is called before the first frame update

    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        activeScreen = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetActiveScreen(GameObject newActiveScreen)
    {
        // Desactivar el screen actual si hay uno activo
        if (activeScreen != null)
        {
            activeScreen.SetActive(false);
        }

        // Activar el nuevo screen
        newActiveScreen.SetActive(true);
        activeScreen = newActiveScreen;
    }

    public void GameOver()
    {
        SetActiveScreen(GameOverScreen);
        
    }

    public void Settings()
    {
        audioManager.PlaySFXButton();
        SetActiveScreen(SettingsScreen);
    }


    public void closeSettings()
    {
        audioManager.PlaySFXButton();
        SettingsScreen.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }
    public void GamePaused()
    {
        audioManager.PlaySFXButton();
        SetActiveScreen(GamePausedScreen);
        Time.timeScale = 0;
        gamePaused = true;
    }
    public void closeGamePaused()
    {
        audioManager.PlaySFXButton();
        GamePausedScreen.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void PlayGame()
    {
        
        SceneManager.LoadScene("GameScene");
        ScoreManager.scoreCount = 0;
    }
    public void RestartGame()
    {
        audioManager.PlaySFXButton();
        //RESTAURAR VALORES
        ScoreManager.scoreCount = 0;
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;

        //Recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void goToMainMenu()
    {
        audioManager.PlaySFXButton();
        SceneManager.LoadScene("HomeMenu");
        Time.timeScale = 1;
        gamePaused = false;
    }
   
}
