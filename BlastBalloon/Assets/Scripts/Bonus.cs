using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public static TextMeshProUGUI livesText;
    public static int livesCount;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        livesCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "" + livesCount;
    }

    public static void Addlife()
    {
        livesCount++;
        livesText.transform.DOScale(2.5f, 2.0f).SetEase(Ease.OutElastic);
    }

}
