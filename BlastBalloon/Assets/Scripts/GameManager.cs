using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    //VARIABLES GLOBALES ENTRE ESCENAS
    public bool isOnline = false;
    public int numberAdstoWatch = 3;
    public bool isRewardedAdOnCountDown = false;
    public bool FirstCountDownTimer = false;
    public bool NextCountDownTimer = true;
    public TimeSpan TimeQuit;

    public TimeSpan TimePassed;

    public DateTime EndAPIDateTime;
    public DateTime CurrentAPIDateTime;


    private void OnApplicationQuit()
    {
        DateTime dateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit", dateQuit.ToString());
        
    }


    public TimeSpan TimeAPIElapsedSecond()
    {
        if (CurrentAPIDateTime > EndAPIDateTime)
        {
            return CurrentAPIDateTime - EndAPIDateTime;
        }
        else
        {
            return EndAPIDateTime - CurrentAPIDateTime;
        }
 
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
