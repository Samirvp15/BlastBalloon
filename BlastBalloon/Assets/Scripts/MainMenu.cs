using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{

    public  GameObject GameOverScreen;
    public  GameObject GamePausedScreen;
    public  GameObject SettingsScreen;
    public  GameObject ReviveScreen;
    public  GameObject activeScreen;
    public static bool gameOver = false;
    public static bool gamePaused = false;

    // Start is called before the first frame update

    AudioManager audioManager;
    TextMeshProUGUI LevelName;
    void Start()
    {
        //RESTAURAR VALORES
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        activeScreen = null;
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

        if (GameManager.Instance.isOnline == false)
        {
            AdsManager.Instance.rewardedAdsButton.LoadAd();
        }
       
    }

    public void QuitGame()
    {
        audioManager.PlaySFXButton();
        Application.Quit();
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

        //EXTRA LEVEL NAME
        if (GameManager.Instance.Level == 4)
        {
            LevelName = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
            LevelName.text = "Level ;)";
        }
        else
        {
            //LEVEL NAME
            LevelName = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
            LevelName.text = "Level " + GameManager.Instance.Level;
        }
        

    }
    public void closeGamePaused()
    {
        audioManager.PlaySFXButton();
        GamePausedScreen.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void RestartGame()
    {
        audioManager.PlaySFXButton();
        //RESTAURAR VALORES
        GameManager.Instance.countRewardedAdsWatched = 0;

        int num = Random.Range(0, 5);

        if (GameManager.Instance.isOnline == true && num == 3)
        {
            AdsManager.Instance.interstitialAds.ShowAd();
        }
        else
        {
            //Recargar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }  
        
    }
    public void goToMainMenu()
    {
        audioManager.PlaySFXButton();
        SceneManager.LoadScene("HomeMenu");
    }


    //ADS SECTION
    public void ShowReviveAdScreen()
    {
        Time.timeScale = 0;
        gamePaused = true;
        SetActiveScreen(ReviveScreen);
    }
    public void CloseReviveAdScreen()
    {
        ReviveScreen.SetActive(false);
        GameOver();

        //RESET VALOR COUNTDOWNTIMERCIRCLEBAR
        ReviveAdScreen.countdownTimerCircularBar = ReviveAdScreen.maxTimer;

        GameManager.Instance.countRewardedAdsWatched = 0;


        /* int isRewardedAdOnCountDown = PlayerPrefs.GetInt("isRewardedAdOnCountDown", 0);
         if (isRewardedAdOnCountDown == 1)
         {

             GameManager.Instance.FirstCountDownTimer = true;
             PlayerPrefs.SetInt("FirstCountDownTimer", 1);
             GameManager.Instance.NextCountDownTimer = false;
             PlayerPrefs.SetInt("NextCountDownTimer", 0);
             GameManager.Instance.EndAPIDateTime = GameManager.Instance.CurrentAPIDateTime;
         }
         else
         {
             //SOLO EL PRIMER CONTEO
             GameManager.Instance.FirstCountDownTimer = false;
             PlayerPrefs.SetInt("FirstCountDownTimer", 0);
         }*/
    }
    public void ShowRewardedAd()
    {
        //RESET VALOR COUNTDOWNTIMERCIRCLEBAR
        ReviveAdScreen.countdownTimerCircularBar = ReviveAdScreen.maxTimer;

        GameManager.Instance.countRewardedAdsWatched += 1;

        //PlayerPrefs.SetInt("countRewardedAdsWatched", GameManager.Instance.countRewardedAdsWatched);
        //PlayerPrefs.Save();
        //CIERRA EL ADSCREEN Y SE REPRODUCE EL ANUNCIO
        ReviveScreen.SetActive(false);
        AdsManager.Instance.rewardedAdsButton.ShowAd();
    }
   
}
