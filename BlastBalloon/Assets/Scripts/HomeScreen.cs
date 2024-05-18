using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{

    public GameObject StartButton;
    public GameObject SettingsButton;
    public GameObject ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        OnScale();
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void OnScale()
    {
        StartButton.transform.DOScale(0.90f, 2.0f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                StartButton.transform.DOScale(1.0f, 2.0f)
                    .SetEase(Ease.InOutSine)
                    .SetDelay(0.8f)
                    .OnComplete(OnScale);
            });
        SettingsButton.transform.DOScale(0.90f, 2.0f)
           .SetEase(Ease.InOutSine)
           .OnComplete(() =>
           {
               SettingsButton.transform.DOScale(1.0f, 2.0f)
                   .SetEase(Ease.InOutSine)
                   .SetDelay(0.8f)
                   .OnComplete(OnScale);
           });
        ExitButton.transform.DOScale(0.90f, 2.0f)
           .SetEase(Ease.InOutSine)
           .OnComplete(() =>
           {
               ExitButton.transform.DOScale(1.0f, 2.0f)
                   .SetEase(Ease.InOutSine)
                   .SetDelay(0.8f)
                   .OnComplete(OnScale);
           });
    }
}
