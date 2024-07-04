using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnBalloonExtraLevel : MonoBehaviour
{
    public float minX = 0;
    public float maxX = 0;
    public float minY = 0;
    public float maxY = 0;

    private Vector3 spawnPos = new Vector3();
    public GameObject PointExtraLevel;
    // Start is called before the first frame update
    void Start()
    {

        Instantiate(GameManager.Instance.balloonExtraLevel, gameObject.transform.position, Quaternion.identity);

        InvokeRepeating(nameof(SpawnPoints), 3.0f, 2.5f);

    }



    void SpawnPoints()
    {
        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                // Posicion Aleatoria para spawn
                spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 8.9f);

                // Instanciar y almacenar una referencia al objeto instanciado
                GameObject spawnedPoint = Instantiate(PointExtraLevel, spawnPos, Quaternion.identity);
                // Destruir el objeto después de 9 segundos
                Destroy(spawnedPoint, 9.0f);

            }
        }
    }

}
