using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem ConfettiParticles;
    private Renderer renderObject;
    public Material[] colorMaterial;
    public Material[] colorMaterialBomb;
    private Collider colliderObject;
    private Renderer particleRenderer;
    public bool isTouched = false;

    AudioManager audioManager;

    private BombAnimation bombAnimation;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //COMPONENTES UNICOS PARA CADA INSTANCIA DE OBJETO
        explosionParticle = transform.Find("BoomParticles").GetComponent<ParticleSystem>();
        ConfettiParticles = transform.Find("ConfettiParticles").GetComponent<ParticleSystem>();
        renderObject = GetComponent<Renderer>();
        colliderObject = GetComponent<Collider>();
        
        // Obtener el componente Renderer del sistema de partículas
        particleRenderer = explosionParticle.GetComponent<Renderer>();

        //ELEGIR ALEATORIAMENTE COLOR
        int colorIndex = Random.Range(0, colorMaterial.Length);
        int colorIndexBomb = Random.Range(0, colorMaterialBomb.Length);



        if (renderObject != null && renderObject.material != null)
        {
            // Verificar si el objeto tiene un renderer y un material renderObject.material != null
            if ((gameObject.CompareTag("Balloon") || gameObject.CompareTag("Bomb") ||
                gameObject.CompareTag("BonusLife") || gameObject.CompareTag("BonusExtraPoints")) && gameObject.layer != 6)
            {
                // Asignar el color aleatorio al material del objeto
                renderObject.material = colorMaterial[colorIndex];
                // ASIGNAR EL MISMO COLOR A LAS PARTICULAS
                particleRenderer.material = colorMaterial[colorIndex];
            }
            else if (gameObject.CompareTag("BombBalloon"))
            {
                // Asignar el color aleatorio al material del objeto bomb
                renderObject.material = colorMaterialBomb[colorIndexBomb];
                particleRenderer.material = colorMaterialBomb[colorIndexBomb];
            }
        }

        ResetBalloon();
    }

    // Update is called once per frame
    void Update()
    {

        if (MainMenu.gameOver == false)
        {
            if (MainMenu.gamePaused == false)
            {
                BalloonsGameplay();
            }

        }


    }
    // Método para restablecer el estado del globo
    public void ResetBalloon()
    {
        // Restablecer el estado del globo
        renderObject.enabled = true;
        colliderObject.enabled = true;
        isTouched = false;
    }

    public IEnumerator AddlifeAnimation()
    {
        Bonus.livesCount++;

        Bonus.livesText.transform.DOScale(1.8f, 1.5f).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(0.1f);
        Bonus.livesText.transform.DOScale(1.0f, 1.5f).SetEase(Ease.InElastic);
    }

    public IEnumerator RemovelifeAnimation()
    {
        Bonus.livesCount--;

        if (Bonus.livesCount != 0)
        {
            Bonus.livesText.transform.DOScale(1.8f, 1.5f).SetEase(Ease.OutElastic);
            yield return new WaitForSeconds(0.1f);
            Bonus.livesText.transform.DOScale(1.0f, 1.5f).SetEase(Ease.InElastic);
        }
        
    }



    public void BalloonsGameplay()
    {
        // Verifica si se ha tocado la pantalla (solo funciona en dispositivos táctiles)
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {

            // Lanza un rayo desde la posición del toque en pantalla
            Ray rayo = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Realiza el raycast para detectar la colisión con los colliders de los globos
            if (Physics.Raycast(rayo, out hit))
            {
                if (hit.collider.gameObject == gameObject && !isTouched)
                {
                    // Marcar el globo como tocado
                    isTouched = true;

                    
                    if (CompareTag("Balloon"))
                    {

                        // Desactivar el renderer y el collider
                        renderObject.enabled = false;
                        colliderObject.enabled = false;

                        //Aumentar el contador de puntuación si el globo es normal
                        ScoreManager.scoreCount++;
                        //AUMENTAR EL CONTADOR DEL MISMO GLOBO REVENTADO PARA GAMEOVERSCREEN
                        SpawnManagerBalloons.PopBalloon(gameObject.name);                     

                    }
                    else if (CompareTag("BonusExtraPoints"))
                    {

                        // Desactivar el renderer y el collider
                        renderObject.enabled = false;
                        colliderObject.enabled = false;

                        //Aumentar el contador x2
                        ScoreManager.scoreCount += 2;

                    }
                    else if (CompareTag("BonusLife"))
                    {

                        // Desactivar el renderer y el collider
                        renderObject.enabled = false;
                        colliderObject.enabled = false;

                        //Aumentar vida
                        StartCoroutine(AddlifeAnimation());
                       
                    }
                    else if (CompareTag("BombBalloon") || gameObject.CompareTag("Bomb"))
                    {
                        //Disminuir vida
                        StartCoroutine(RemovelifeAnimation());

                        if (Bonus.livesCount < 1)
                        {
                            MainMenu.gamePaused = true;
                            bombAnimation = GetComponent<BombAnimation>();
                            // Play bomb animation
                            bombAnimation.PlayBombAnimation();
                        }
                        else
                        {
                            // Desactivar el renderer y el collider
                            renderObject.enabled = false;
                            colliderObject.enabled = false;
                        }
   
                    }

                    // Reproducir efectos de sonido y partículas
                    audioManager.PlaySFX_PopBallon();
                    explosionParticle.Play();
                    ConfettiParticles.Play();
                }
            }
        }
    }

}
