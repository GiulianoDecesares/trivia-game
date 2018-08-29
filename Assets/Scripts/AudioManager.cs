using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource fastTimerAudioSource;
    public AudioClip slowTimerClip;
    public AudioClip correctAnswerClip;
    public AudioClip wrongAnswerClip;
    public AudioClip carouselSpinningClip;
    public AudioClip panelSwipeClip;
    public AudioClip mainMenuTheme;
    public AudioClip NewCategory;
    public AudioClip NewCategory2;
    #region Singleton
    
    public static AudioManager instance = null;

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
    #endregion // Singleton

    #region Public Methods
    //Plays a swipe sound when moving between panels
    public void PlayMainMenuTheme ()
    {
            audioSource.loop = true;
            PlaySound(mainMenuTheme);
        
    }
    
    public void StopMainMenuTheme ()
    {
        audioSource.loop = false;
        audioSource.Stop();
        
    }
    public void PlayCategoryMusic ()
    {
        audioSource.loop = false;
        int RandomCategoryChoice = Random.Range(0,5);
        if (RandomCategoryChoice <= 2)
        {
            PlaySound(NewCategory);
        }
        else if (RandomCategoryChoice > 2)
        {
            PlaySound(NewCategory2);
        }
    }

    public void PlaySwipeSound()
    {
        audioSource.loop = false;
        PlaySound(panelSwipeClip);
    }

    //Plays the correct answer aouns when bool is true and wrong answer sound when it's false
    public void PlayButtonSound(bool IsCorrect)
    {
        audioSource.loop = false;
        fastTimerAudioSource.volume = 0f;
        fastTimerAudioSource.Stop();
        if (IsCorrect)
        {
            PlaySound(correctAnswerClip);
        }
        else
        {
            PlaySound(wrongAnswerClip);
        }
    }

    //Plays the carousel sound when this method is called with a true bool and it starts looping until this method is called again with a false bool when it stops.
    public void PlayCarouselSound(bool begining)
    {
        if (begining)
        {
            audioSource.loop = true;
            PlaySound(carouselSpinningClip);
        }
        else
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    //It plays and starts looping the slow timer clip when the timer starts, changes the sound to the fast timer clip when it's 1/3 of the time left and stops the sound when it reaches the end of the time.
    public void PlayTimerSound(int actualTime, int totalTime)
    {
        int aux = Mathf.RoundToInt((totalTime * 1f) / 3f);
        if (actualTime == totalTime)
        {
            audioSource.loop = true;
            PlaySound(slowTimerClip);
            fastTimerAudioSource.Play();
        }
        else
        {
            if (actualTime == aux)
            {
                audioSource.Stop();
                fastTimerAudioSource.volume = 1;
            }
            else
            {
                if (actualTime == 1)
                {
                    PlayButtonSound(false);
                }
            }
        }
    }
    #endregion

    #region Private Methods
    //Stops the previous sfx and plays the Clip To Play.
    private void PlaySound(AudioClip ClipToPlay)
    {
        audioSource.Stop();
        audioSource.clip = ClipToPlay;
        audioSource.Play();
    }
    #endregion
}
