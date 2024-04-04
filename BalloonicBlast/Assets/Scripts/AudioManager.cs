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
    public AudioClip butttonSFX;


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

    public void PlaySFX_PopBallon()
    {
        // Elegir aleatoriamente entre los clips
        int indexSoundFX = Random.Range(0, popBalloonSounds.Length);

        soundFXSource.PlayOneShot(popBalloonSounds[indexSoundFX]);
    }
}
