using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public GameObject SelectLevel;
    public GameObject SelectBalloon;
    public GameObject HowToPlay;
    public GameObject activeScreen;

    public GameObject StartButton;
    public GameObject SettingsButton;
    public GameObject ExitButton;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        activeScreen = null;

        DOTween.SetTweensCapacity(500, 50);
 
        // Iniciar la animación
        StartCoroutine(ScaleButton(StartButton));
        StartCoroutine(ScaleButton(SettingsButton));
        StartCoroutine(ScaleButton(ExitButton));
    }

    void SetActiveScreen(GameObject newActiveScreen)
    {
        // Desactivar el screen actual si hay uno activo
        if (activeScreen != null)
        {
            activeScreen.SetActive(false);
        }

        // Activar el nuevo screen
        newActiveScreen.SetActive(true);
        activeScreen = newActiveScreen;
    }

    public void HowToPlayScreen()
    {
        audioManager.PlaySFXButton();
        SetActiveScreen(HowToPlay);
    }

    public void SelectLevelTransition()
    {
        audioManager.PlaySFXButton();
        SetActiveScreen(SelectLevel);
    }

    public void CloseSelectLevelTransition()
    {
        audioManager.PlaySFXButton();
        SelectLevel.SetActive(false);
    }

    public void SelectBalloonExtraScene()
    {
        audioManager.PlaySFXButton();
        SetActiveScreen(SelectBalloon);
    }

    public void CloseSelectBalloonExtraScene()
    {
        audioManager.PlaySFXButton();
        SelectBalloon.SetActive(false);
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
