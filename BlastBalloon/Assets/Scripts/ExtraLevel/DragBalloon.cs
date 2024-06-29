using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBalloon : MonoBehaviour
{
    private Vector3 offset;
    private float zCoordinate;

    private Renderer renderObject;
    private Collider colliderobject;
    public Material[] colorMaterial;

    ParticleSystem explosionParticle;
    ParticleSystem ConfettiParticles;
    private Renderer particleRenderer;
    private bool isDragging = false;


    void Start()
    {
        
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        transform.Rotate(Vector3.up, 180f);

        colliderobject = GetComponent<Collider>();

        explosionParticle = transform.Find("BoomParticles").GetComponent<ParticleSystem>();
        ConfettiParticles = transform.Find("ConfettiParticles").GetComponent<ParticleSystem>();
        particleRenderer = explosionParticle.GetComponent<Renderer>();

        //ASIGANR COLORES AL GOLOBO
        renderObject = GetComponent<Renderer>();
        int colorIndex = Random.Range(0, colorMaterial.Length);
        //EXCEPTO GOLOBOS ESPECIALES
        if (gameObject.layer != 6)
        {
            renderObject.material = colorMaterial[colorIndex];
            particleRenderer.material = renderObject.material;
        }

    }


    void OnTriggerEnter(Collider other)
    {
       
        UIExtraLevel.livesCount--;
        explosionParticle.Play();
        ConfettiParticles.Play();


    }

    void Update()
    {
        if (UIExtraLevel.livesCount <= 0)
        {
            colliderobject.enabled = false;
            renderObject.enabled = false;
        }
        else
        {
            colliderobject.enabled = true;
            renderObject.enabled = true;
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = GetTouchWorldPosition(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchingObject(touch.position))
                    {
                        isDragging = true;
                        offset = transform.position - touchPos;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = touchPos + offset;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }

    private Vector3 GetTouchWorldPosition(Vector3 screenPosition)
    {
        screenPosition.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    private bool IsTouchingObject(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
