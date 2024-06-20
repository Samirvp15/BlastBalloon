using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombAnimation : MonoBehaviour
{

    private Animator bombAnimator;
    private MainMenu Canvas;
    private Renderer renderObject;
    private Collider colliderObject;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Canvas = GameObject.Find("Canvas").GetComponent<MainMenu>();
        bombAnimator = GetComponent<Animator>();
        renderObject = GetComponent<Renderer>();
        colliderObject = GetComponent<Collider>();
        bombAnimator.enabled = false;
    }
    public void PlayBombAnimation()
    {
        StartCoroutine(AnimateBomb());
    }


    IEnumerator AnimateBomb()
    {
        // Pause the scene
        Time.timeScale = 0f;

        // Enable the animator and play the animation
        bombAnimator.enabled = true;
        bombAnimator.Play("BombScale");

        // Ensure the animation continues in real-time
        AnimatorStateInfo animationState = bombAnimator.GetCurrentAnimatorStateInfo(0);
        float duration = animationState.length;
        float timer = 0f;

        while (timer < duration)
        {
            bombAnimator.Update(Time.unscaledDeltaTime);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        audioManager.PlaySFX_Bomb();
        // Disable the renderer and collider after the animation
        renderObject.enabled = false;
        colliderObject.enabled = false;

        // Resume the scene
        Time.timeScale = 1f;
        MainMenu.gamePaused = false;
        bombAnimator.enabled = false;

        // Show revive ad screen
        Canvas.ShowReviveAdScreen();
    }
}
