using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance = null;
    public int seconds = 0;
    public static UnityAction OnTimeOut;
    public static UnityAction OnTick;

    private GameManager gm;
    
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
        gm = GameManager.instance;
    }

    public void StartCountdown ()
    {
        seconds = gm.timeToAnswer;
        if (seconds > 1)
        {
            StartCoroutine(GameClock());
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
        for (int i=seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            if (OnTick != null)
            {
                OnTick();
            }
        }
        if(OnTimeOut != null)
        {
            OnTimeOut();
        }
        yield return null;
    }
}
