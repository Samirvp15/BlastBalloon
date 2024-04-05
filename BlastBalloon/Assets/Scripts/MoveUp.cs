using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveUp : MonoBehaviour
{
    public static float speed;
    public float minSpeed;
    public float maxSpeed;
    public static float seconsMaxDifc = 80;
    private float topBound = 20;
    public float spinSpeed;
    private float horizontalAmplitude = 1.5f;  // Amplitud del movimiento horizontal
    private float horizontalPeriod = 3.5f;     // Período del movimiento horizontal
    private float elapsedTime = 10.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up, 180f);
    }
    public static float getSeconsMaxDifc()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / seconsMaxDifc);
    }
    // Update is called once per frame
    void Update()
    {
  
        speed = Mathf.Lerp(minSpeed, maxSpeed, getSeconsMaxDifc());
        
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Movimiento horizontal periódico
        float horizontalOffset = Mathf.Sin(elapsedTime * 2 * Mathf.PI / horizontalPeriod) * horizontalAmplitude;
        transform.Translate(Vector3.right * horizontalOffset * Time.deltaTime);


        //Aumentar la amplitud
        horizontalAmplitude = Mathf.Lerp(1.5f, 4.0f, getSeconsMaxDifc());
        horizontalPeriod = Mathf.Lerp(3.5f, 2.0f, getSeconsMaxDifc());
        // Incrementar el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime); 


        if (transform.position.y > topBound && (gameObject.CompareTag("Balloon") || gameObject.CompareTag("BombBalloon") || gameObject.CompareTag("Bomb")))
        {
            Destroy(gameObject);
        }

    }




}
