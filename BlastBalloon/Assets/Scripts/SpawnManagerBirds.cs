using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerBirds : MonoBehaviour
{
    public GameObject[] birdsPrefab;
    private Vector3 spawnPos = new Vector3();
    public float minX = 0;
    public float maxX = 0;
    public float minZ = 0;
    public float maxZ = 0;
    public float minY = 0;
    public float maxY = 0;
    private float startDelay = 2.0f;
    private float repeatRate = 2.5f;
    public float minRepeatRate = 2.2f;
    public float maxRepeatRate = 2.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnBirds), startDelay, repeatRate);
    }

    // Update is called once per frame
    void SpawnBirds()
    {
        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                // Posicion Aleatoria para spawn
                spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

                repeatRate = Random.Range(minRepeatRate, maxRepeatRate);

                // Elegir aleatoriamente entre los dos objetos
                int indexBird = Random.Range(0, birdsPrefab.Length);

                // Instanciar el objeto seleccionado en la posición aleatoria
                Instantiate(birdsPrefab[indexBird], spawnPos, Quaternion.identity);
            }
            
        }
    }
}
