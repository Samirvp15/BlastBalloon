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
    float startMinutes = 5.0f;
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

            if (MainMenu.countRewardedAdsWatched < GameManager.Instance.numberAdstoWatch)
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
                
                currentTimeText.gameObject.SetActive(true);
                rewardedButtonAd.interactable = false;
                CircularBarFilled.gameObject.SetActive(false);

                //--------------INTERACCION A LA API WORLD TIME -----------

                //AQUI EMPIEZA EL CONTEO 
                if (!GameManager.Instance.isRewardedAdOnCountDown)
                {
                    //DateTime currentStartDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    //GameManager.Instance.StartAPIDateTime = currentStartDateTime;


                    DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;

                    GameManager.Instance.isRewardedAdOnCountDown = true;
                    Debug.Log("ACTUAL TIME??:   " + GameManager.Instance.CurrentAPIDateTime);
                }


                if (!GameManager.Instance.xdd)
                {
                    DateTime currentDateTime = DateTime.Now;
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;

                    Debug.Log("END TIME??:   " + GameManager.Instance.EndAPIDateTime);
                    Debug.Log("ACTUAL TIME:   " + GameManager.Instance.CurrentAPIDateTime);

                    time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsedSecond();

                    /*if (GameManager.Instance.ayy == 0)
                    {
                        Debug.Log("1 vez Y YAAAA ");
                        // PRIMERA RESTA DE TIEMPOS 
                        //AQUI
                        //time = GameManager.Instance.TimeQuit - GameManager.Instance.timeSpan;
                        time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsedSecond();
                        GameManager.Instance.ayy++;
                    }
                    else
                    {
                        Debug.Log("SIIIII ");
                        //SEGUNDA RESTA PARA LOS CURRENTTIMES, NO UN CURRENTTIME CON EL STARTTIME
                        time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsedSecond();

                    }*/


                    Debug.Log("TimeQuit:    " + GameManager.Instance.TimeQuit);
                    Debug.Log("lapso de tiempo MALO:    " + GameManager.Instance.TimeAPIElapsed());
                    Debug.Log("lapso de tiempo BUENO?:    " + GameManager.Instance.TimeAPIElapsedSecond());
                    Debug.Log("tiempo de continuidad:    " + time);
                    GameManager.Instance.xdd = true;
                }


                /*
                //Obtiene por unica vez el tiempo inicial desde la API
                if (!GameManager.Instance.isRewardedAdOnCountDown)
                {
                    DateTime currentStartDateTime = WorldTimeAPI.Instance.GetCurrentDateTimeSecond();
                    GameManager.Instance.StartAPIDateTime = currentStartDateTime;

                    GameManager.Instance.isRewardedAdOnCountDown = true;
                    Debug.Log("INICIO TIME:   " + GameManager.Instance.StartAPIDateTime);
                }
                else
                {
                    /*Debug.Log("uuuu");
                    DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;
                    

                }

                //Obtiene por unica vez el tiempo actual desde la API
                if (!GameManager.Instance.xdd)
                {
                    DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                    GameManager.Instance.CurrentAPIDateTime = currentDateTime;
                    Debug.Log("ACTUAL TIME:   " + GameManager.Instance.CurrentAPIDateTime);





                    // PRIMERA RESTA DE TIEMPOS 
                    //time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsed();

                    if (GameManager.Instance.ayy == 0)
                    {
                        Debug.Log("1 vez Y YAAAA ");
                        // PRIMERA RESTA DE TIEMPOS 


                         
                        //AQUI PUEDE QUE SEA TODA LO QUE FALTA YA ESTA CASI TODO SOLO FALTA ESTO QUIZA SACARLE ESA RESTA TODO LO DEMAS ESTA BIEN
                        time = GameManager.Instance.TimeQuit;
                        GameManager.Instance.ayy++;
                        

                    }
                    else
                    {
                       

                        Debug.Log("SIIIII ");
                        //SEGUNDA RESTA PARA LOS CURRENTTIMES, NO UN CURRENTTIME CON EL STARTTIME
                        time = GameManager.Instance.TimeQuit - GameManager.Instance.TimeAPIElapsedSecond();
                       
                    }


                   
                    Debug.Log("TimeQuit:    " + GameManager.Instance.TimeQuit);
                    Debug.Log("lapso de tiempo MALO:    " + GameManager.Instance.TimeAPIElapsed());
                    Debug.Log("lapso de tiempo BUENO?:    " + GameManager.Instance.TimeAPIElapsedSecond());
                    Debug.Log("tiempo de continuidad:    " + time);
                    GameManager.Instance.xdd = true;





                }
               
                */

                if (!GameManager.Instance.FirstCountDownTimer)
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
                    
                    currentTimeText.gameObject.SetActive(false);
                    rewardedButtonAd.interactable = true;
                    CircularBarFilled.gameObject.SetActive(true);
                    currentTime = startMinutes * 60;
                    time = TimeSpan.Zero;
                    MainMenu.countRewardedAdsWatched = 0;
                    GameManager.Instance.isRewardedAdOnCountDown = false;
                   
                }

               
                currentTimeText.text = "Restart in :   " + string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);

                GameManager.Instance.TimeQuit = time;
            }

        }

    }
}
