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
    public static float countdownTimerCircularBar = 5;
    public static float maxTimer = 0;
    private MainMenu Canvas;
    public GameObject PanelReviveAd;
    Button rewardedButtonAd;

    TimeSpan time = TimeSpan.Zero;
   

    float currentTime;
    float startMinutes = 2.0f;
    public TextMeshProUGUI currentTimeText;
    
    void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<MainMenu>();
        CircularBarFilled = GameObject.Find("CircularBarFilled").GetComponent<Image>();
        rewardedButtonAd = GameObject.Find("RewardedAdButton").GetComponent<Button>();
        currentTimeText = GameObject.Find("TimerCountDownText").GetComponent<TextMeshProUGUI>();

        maxTimer = countdownTimerCircularBar;

        currentTime = startMinutes * 60;

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
           
            int countRewardedAdsWatchedQuit = PlayerPrefs.GetInt("countRewardedAdsWatched", 0);

            Debug.Log("YAAA numero de counts AD: " + countRewardedAdsWatchedQuit);




            if (countRewardedAdsWatchedQuit < GameManager.Instance.numberAdstoWatch)
            {
                //CountDown CircleBar Progress
                if (countdownTimerCircularBar >= 0)
                {
                    countdownTimerCircularBar -= Time.deltaTime;
                    CircularBarFilled.fillAmount = countdownTimerCircularBar / maxTimer;
                }
                else
                {
                    Debug.Log("SUS");
                    countdownTimerCircularBar = maxTimer;
                    MainMenu.gameOver = true;
                    Canvas.CloseReviveAdScreen();
                   
                }
            }
            else
            {
                
                currentTimeText.gameObject.SetActive(true);
                rewardedButtonAd.interactable = false;
                CircularBarFilled.gameObject.SetActive(false);

                //--------------INTERACCION A LA API WORLD TIME -----------

                //AQUI EMPIEZA EL CONTEO 
                //if (!GameManager.Instance.isRewardedAdOnCountDown)
                int isRewardedAdOnCountDown = PlayerPrefs.GetInt("isRewardedAdOnCountDown", 0);
                if (isRewardedAdOnCountDown == 0)
                {
                    //DateTime currentStartDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    //GameManager.Instance.StartAPIDateTime = currentStartDateTime;


                    DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;
                    GameManager.Instance.isRewardedAdOnCountDown = true;
                    PlayerPrefs.SetInt("isRewardedAdOnCountDown", 1);

                    Debug.Log("ACTUAL TIME??:   " + GameManager.Instance.CurrentAPIDateTime);
                }

                int xdd = PlayerPrefs.GetInt("xdd", 0);
                // if (!GameManager.Instance.xdd )
                if (xdd == 0 )
                {
                    DateTime currentDateTime = DateTime.Now;
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;

                    Debug.Log("END TIME??:   " + GameManager.Instance.EndAPIDateTime);
                    Debug.Log("ACTUAL TIME:   " + GameManager.Instance.CurrentAPIDateTime);

                    

                    //LAPSO DE TIEMPO FUERA DEL JUEGO (APP CERRADA)   
                    string dateQuitString = PlayerPrefs.GetString("dateQuit", "");
                    Debug.Log("FECHA DE REINGRESO:  "+ dateQuitString);

                    if (!dateQuitString.Equals(""))
                    {
                        DateTime dateQuit = DateTime.Parse(dateQuitString);
                        DateTime dateNow = DateTime.Now;

                        if (dateNow > dateQuit)
                        {
                            GameManager.Instance.TimePassed = dateNow - dateQuit;

                            string StringTimeQuit = PlayerPrefs.GetString("TimeQuit", "");
                            TimeSpan TimeQuiteFromExit = TimeSpan.Parse(StringTimeQuit);

                            //time = GameManager.Instance.TimeQuit - GameManager.Instance.TimePassed;
                            time = TimeQuiteFromExit - GameManager.Instance.TimePassed;

                            Debug.Log("TimeQuit from QUITT:    " + TimeQuiteFromExit);
                            Debug.Log("TIME PASSED: " + GameManager.Instance.TimePassed);
                            Debug.Log("tiempo de continuidad from QUIT:    " + time);
                        }

                        PlayerPrefs.SetString("dateQuit", "");
                    }
                    else
                    {
                        //LAPSO DE TIEMPO DENTRO DEL JUEGO
                        time = GameManager.Instance.TimeQuit - (GameManager.Instance.TimeAPIElapsedSecond());


                        Debug.Log("TimeQuit:    " + GameManager.Instance.TimeQuit);
                        Debug.Log("lapso de tiempo MALO:    " + GameManager.Instance.TimeAPIElapsed());
                        Debug.Log("lapso de tiempo BUENO?:    " + GameManager.Instance.TimeAPIElapsedSecond());
                        Debug.Log("tiempo de continuidad:    " + time);
                    }

                    
                    GameManager.Instance.xdd = true;
                    PlayerPrefs.SetInt("xdd", 1);
                }


                int FirstCountDownTimer = PlayerPrefs.GetInt("FirstCountDownTimer", 0);
                if (FirstCountDownTimer == 0)
                {
                    currentTime -= Time.deltaTime;
                    time = TimeSpan.FromSeconds(currentTime);
                    Debug.Log("1 sola VEZ");
                }
                else
                {
                    Debug.Log("varias veces");
                    time -= TimeSpan.FromSeconds(Time.deltaTime);
                }


                

                if (time < TimeSpan.Zero)
                {
                    //RESET VALORES
                    Debug.Log("ESTO AL FINAL TODAVIAA");
                    currentTimeText.gameObject.SetActive(false);
                    rewardedButtonAd.interactable = true;
                    CircularBarFilled.gameObject.SetActive(true);

                    currentTime = startMinutes * 60;
                    time = TimeSpan.Zero;

                    MainMenu.countRewardedAdsWatched = 0;
                    PlayerPrefs.SetInt("isRewardedAdOnCountDown", 0);
                    GameManager.Instance.isRewardedAdOnCountDown = false;
                    PlayerPrefs.SetInt("countRewardedAdsWatched", 0);
                    PlayerPrefs.SetString("TimeQuit", "");

                    GameManager.Instance.FirstCountDownTimer = false;
                    PlayerPrefs.SetInt("FirstCountDownTimer", 0);
                    GameManager.Instance.xdd = true;
                    PlayerPrefs.SetInt("xdd", 1);


                }

               
                currentTimeText.text = "Restart in :   " + string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);

                GameManager.Instance.TimeQuit = time;

                string TimeQuit = time.ToString();
                PlayerPrefs.SetString("TimeQuit", TimeQuit);
            }

        }

    }
}
