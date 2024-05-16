using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    //VARIABLES GLOBALES ENTRE ESCENAS
    public int numberAdstoWatch = 3;
    public bool isRewardedAdOnCountDown = false;
    public bool FirstCountDownTimer = false;
    public bool xdd = true;
    public TimeSpan TimeQuit;

    public TimeSpan TimePassed;



    //public int ayy = 0;
    public DateTime StartAPIDateTime;
    public DateTime EndAPIDateTime;
    public DateTime CurrentAPIDateTime;

    void Start()
    {
        
        Debug.Log("numero de counts AD: " + PlayerPrefs.GetInt("countRewardedAdsWatched", 0));
        /*string dateQuitString = PlayerPrefs.GetString("dateQuit", "");
        if (!dateQuitString.Equals(""))
        {
            DateTime dateQuit = DateTime.Parse(dateQuitString);
            DateTime dateNow = DateTime.Now;

            if (dateNow > dateQuit)
            {
                TimePassed = dateNow - dateQuit;
                //Debug.Log("QUIT FOR: " + TimePassed.TotalSeconds + " seconds.");
            }

            PlayerPrefs.SetString("dateQuit","");
        }*/
    }
     
    private void OnApplicationQuit()
    {
        DateTime dateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit", dateQuit.ToString());
        Debug.Log("QUIT AT: "+dateQuit.ToString());
    }


    public TimeSpan TimeAPIElapsed()
    {
        return CurrentAPIDateTime - StartAPIDateTime;
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
