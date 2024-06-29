using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public static TextMeshProUGUI livesText;
    public static int livesCount;


    public static Image CircularBarPoints;
    Image IconExtraPoints;
    public static float countdownTimerCircularBar = 10.0f;
    public static float maxTimer = 0.0f;
    public static bool onTimer = false;


    // Start is called before the first frame update
    void Start()
    {
        IconExtraPoints = GameObject.Find("IconExtraPoints").GetComponent<Image>();
        CircularBarPoints = GameObject.Find("CircularBarPoints").GetComponent<Image>();
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        livesCount = 2;
        maxTimer = countdownTimerCircularBar;

        onTimer = false;
        IconExtraPoints.gameObject.SetActive(false);
        CircularBarPoints.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "" + livesCount;

        if (onTimer == true)
        {
            //START COUNTDOWN CIRCULAR BAR
            IconExtraPoints.gameObject.SetActive(true);
            CircularBarPoints.gameObject.SetActive(true);
            BonusExtraPointsTimer();
        }
        else
        {
            IconExtraPoints.gameObject.SetActive(false);
            CircularBarPoints.gameObject.SetActive(false);
            countdownTimerCircularBar = maxTimer;
        }
    }

    public static void BonusExtraPointsTimer()
    {
        //CountDown CircleBar Progress
        if (countdownTimerCircularBar >= 0)
        {
            countdownTimerCircularBar -= Time.deltaTime;
            CircularBarPoints.fillAmount = countdownTimerCircularBar / maxTimer;
        }
        else
        {
            onTimer = false;
        }

    }
    
}
