using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    //CLASE PARA LAS GLOBOS BOMBA DE LA ESCENA EXTRA
    private float bottomBound = -12.0f;
    private float seconsMaxDifcExtraLevel = 120.0f;
    private float speedBombs;
    private float minSpeedBombs = 6.5f;
    private float maxSpeedBombs = 11.0f;
    public bool collisionTouch = false;

    private BombAnimation bombAnimation;

    AudioManager audioManager;

    ParticleSystem explosionParticle;
    ParticleSystem ConfettiParticles;
    private Renderer renderObject;
    private Renderer particleRenderer;
    private Collider colliderObject;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        transform.Rotate(Vector3.up, 180f);

       
        explosionParticle = transform.Find("BoomParticles").GetComponent<ParticleSystem>();
        ConfettiParticles = transform.Find("ConfettiParticles").GetComponent<ParticleSystem>();
        particleRenderer = explosionParticle.GetComponent<Renderer>();
        renderObject = GetComponent<Renderer>();
        colliderObject = GetComponent<Collider>();

        particleRenderer.material = renderObject.material;
        // Restablecer el estado del globo
        renderObject.enabled = true;
        colliderObject.enabled = true;
    }
    public float GetSeconsMaxDifcExtraLevel()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / seconsMaxDifcExtraLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionTouch) {

            if (UIExtraLevel.livesCount < 1)
            {
                MainMenu.gamePaused = true;
                bombAnimation = GetComponent<BombAnimation>();
                // Play bomb animation
                bombAnimation.PlayBombAnimation();
            }
            else if (collisionTouch)
            {
                // Desactivar el renderer y el collider
                renderObject.enabled = false;
                transform.Find("Bomb").gameObject.SetActive(false);
                colliderObject.enabled = false;
            }


            audioManager.PlaySFX_PopBallon();
            explosionParticle.Play();
            ConfettiParticles.Play();

            collisionTouch = false;
        }


        speedBombs = Mathf.Lerp(minSpeedBombs, maxSpeedBombs, GetSeconsMaxDifcExtraLevel());

        transform.Translate(speedBombs * Time.deltaTime * Vector3.down);
        //transform.Rotate(spinSpeedBombs * Time.deltaTime * Vector3.up);
        
        //ELIMINAR Bombs AL PASAR EL LIMITE
        if (transform.position.y < bottomBound && gameObject.CompareTag("BombBalloon"))
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            collisionTouch = true;
        }
    }
}
