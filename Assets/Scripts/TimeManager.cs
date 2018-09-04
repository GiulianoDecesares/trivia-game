using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance = null;

    public static UnityAction OnTimeOut;
    public static UnityAction OnTick;

    public int seconds = 0;

    private GameManager gameManager;
    
    #region Singleton

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    
    // Starts the clock corutine. Works for more than 2 seconds.
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void StartCountdown ()
    {
        seconds = gameManager.timeToAnswer;
        if (seconds > 1)
        {
            StartCoroutine(GameClock());
        }
        else
        {
            Debug.Log("givenTime must be greater than 1 second");
        }
	}

	//Stops the clock corutine
    public void StopCountdown()
    {
        StopAllCoroutines();
    }

    //Clock corutine
    private IEnumerator GameClock()
    {
        for (int i=seconds; i >0; i--)
        {
            yield return new WaitForSeconds(1f);
            AudioManager.instance.PlayTimerSound(i, seconds);
            OnTick?.Invoke();
        }

        OnTimeOut?.Invoke();

        yield return null;
    }
}
