using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-----Music Source")]
    [SerializeField] AudioSource homeMusicSource;
    [SerializeField] AudioSource gameMusicSource;
    [SerializeField] AudioSource soundFXSource;

    public AudioClip gameMusic;
    public AudioClip homeMusic;

    [Header("----Sound FX")]
    public AudioClip[] popBalloonSounds;
    public AudioClip bombSFX;
    public AudioClip butttonSFX;
    public AudioClip starCompletedSFX;
    public AudioClip star2CompletedSFX;
    public AudioClip totalStarsCompletedSFX;


    // Start is called before the first frame update
    void Start()
    {
        gameMusicSource.clip = gameMusic;
        homeMusicSource.clip = homeMusic;

        if (SceneManager.GetActiveScene().name == "HomeMenu")
        {
            homeMusicSource.Play();
        }else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            gameMusicSource.Play();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFXButton()
    {
        soundFXSource.PlayOneShot(butttonSFX);
    }
    public void PlayStarCompletedSFX()
    {
        soundFXSource.PlayOneShot(starCompletedSFX);
    }
    public void PlayStar2CompletedSFX()
    {
        soundFXSource.PlayOneShot(star2CompletedSFX);
    }
    public void PlayTotalStarsCompletedSFX()
    {
        soundFXSource.PlayOneShot(totalStarsCompletedSFX);
    }

    public void PlaySFX_Bomb()
    {
        soundFXSource.PlayOneShot(bombSFX);
    }

    public void PlaySFX_PopBallon()
    {
        // Elegir aleatoriamente entre los clips
        int indexSoundFX = Random.Range(0, popBalloonSounds.Length);

        soundFXSource.PlayOneShot(popBalloonSounds[indexSoundFX]);
    }
}
