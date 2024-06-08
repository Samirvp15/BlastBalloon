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
        DOTween.SetTweensCapacity(500, 50);
 
        // Iniciar la animación
        StartCoroutine(ScaleButton(StartButton));
        StartCoroutine(ScaleButton(SettingsButton));
        StartCoroutine(ScaleButton(ExitButton));
    }


    private IEnumerator ScaleButton(GameObject button)
    {
        while (true)
        {
            // Escalar a 0.90
            yield return button.transform.DOScale(0.90f, 2.0f).SetEase(Ease.InOutSine).WaitForCompletion();
            // Escalar a 1.0 con retraso
            yield return new WaitForSeconds(0.8f);
            yield return button.transform.DOScale(1.0f, 2.0f).SetEase(Ease.InOutSine).WaitForCompletion();
        }
    }

   
}
