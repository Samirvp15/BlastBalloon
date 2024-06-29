using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class ProgressBar : MonoBehaviour
{
    public GameObject StarIcon1;
    public GameObject StarIcon2;
    public GameObject StarIcon3;
    public static bool FirstStar = false;
    public static bool SecondtStar = false;
    public static bool ThirdStar = false;
    Image progressBar;
    bool starCompletedSoundPlayed = false;

    AudioManager audioManager;



    void Start()
    {
        //RESTAURAR VALORES
        FirstStar = false;
        SecondtStar = false;
        ThirdStar = false;
        starCompletedSoundPlayed = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        progressBar = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = (float)ScoreManager.scoreCount / GameManager.Instance.maxPoints;

        if (progressBar.fillAmount >= 1f && !ThirdStar)
        {
            ThirdStar = true;
            GameManager.Instance.Level_Completed = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_Completed", 1);
            }
            else if (GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_Completed", 1);
            }
            else if (GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_Completed", 1);
            }
            audioManager.PlayStar2CompletedSFX();
            StarIcon3.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
            starCompletedSoundPlayed = true; 
        }
        else if (progressBar.fillAmount >= 0.6f && !SecondtStar)
        {
            SecondtStar = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_Second_StarIcon", 1);
            }
            else if (GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_Second_StarIcon", 1);
            }
            else if (GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_Second_StarIcon", 1);
            }
            audioManager.PlayStar2CompletedSFX();
            StarIcon2.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
            starCompletedSoundPlayed = true; 
        }
        else if (progressBar.fillAmount >= 0.26f && !FirstStar)
        {
            FirstStar = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_First_StarIcon", 1);
            }
            else if (GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_First_StarIcon", 1);
            }
            else if (GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_First_StarIcon", 1);
            }
            audioManager.PlayStar2CompletedSFX();
            StarIcon1.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
            starCompletedSoundPlayed = true; // Marcar que el sonido se ha reproducido
        }

        // Restablecer starCompletedSoundPlayed a false en cada rama del else if
        if (starCompletedSoundPlayed)
        {
            starCompletedSoundPlayed = false;
        }


    }

    
}
