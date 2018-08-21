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


	
public void PlaySwipeSound()
    {
        audioSource.loop = false;
        PlaySound(panelSwipeClip);
    }
	public void PlayButtonSound (bool IsCorrect) {
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
    public void PlayCarouselSound(bool begining)
    {
        if (begining)
        {
            audioSource.loop = true;
            PlaySound(carouselSpinningClip);
        }else
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    private void PlaySound(AudioClip ClipToPlay)
    {
        audioSource.Stop();
        audioSource.clip = ClipToPlay;
        audioSource.Play();
    }

    public void PlayTimerSound(int actualTime, int totalTime)
    {
        int aux = Mathf.RoundToInt((totalTime * 1f) / 3f);
        if (actualTime == totalTime)
        {
            audioSource.loop = true;
            PlaySound(slowTimerClip);
            fastTimerAudioSource.Play();
        }else
        {
            if(actualTime == aux)
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
}
