using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider GameMusicSlider;
    public Slider SoundFXSlider;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("GameMusic") && PlayerPrefs.HasKey("SoundFX"))
        {
            loadGameMusicVolume();
            loadSoundFXVolume();
        }
        else
        {
            SetGameMusicVolume();
            SetSoundFXVolume();
        }
        

    }

    public void SetGameMusicVolume() {
        float musicVolume = GameMusicSlider.value;
        audioMixer.SetFloat("GameMusic", Mathf.Log10(musicVolume)*20);
        PlayerPrefs.SetFloat("GameMusic", musicVolume);
    }
    public void SetSoundFXVolume()
    {
        float soundFXVolume = SoundFXSlider.value;
        audioMixer.SetFloat("SoundFX", Mathf.Log10(soundFXVolume) * 20);
        PlayerPrefs.SetFloat("SoundFX", soundFXVolume);
    }

    private void loadGameMusicVolume()
    {
        GameMusicSlider.value = PlayerPrefs.GetFloat("GameMusic");
        SetGameMusicVolume();
    }
    private void loadSoundFXVolume()
    {
        SoundFXSlider.value = PlayerPrefs.GetFloat("SoundFX");
        SetSoundFXVolume();
    }

}
