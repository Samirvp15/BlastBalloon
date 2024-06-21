using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManagerBalloons : MonoBehaviour
{

    public List<GameObject> balloonsList = new List<GameObject>();
    public GameObject[] balloonPrefab;
    public GameObject[] bombBalloonPrefab;
    public GameObject[] bombBalloon;

    public GameObject balloonBonusLife;
    public GameObject balloonBonusExtraPoints;

    private Vector3 spawnPos = new Vector3();
    public float minX = 0;
    public float maxX = 0;
    public float minZ = 0;
    public float maxZ = 0;
    private float startDelay = 1;
    public float repeatRate = 0.30f;

    // Variables para controlar la cantidad de objetos instanciados recientemente
    private int maxSameBombBalloons = 2;  // Máximo de globos iguales consecutivos
    private int sameBombBalloonCount = 0; // Contador de globos iguales instanciados

    private int maxBalloons = 6;  // Máximo de globos iguales consecutivos
    private int balloonCount = 0; // Contador de globos iguales instanciados


    // Diccionario para almacenar el contador de cada tipo de globo
    public static Dictionary<string, int> poppedBalloonsCount = new Dictionary<string, int>();

    void InitializePoppedBalloonCounts()
    {
        // Inicializar el contador para cada tipo de globo con valor cero
        poppedBalloonsCount.Clear();  // Limpiar el diccionario por si acaso
        poppedBalloonsCount.Add("Balloon.001(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.002(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.003(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.004(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.005(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.006(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.007(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.008(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.009(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.010(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.011(Clone)", 0);
        poppedBalloonsCount.Add("Balloon.012(Clone)", 0);
        poppedBalloonsCount.Add("BalloonAlien.013(Clone)", 0);
        poppedBalloonsCount.Add("BalloonCat.014(Clone)", 0);
        poppedBalloonsCount.Add("BalloonCat.015(Clone)", 0);
        // Agregar más tipos de globos según sea necesario
    }
    public void ResetValues()
    {
        balloonsList.Clear();
        GameManager.Instance.speed = 0;
        repeatRate = 0.30f;

    }

    void Start()
    {
        ResetValues();
        InitializePoppedBalloonCounts();

        balloonsList.AddRange(balloonPrefab);

        InvokeRepeating("SpawnBalloon", startDelay, repeatRate);

    }

    // Update is called once per frame
    void Update()
    {
        // Verificar la velocidad continuamente con solo 2 decimales
        float roundedspeed = Mathf.Round(GameManager.Instance.speed * 100f) / 100f;

        //LEVEL 1
        if (GameManager.Instance.Level == 1)
        {
            if (roundedspeed > 6.5f && !balloonsList.Contains(bombBalloon[0]))
            {
                // SPAWN BONUS LIFE
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusLife, spawnPos, Quaternion.identity);
                // SPAWN BONUS EXTRA POINTS
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusExtraPoints, spawnPos, Quaternion.identity);
                //SPAWN BOMBS
                repeatRate = 0.10f;
                balloonsList.AddRange(bombBalloon);
            }
        
            // Verifica si la velocidad supera el umbral y si los prefabs de bomba no están ya en la lista
            else if (roundedspeed > 7.5f && !balloonsList.Contains(bombBalloonPrefab[0]))
            {
                // SPAWN BONUS LIFE
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusLife, spawnPos, Quaternion.identity);
                // SPAWN BONUS EXTRA POINTS
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusExtraPoints, spawnPos, Quaternion.identity);
                //SPAWN BOMB BALLOONS
                repeatRate = 0.05f;
                balloonsList.AddRange(bombBalloonPrefab);
            }
        }

        //LEVEL 2
        if (GameManager.Instance.Level == 2)
        {
            if (roundedspeed > 7.5f && !balloonsList.Contains(bombBalloon[0]))
            {
                // SPAWN BONUS LIFE
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusLife, spawnPos, Quaternion.identity);
                //SPAWN BOMBS
                repeatRate = 0.10f;
                balloonsList.AddRange(bombBalloon);
            }

            // Verifica si la velocidad supera el umbral y si los prefabs de bomba no están ya en la lista
            else if (roundedspeed > 8.0f && !balloonsList.Contains(bombBalloonPrefab[0]))
            {
                //SPAWN BOMB BALLOONS
                repeatRate = 0.05f;
                balloonsList.AddRange(bombBalloonPrefab);
            }
        }

        //LEVEL 3
        if (GameManager.Instance.Level == 3)
        {
            if (roundedspeed > 7.5f && !balloonsList.Contains(bombBalloon[0]))
            {
                // SPAWN BONUS LIFE
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));
                Instantiate(balloonBonusLife, spawnPos, Quaternion.identity);
                //SPAWN BOMBS
                repeatRate = 0.10f;
                balloonsList.AddRange(bombBalloon);
            }

            // Verifica si la velocidad supera el umbral y si los prefabs de bomba no están ya en la lista
            else if (roundedspeed > 8.5f && !balloonsList.Contains(bombBalloonPrefab[0]))
            {
                //SPAWN BOMB BALLOONS
                repeatRate = 0.05f;
                balloonsList.AddRange(bombBalloonPrefab);
            }
        }



    }

    void SpawnBalloon()
    {
        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                // Posicion Aleatoria para spawn
                spawnPos = new Vector3(Random.Range(minX, maxX), -5, Random.Range(minZ, maxZ));

                // Elegir aleatoriamente entre los objetos
                int balloonIndex = Random.Range(0, balloonsList.Count);


                // Instanciar el objeto seleccionado en la posición aleatoria
                GameObject objectToRespawn = balloonsList[balloonIndex];


                // Verificar si estamos instanciando un globo del mismo tipo consecutivamente
                if (objectToRespawn.CompareTag("BombBalloon") && sameBombBalloonCount < maxSameBombBalloons)
                {
                    sameBombBalloonCount++; // Incrementar contador
                }
                else if (objectToRespawn.CompareTag("Bomb") && sameBombBalloonCount < maxSameBombBalloons)
                {
                    sameBombBalloonCount++; // Incrementar contador
                }
                else if (objectToRespawn.CompareTag("Balloon") && balloonCount < maxBalloons)
                {
                    balloonCount++;
                    sameBombBalloonCount = 0;  // Reiniciar contador
                }
                else
                {
                    objectToRespawn = bombBalloon[Random.Range(0, bombBalloon.Length)];
                    sameBombBalloonCount = 0;
                    balloonCount = 0;  // Reiniciar contador
                }


                Instantiate(objectToRespawn, spawnPos, Quaternion.identity);
            }
            
        }
    }


    // Método para reventar un globo de un tipo específico
    public static void PopBalloon(string balloonType)
    {

        if (poppedBalloonsCount.ContainsKey(balloonType))
        {
            poppedBalloonsCount[balloonType] += 1;  //Incrementar el contador del tipo de globo
            ScoreManager.PoppedBallonCountText(balloonType, poppedBalloonsCount[balloonType]);
        }
        else
        {
            Debug.LogWarning("Balloon type not found: " + balloonType);
        }

    }

}
