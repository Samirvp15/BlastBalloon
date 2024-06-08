using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class ReviveAdScreen : MonoBehaviour
{

    Image CircularBarFilled;
    public TextMeshProUGUI RevivePhrase;
    List<string> phrases = new List<string>();
    public static float countdownTimerCircularBar = 10.0f;
    public static float maxTimer = 0.0f;
    private MainMenu Canvas;
    public GameObject PanelReviveAd;
    Button rewardedButtonAd;

    TimeSpan time = TimeSpan.Zero;
   

    float currentTime;
    float startMinutes = 2.0f;
    public TextMeshProUGUI currentTimeText;
    
    void InitializePhrases()
    {
        phrases.Add("That was tough! Watch an ad to respawn and try again!");
        phrases.Add("Almost there! Watch an ad to continue and crush it!");
        phrases.Add("Don't rage quit! Watch an ad to get back in the game!");
        phrases.Add("Game over? Not yet! Watch an ad for another shot!");
        phrases.Add("Need an extra life? Watch an ad and jump back in!");
        phrases.Add("Not your best run? Watch an ad and get another shot!");
        phrases.Add("Lost the match? Watch an ad for a comeback chance!");
        phrases.Add("Out of lives? Watch an ad to recharge and fight on!");

    }
    public void ChooseRamdomRevivePhrase()
    {
        int index = UnityEngine.Random.Range(0, phrases.Count);
        RevivePhrase.text = phrases[index];
    }

    



    void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<MainMenu>();
        CircularBarFilled = GameObject.Find("CircularBarFilled").GetComponent<Image>();
        rewardedButtonAd = GameObject.Find("RewardedAdButton").GetComponent<Button>();
        currentTimeText = GameObject.Find("TimerCountDownText").GetComponent<TextMeshProUGUI>();
        maxTimer = countdownTimerCircularBar;

        currentTime = startMinutes * 60;

        InitializePhrases();
        ChooseRamdomRevivePhrase();

        PanelReviveADFadeIn();
    }

    public void PanelReviveADFadeIn()
    {
        PanelReviveAd.transform.DOScale(1f, 1f).SetEase(Ease.OutElastic);
    }

    // Update is called once per frame
    void Update()
    {

        if (MainMenu.gamePaused == true)
        {
            Time.timeScale = 1;
           
            //int countRewardedAdsWatchedQuit = PlayerPrefs.GetInt("countRewardedAdsWatched", 0);

            if ((MainMenu.countRewardedAdsWatched < GameManager.Instance.numberAdstoWatch) && GameManager.Instance.isOnline)
            { 
                //CountDown CircleBar Progress
                if (countdownTimerCircularBar >= 0)
                {
                    countdownTimerCircularBar -= Time.deltaTime;
                    CircularBarFilled.fillAmount = countdownTimerCircularBar / maxTimer;
                }
                else
                {
                    countdownTimerCircularBar = maxTimer;
                    MainMenu.gameOver = true;
                    Canvas.CloseReviveAdScreen();
                }
            }
            else
            {

                RevivePhrase.text = "Don't give up! Just blast on them!";

                currentTimeText.gameObject.SetActive(true);
                rewardedButtonAd.interactable = false;
                CircularBarFilled.gameObject.SetActive(false);

                //--------------INTERACCION A LA API WORLD TIME -----------

                int isRewardedAdOnCountDown = PlayerPrefs.GetInt("isRewardedAdOnCountDown", 0);
                if (isRewardedAdOnCountDown == 0)
                {

                    DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;
                    GameManager.Instance.isRewardedAdOnCountDown = true;
                    PlayerPrefs.SetInt("isRewardedAdOnCountDown", 1);

                }

                int NextCountDownTimer = PlayerPrefs.GetInt("NextCountDownTimer", 0);
                if (NextCountDownTimer == 0 )
                {
                    DateTime currentDateTime = DateTime.Now;
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;
         

                    //LAPSO DE TIEMPO FUERA DEL JUEGO (APP CERRADA)   
                    string dateQuitString = PlayerPrefs.GetString("dateQuit", "");

                    if (!dateQuitString.Equals(""))
                    {
                        DateTime dateQuit = DateTime.Parse(dateQuitString);
                        DateTime dateNow = DateTime.Now;

                        if (dateNow > dateQuit)
                        {
                            GameManager.Instance.TimePassed = dateNow - dateQuit;

                            string StringTimeQuit = PlayerPrefs.GetString("TimeQuit", "");
                            TimeSpan TimeQuiteFromExit = TimeSpan.Parse(StringTimeQuit);

                            time = TimeQuiteFromExit - GameManager.Instance.TimePassed;

                        }

                        PlayerPrefs.SetString("dateQuit", "");
                    }
                    else
                    {
                        //LAPSO DE TIEMPO DENTRO DEL JUEGO
                        time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsedSecond();

                    }

                    
                    GameManager.Instance.NextCountDownTimer = true;
                    PlayerPrefs.SetInt("NextCountDownTimer", 1);
                }


                int FirstCountDownTimer = PlayerPrefs.GetInt("FirstCountDownTimer", 0);
                if (FirstCountDownTimer == 0)
                {
                    //PRIMER CONTEO DEL COUNTDOWN 
                    currentTime -= Time.deltaTime;
                    time = TimeSpan.FromSeconds(currentTime);
                }
                else
                {
                    //CONTEO DEL COUNTDOWN CON TIEMPO REAL
                    time -= TimeSpan.FromSeconds(Time.deltaTime);
                }


                

                if (time < TimeSpan.Zero)
                {


                    //CHECK CONNECTION INTERNET FOR ADS
                    if (GameManager.Instance.isOnline == false)
                    {
                        AdsManager.Instance.initializeAds.InitializeAd();
                    }

                    //RESET VALORES
                    ChooseRamdomRevivePhrase();
                    currentTimeText.gameObject.SetActive(false);
                    rewardedButtonAd.interactable = true;
                    CircularBarFilled.gameObject.SetActive(true);

                    currentTime = startMinutes * 60;
                    time = TimeSpan.Zero;

                    MainMenu.countRewardedAdsWatched = 0;
                    //PlayerPrefs.SetInt("countRewardedAdsWatched", 0);
                    GameManager.Instance.isRewardedAdOnCountDown = false;
                    PlayerPrefs.SetInt("isRewardedAdOnCountDown", 0);

                    PlayerPrefs.SetString("TimeQuit", "");

                    GameManager.Instance.FirstCountDownTimer = false;
                    PlayerPrefs.SetInt("FirstCountDownTimer", 0);
                    GameManager.Instance.NextCountDownTimer = true;
                    PlayerPrefs.SetInt("NextCountDownTimer", 1);

                }

               
                currentTimeText.text = "Restart in :   " + string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);

                GameManager.Instance.TimeQuit = time;

                string TimeQuit = time.ToString();
                PlayerPrefs.SetString("TimeQuit", TimeQuit);
            }

        }

    }
}
