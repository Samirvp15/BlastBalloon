using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer renderObject;
    public int speed = 10;
    private float rightBound = 90;
    public Material[] colorMaterial;
    void Start()
    {
        
        renderObject = GetComponentInChildren<Renderer>();

        int colorIndex = Random.Range(0, colorMaterial.Length);

        if (renderObject != null && renderObject.material != null)
        {
            // Verificar si el objeto tiene un renderer y un material renderObject.material != null
            if (gameObject.CompareTag("Bird"))
            {
                // Asignar el color aleatorio al material del objeto
                renderObject.material = colorMaterial[colorIndex];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (transform.position.x > rightBound && gameObject.CompareTag("Bird"))
        {
            Destroy(gameObject);
        }

    }
}
