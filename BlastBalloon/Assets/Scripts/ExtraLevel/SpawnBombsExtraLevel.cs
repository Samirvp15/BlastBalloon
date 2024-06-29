using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBombsExtraLevel : MonoBehaviour
{
    // Start is called before the first frame update

    public float minX = 0;
    public float maxX = 0;
 
    private Vector3 spawnPos = new Vector3();
    public GameObject[] BombBalloons;

    void Start()
    {
        InvokeRepeating("SpawnBalloon", 1.0f, 0.30f);
    }

   

    void SpawnBalloon()
    {
        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                // Posicion Aleatoria para spawn
                spawnPos = new Vector3(Random.Range(minX, maxX), 19.5f, 8.9f);

                // Elegir aleatoriamente entre los objetos
                int balloonIndex = Random.Range(0, BombBalloons.Length);


                // Instanciar el objeto seleccionado en la posición aleatoria
                GameObject objectToRespawn = BombBalloons[balloonIndex];


                Instantiate(objectToRespawn, spawnPos, Quaternion.identity);
            }

        }
    }
}
