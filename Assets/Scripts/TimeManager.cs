using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {
    public int seconds = 0;
    static UnityAction OnTimeOut;

    #region Singleton

    public static SpriteManager instance;

    #endregion

    // Starts the clock corutine. Works for more than 2 seconds.
    public void StartCountdown (int givenTime)
    {
        seconds = givenTime;
        if (givenTime > 1)
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
        for (int i=1; i < seconds; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        if(OnTimeOut != null)
        {
            OnTimeOut();
        }else {
            Debug.Log("There are no listeners for OnTimeOut event");
        }
        yield return null;
    }
}
