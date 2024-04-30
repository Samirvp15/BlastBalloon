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

        /*if (gamePlayed % 3 == 0)
        {
            //AL QUINTO RESTART GAME SE PRODUCE EL AD
            AdsManager.Instance.rewardedAdsButton.ShowAd();
            AdsManager.Instance.rewardedAdsButton.OnUnityAdsShowComplete(InterstitialAds._adUnitId, UnityAdsShowCompletionState.COMPLETED);
            
        }*/

        SetActiveScreen(GameOverScreen);
        isRewarded = false;
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


        //StartCoroutine(DisplayBannerWithDelay());


    }

    private IEnumerator DisplayBannerWithDelay()
    { 
        yield return new WaitForSeconds(1f);
        AdsManager.Instance.interstitialAds.ShowAd();
    }

    public void RestartGame()
    {
        audioManager.PlaySFXButton();
        //RESTAURAR VALORES
        isRewarded = false;
        ScoreManager.scoreCount = 0;
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;
        gamePlayed++;
        ProgressBar.FirstStar = false;
        ProgressBar.SecondtStar = false;
        ProgressBar.ThirdStar = false;


        if (gamePlayed % 5 == 0)
        {
            //AL QUINTO RESTART GAME SE PRODUCE EL AD
            AdsManager.Instance.interstitialAds.ShowAd();
            AdsManager.Instance.interstitialAds.OnUnityAdsShowComplete(InterstitialAds._adUnitId, UnityAdsShowCompletionState.COMPLETED);
        }
        


        //Recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void goToMainMenu()
    {
        audioManager.PlaySFXButton();
        SceneManager.LoadScene("HomeMenu");
        Time.timeScale = 1;
        gamePaused = false;
        gamePlayed = 0;
    }
   
}
