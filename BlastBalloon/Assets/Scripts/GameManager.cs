using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    //VARIABLES GLOBALES ENTRE ESCENAS
    public int numberAdstoWatch = 1;
    public bool isRewardedAdOnCountDown = false;
    public bool xdd = true;
    public TimeSpan TimeQuit;
    public TimeSpan TimePassed;
    public TimeSpan timeSpan;
    public int ayy = 0;
    public DateTime StartAPIDateTime;
    public DateTime EndAPIDateTime;
    public DateTime CurrentAPIDateTime;

    void Start()
    {
        string dateQuitString = PlayerPrefs.GetString("dateQuit", "");
        if (!dateQuitString.Equals(""))
        {
            DateTime dateQuit = DateTime.Parse(dateQuitString);
            DateTime dateNow = DateTime.Now;

            if (dateNow > dateQuit)
            {
                timeSpan = dateNow - dateQuit;
                //Debug.Log("QUIT FOR: " + timeSpan.TotalSeconds + " seconds.");
            }

            PlayerPrefs.SetString("dateQuit","");
        }
    }
     
    private void OnApplicationQuit()
    {
        DateTime dateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit", dateQuit.ToString());
       //Debug.Log("QUIT AT: "+dateQuit.ToString());
    }


    public bool FirstCountDownTimer = false;


    public TimeSpan TimeAPIElapsed()
    {
        return CurrentAPIDateTime - StartAPIDateTime;
    }


    public TimeSpan TimeAPIElapsedSecond()
    {
        return CurrentAPIDateTime - EndAPIDateTime;
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
