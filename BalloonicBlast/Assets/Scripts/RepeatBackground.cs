using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [Range(-1f, 1f)]
    public float scrollSpeed = 0.5f;
    private float offSet;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame

    void Update()
    {
        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                offSet += (Time.deltaTime * scrollSpeed) / 10f;
                mat.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
            }
        }
                

    }

}
