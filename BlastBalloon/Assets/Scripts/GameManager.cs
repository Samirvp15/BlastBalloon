using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    //VARIABLES GLOBALES ENTRE ESCENAS
    public bool isOnline = false;
    public int countRewardedAdsWatched = 0;
    public int numberAdstoWatch = 3;

    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float seconsMaxDifc;
    public int maxPoints;

    public int Level = 0;

    public bool Level_Completed = false;

    public bool Levels_First_StarIcon = false;
    public bool Levels_Second_StarIcon = false;
    public bool Levels_Third_StarIcon = false;

    public bool Level1_Completed = false;
    //public bool Level2_Completed = false;
    //public bool Level3_Completed = false;
    //public bool Levels_Completed = false; 



    //LEVEL 1 VARIABLES




    //LEVEL 2 VARIABLES





    //LEVEL 3 VARIABLES




    /*public bool isRewardedAdOnCountDown = false;
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

    }*/


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
