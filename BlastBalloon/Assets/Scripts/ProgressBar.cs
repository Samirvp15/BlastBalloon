using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.Progress;
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
    public int maxPoints;



    void Start()
    {
        progressBar = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = (float)ScoreManager.scoreCount / maxPoints;
        //1 STAR 65 POINTS
        //2 STAR 150 POINTS
        //3 STAR 250 POINTS

        if (progressBar.fillAmount >= 1f)
        {
            ThirdStar = true;
            StarIcon3.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
        }
        else if (progressBar.fillAmount >= 0.6f)
        {
            SecondtStar = true;
            StarIcon2.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
        }
        else if (progressBar.fillAmount >= 0.26f)
        {
            FirstStar = true;
            StarIcon1.transform.DOScale(1f, 0.8f).SetEase(Ease.OutBounce);
        }
        


    }

}
