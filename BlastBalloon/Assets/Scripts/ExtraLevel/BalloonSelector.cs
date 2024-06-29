using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonSelector : MonoBehaviour
{
    public Button[] balloonButtons;
    public GameObject[] balloons;

    private void Start()
    {
        for (int i = 0; i < balloonButtons.Length; i++)
        {
            int index = i; // Necesario para evitar problemas con closures en el loop
            balloonButtons[i].onClick.AddListener(() => SelectBalloon(index));
        }
    }

    private void SelectBalloon(int index)
    {
        GameObject selectedBalloon = balloons[index];
        // Aquí puedes almacenar el globo seleccionado en una variable global o en PlayerPrefs si necesitas persistirlo entre escenas
        GameManager.Instance.balloonExtraLevel = selectedBalloon;
        GameManager.Instance.Level = 4;
        SceneManager.LoadScene("ExtraGameScene");
    }
}
