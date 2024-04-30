using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    public List<GameObject> ballonsIcons = new List<GameObject>();


    public IEnumerator StarsCompleted()
    {

        if (ProgressBar.FirstStar == true)
        {
            First_StarIcon.transform.DOScale(0.8f, 1.5f).SetEase(Ease.OutBounce);
             yield return new WaitForSeconds(0.8f);
        }
        if (ProgressBar.SecondtStar == true)
        {
            Second_StarIcon.transform.DOScale(0.8f, 1.5f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.8f);
        }
        if (ProgressBar.ThirdStar == true)
        {
            Third_StarIcon.transform.DOScale(0.8f, 1.5f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.8f);
        }


    }


    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1f, fadeTime);
        StartCoroutine(BallonIconsAnimation());
    }
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutElastic);
        canvasGroup.DOFade(0f, fadeTime);
    }
    void Start()
    {
        PanelFadeIn();
        StartCoroutine(StarsCompleted());
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
