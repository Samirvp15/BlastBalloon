using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public float fadeTime = 1f;
    public GameObject First_StarIcon;
    public GameObject Second_StarIcon;
    public GameObject Third_StarIcon;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public RectTransform StarsrectTransform;
    public List<GameObject> ballonsIcons = new List<GameObject>();

    AudioManager audioManager;
    public GameObject LevelCompleted;



    public IEnumerator StarsCompleted()
    {
        yield return new WaitForSeconds(0.7f);

        if (ProgressBar.FirstStar == true)
        {
            GameManager.Instance.Levels_First_StarIcon = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_First_StarIcon", 1);
            }
            else if (GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_First_StarIcon", 1);
            }
            else if(GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_First_StarIcon", 1);
            }

            First_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
            audioManager.PlayStarCompletedSFX();
            yield return new WaitForSeconds(0.8f);  
        }
        if (ProgressBar.SecondtStar == true)
        {
            GameManager.Instance.Levels_Second_StarIcon = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_Second_StarIcon", 1);
            }
            else if(GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_Second_StarIcon", 1);
            }
            else if(GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_Second_StarIcon", 1);
            }
            Second_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
            audioManager.PlayStarCompletedSFX();
            yield return new WaitForSeconds(0.8f);  
        }
        if (ProgressBar.ThirdStar == true)
        {
            
            GameManager.Instance.Levels_Third_StarIcon = true;
            GameManager.Instance.Level_Completed = true;
            // PESISTENT INFO LEVEL STAR COMPLETED FOR SELECT LEVEL
            if (GameManager.Instance.Level == 3)
            {
                PlayerPrefs.SetInt("Level3_Completed", 1);
            }
            else if(GameManager.Instance.Level == 2)
            {
                PlayerPrefs.SetInt("Level2_Completed", 1);
            }
            else if(GameManager.Instance.Level == 1)
            {
                PlayerPrefs.SetInt("Level1_Completed", 1);
            }
            Third_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
            audioManager.PlayStarCompletedSFX();
            yield return new WaitForSeconds(0.8f);
            //STARS GROUP SLIDES DOWN
            StarsDown();
            yield return new WaitForSeconds(0.6f);
            //APEARS LEVEL COMPLETED
            LevelCompleted.transform.DOScale(1.0f, 2.50f).SetEase(Ease.OutElastic);
            audioManager.PlayTotalStarsCompletedSFX();
            yield return new WaitForSeconds(0.8f);
        }

        PlayerPrefs.Save();
    }

    public void StarsDown()
    {
        StarsrectTransform.DOAnchorPos(new Vector2(-3.3617e-05f, 224f), 0.5f , false).SetEase(Ease.OutElastic);
    }

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1f, fadeTime);
        StartCoroutine(BallonIconsAnimation());
        StartCoroutine(StarsCompleted());
    }

    void Start()
    {
        //RESET LEVEL STARS
        if  (GameManager.Instance.Level_Completed)
        {
            GameManager.Instance.Levels_Third_StarIcon = false;
            GameManager.Instance.Levels_Second_StarIcon = false;
            GameManager.Instance.Levels_First_StarIcon = false;
            GameManager.Instance.Level_Completed = false;
        }

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        PanelFadeIn();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BallonIconsAnimation()
    {
        foreach (var item in ballonsIcons)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in ballonsIcons)
        {
            item.transform.DOScale(1f,fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.12f);

        }

    }
}
