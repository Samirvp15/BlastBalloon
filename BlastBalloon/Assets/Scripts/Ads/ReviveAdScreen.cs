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
    Button rewardedButtonAd;

    float currentTime;
    float startMinutes = 15f;
    int numberAdstoWatch = 3;
    public TextMeshProUGUI currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<MainMenu>();
        CircularBarFilled = GameObject.Find("CircularBarFilled").GetComponent<Image>();
        rewardedButtonAd = GameObject.Find("RewardedAdButton").GetComponent<Button>();
        currentTimeText = GameObject.Find("TimerCountDownText").GetComponent<TextMeshProUGUI>();

        maxTimer = countdownTimerCircularBar;

        currentTime = startMinutes * 60;

    }

    // Update is called once per frame
    void Update()
    {

        if (MainMenu.gamePaused == true)
        {
            Time.timeScale = 1;

            if (MainMenu.countRewardedAdsWatched < numberAdstoWatch)
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
                    Canvas.CloseReviveAdScreen();
                    MainMenu.gameOver = true;
                    Canvas.GameOver();
                }
            }
            else
            {
                currentTimeText.gameObject.SetActive(true);
                rewardedButtonAd.interactable = false;
                CircularBarFilled.gameObject.SetActive(false);
 
                currentTime -= Time.deltaTime;

                if (currentTime < 0)
                {
                    //RESET VALORES
                    currentTimeText.gameObject.SetActive(false);
                    rewardedButtonAd.interactable = true;
                    CircularBarFilled.gameObject.SetActive(true);
                    currentTime = startMinutes * 60;
                    MainMenu.countRewardedAdsWatched = 0;
                }

                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                currentTimeText.text = "Restart in :   " + string.Format("{0:00}:{1:00}:{2:00}", time.Hours , time.Minutes, time.Seconds);
            }

        }

    }
}
