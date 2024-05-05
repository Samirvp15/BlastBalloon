using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public  GameObject GameOverScreen;
    public  GameObject GamePausedScreen;
    public  GameObject SettingsScreen;
    public  GameObject activeScreen;
    public static bool gameOver = false;
    public static bool gamePaused = false;

    //FOR ADS
    public static int gamePlayed = 0;
    public static bool isRewarded = false;

    // Start is called before the first frame update

    AudioManager audioManager;
    void Start()
    {
        //RESTAURAR VALORES
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;

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

        /*if (gamePlayed % 3 == 0)
        {
            //AL QUINTO RESTART GAME SE PRODUCE EL AD
            AdsManager.Instance.rewardedAdsButton.ShowAd();
            AdsManager.Instance.rewardedAdsButton.OnUnityAdsShowComplete(InterstitialAds._adUnitId, UnityAdsShowCompletionState.COMPLETED);
            
        }*/

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
    }


    public void RestartGame()
    {
        audioManager.PlaySFXButton();
        //RESTAURAR VALORES
        
        


        /*if (gamePlayed % 5 == 0)
        {
            //AL QUINTO RESTART GAME SE PRODUCE EL AD
            AdsManager.Instance.interstitialAds.ShowAd();
            AdsManager.Instance.interstitialAds.OnUnityAdsShowComplete(InterstitialAds._adUnitId, UnityAdsShowCompletionState.COMPLETED);
        }*/
        


        //Recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void goToMainMenu()
    {
        audioManager.PlaySFXButton();
        SceneManager.LoadScene("HomeMenu");

    }
   
}
