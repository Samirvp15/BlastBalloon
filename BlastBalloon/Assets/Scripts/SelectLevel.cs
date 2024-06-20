using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{

    Button Level2Button;
    Button Level3Button;

    public GameObject Level_1_First_StarIcon;
    public GameObject Level_1_Second_StarIcon;
    public GameObject Level_1_Third_StarIcon;

    public GameObject Level_2_First_StarIcon;
    public GameObject Level_2_Second_StarIcon;
    public GameObject Level_2_Third_StarIcon;

    public GameObject Level_3_First_StarIcon;
    public GameObject Level_3_Second_StarIcon;
    public GameObject Level_3_Third_StarIcon;

    public int Level1_Completed_Static = 0;
    public int Level2_Completed_Static = 0;
    public int Level3_Completed_Static = 0;

    public int Level1_First_StarIcon = 0;
    public int Level1_Second_StarIcon = 0;
    public int Level2_First_StarIcon = 0;
    public int Level2_Second_StarIcon = 0;
    public int Level3_First_StarIcon = 0;
    public int Level3_Second_StarIcon = 0;


    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Level2Button = GameObject.Find("ButtonL2").GetComponent<Button>();
        Level3Button = GameObject.Find("ButtonL3").GetComponent<Button>();

        //Level2Button.interactable = false;
        //Level3Button.interactable = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


        //LOAD LEVELS COMPLETED INFO
        Level1_Completed_Static = PlayerPrefs.GetInt("Level1_Completed", 0);
        Level2_Completed_Static = PlayerPrefs.GetInt("Level2_Completed", 0);
        Level3_Completed_Static = PlayerPrefs.GetInt("Level3_Completed", 0);

        Level1_First_StarIcon = PlayerPrefs.GetInt("Level1_First_StarIcon", 0);
        Level1_Second_StarIcon = PlayerPrefs.GetInt("Level1_Second_StarIcon", 0);
        Level2_First_StarIcon = PlayerPrefs.GetInt("Level2_First_StarIcon", 0);
        Level2_Second_StarIcon = PlayerPrefs.GetInt("Level2_Second_StarIcon", 0);
        Level3_First_StarIcon = PlayerPrefs.GetInt("Level3_First_StarIcon", 0);
        Level3_Second_StarIcon = PlayerPrefs.GetInt("Level3_Second_StarIcon", 0);

        //STATIC STARS COMPLETED AFTER LEVELS COMPLETED
        StaticStarsCompleted();

        GameManager.Instance.Level1_Completed = true;
        

        /*if (Level3_Completed_Static == 0)
        {
            if (Level2_Completed_Static == 1 && GameManager.Instance.Level == 3)
            {
                if (GameManager.Instance.Levels_Third_StarIcon)
                {
                    StartCoroutine(StarsCompleted(Level_3_First_StarIcon, Level_3_Second_StarIcon, Level_3_Third_StarIcon));
                    //GameManager.Instance.Levels_Completed = true;
                    //PERSIST LEVEL COMPLETED INFO  
                    //PlayerPrefs.SetInt("Level3_Completed", 1);
                }
                else if (GameManager.Instance.Levels_Second_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level3_Second_StarIcon", 1);
                    StartCoroutine(TwoStarsCompleted(Level_3_First_StarIcon, Level_3_Second_StarIcon));
                }
                else if (GameManager.Instance.Levels_First_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level3_First_StarIcon", 1);
                    StartCoroutine(OneStarCompleted(Level_3_First_StarIcon));
                }
            }
            else if (Level1_Completed_Static == 1 && GameManager.Instance.Level == 2)
            {
                if (GameManager.Instance.Levels_Third_StarIcon)
                {
                    StartCoroutine(StarsCompleted(Level_2_First_StarIcon, Level_2_Second_StarIcon, Level_2_Third_StarIcon));
                    Level3Button.interactable = true;
                   //GameManager.Instance.Level3_Completed = true;
                    //PERSIST LEVEL COMPLETED INFO 
                   // PlayerPrefs.SetInt("Level2_Completed", 1);
                }
                else if (GameManager.Instance.Levels_Second_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level2_Second_StarIcon", 1);
                    StartCoroutine(TwoStarsCompleted(Level_2_First_StarIcon, Level_2_Second_StarIcon));
                }
                else if (GameManager.Instance.Levels_First_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level2_First_StarIcon", 1);
                    StartCoroutine(OneStarCompleted(Level_2_First_StarIcon));
                }

            }
            else if (GameManager.Instance.Level1_Completed && Level1_Completed_Static == 0) //&& Level1_Completed_Static == 0  ----- GameManager.Instance.Level == 1
            {

                if (GameManager.Instance.Levels_Third_StarIcon)
                {
                    StartCoroutine(StarsCompleted(Level_1_First_StarIcon, Level_1_Second_StarIcon, Level_1_Third_StarIcon));
                    Level2Button.interactable = true;
                    //GameManager.Instance.Level2_Completed = true;
                    //PERSIST LEVEL COMPLETED INFO 
                    //PlayerPrefs.SetInt("Level1_Completed", 1);
                }
                else if (GameManager.Instance.Levels_Second_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level1_Second_StarIcon", 1);
                    StartCoroutine(TwoStarsCompleted(Level_1_First_StarIcon, Level_1_Second_StarIcon));
                }
                else if (GameManager.Instance.Levels_First_StarIcon)
                {
                    //PlayerPrefs.SetInt("Level1_First_StarIcon", 1);
                    StartCoroutine(OneStarCompleted(Level_1_First_StarIcon));
                }

            }

        }*/


        
        //PlayerPrefs.Save();


    }


    public void One_Static_Stars(GameObject Level_1_First_StarIcon)
    {
        RectTransform rectTransform_S1 = Level_1_First_StarIcon.GetComponent<RectTransform>();
        rectTransform_S1.localScale = new Vector3(0.8f, 0.8f, 0f);
    }

    public void Two_Static_Stars(GameObject Level_1_First_StarIcon, GameObject Level_1_Second_StarIcon)
    {
        One_Static_Stars(Level_1_First_StarIcon);

        RectTransform rectTransform_S2 = Level_1_Second_StarIcon.GetComponent<RectTransform>();
        rectTransform_S2.localScale = new Vector3(0.8f, 0.8f, 0f);

        
    }

    public void Three_Static_Stars(GameObject Level_1_First_StarIcon, GameObject Level_1_Second_StarIcon, GameObject Level_1_Third_StarIcon)
    {

        Two_Static_Stars(Level_1_First_StarIcon, Level_1_Second_StarIcon);

        RectTransform rectTransform_S3 = Level_1_Third_StarIcon.GetComponent<RectTransform>();
        rectTransform_S3.localScale = new Vector3(0.8f, 0.8f, 0f);

        
    }


    // Update is called once per frame
    void StaticStarsCompleted()
    {

        //LEVEL 1
        if (Level1_Completed_Static == 1)
        {
            Level2Button.interactable = true;
            Three_Static_Stars(Level_1_First_StarIcon, Level_1_Second_StarIcon, Level_1_Third_StarIcon);
        }
        else if (Level1_Second_StarIcon == 1)
        {
           Two_Static_Stars(Level_1_First_StarIcon, Level_1_Second_StarIcon);
        }else if (Level1_First_StarIcon == 1)
        {
            One_Static_Stars(Level_1_First_StarIcon);
        }

        //LEVEL 2
        if (Level2_Completed_Static == 1)
        {
            Level3Button.interactable = true;
            Three_Static_Stars(Level_2_First_StarIcon, Level_2_Second_StarIcon, Level_2_Third_StarIcon);
        }
        else if (Level2_Second_StarIcon == 1)
        {
            Two_Static_Stars(Level_2_First_StarIcon, Level_2_Second_StarIcon);
        }
        else if (Level2_First_StarIcon == 1)
        {
            One_Static_Stars(Level_2_First_StarIcon);
        }


        //LEVEL 3
        if (Level3_Completed_Static == 1)
        {
            Three_Static_Stars(Level_3_First_StarIcon, Level_3_Second_StarIcon, Level_3_Third_StarIcon);
        }
        else if (Level3_Second_StarIcon == 1)
        {
            Two_Static_Stars(Level_3_First_StarIcon, Level_3_Second_StarIcon);
        }
        else if (Level3_First_StarIcon == 1)
        {
            One_Static_Stars(Level_3_First_StarIcon);
        }

    }

    /*public IEnumerator OneStarCompleted(GameObject First_StarIcon)
    {
        yield return new WaitForSeconds(0.5f);

        First_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
        //audioManager.PlayStarCompletedSFX();
        yield return new WaitForSeconds(0.5f);

    }


    public IEnumerator TwoStarsCompleted(GameObject First_StarIcon, GameObject Second_StarIcon)
    {
        yield return new WaitForSeconds(0.5f);

        First_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
        //audioManager.PlayStarCompletedSFX();
        yield return new WaitForSeconds(0.5f);


        Second_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
       //audioManager.PlayStarCompletedSFX();
        yield return new WaitForSeconds(0.5f);

    }
    

    public IEnumerator StarsCompleted(GameObject First_StarIcon, GameObject Second_StarIcon, GameObject Third_StarIcon)
    {
        yield return new WaitForSeconds(0.5f);

        First_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
        //audioManager.PlayStarCompletedSFX();
        yield return new WaitForSeconds(0.5f);
        
        
        Second_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
        //audioManager.PlayStarCompletedSFX();
        yield return new WaitForSeconds(0.5f);
        
        
        Third_StarIcon.transform.DOScale(0.8f, 2.0f).SetEase(Ease.OutElastic);
        //audioManager.PlayTotalStarsCompletedSFX();
        yield return new WaitForSeconds(0.5f);

    }*/




    public void PlayGame() {
        SceneManager.LoadScene("GameScene");
        GameManager.Instance.countRewardedAdsWatched = 0;
    }

    public void Level_1()
    {
        GameManager.Instance.Level = 1;
        GameManager.Instance.maxPoints = 100;
        GameManager.Instance.minSpeed = 6.0f;
        GameManager.Instance.maxSpeed = 9.0f;
        GameManager.Instance.seconsMaxDifc = 120;
        PlayGame();

    }

    public void Level_2()
    {
        GameManager.Instance.Level = 2;
        GameManager.Instance.maxPoints = 250;
        GameManager.Instance.minSpeed = 7.0f;
        GameManager.Instance.maxSpeed = 10.0f;
        GameManager.Instance.seconsMaxDifc = 150;
        PlayGame();
    }

    public void Level_3()
    {
         //300 point
         GameManager.Instance.Level = 3;
         GameManager.Instance.maxPoints = 300;
         GameManager.Instance.minSpeed = 7.5f;
         GameManager.Instance.maxSpeed = 11.0f;
         GameManager.Instance.seconsMaxDifc = 180;
        PlayGame();
    }


}

