using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    public ScrollRect screenScroll;

    [Range(0.1f, 0.5f)] public float animDelay = 0.2f;

    public enum States { LoginPanel, PlayPanel, ResultPanel };

    public States screenState = States.LoginPanel;

    public System.Action onScrollEnd;

    //This sets the screen state to Login Panel on start
    void Start()
    {
        screenState = States.LoginPanel;
    }

    //This method is called to change between screens using the state enum
    public void ChangeScreen(States toScreen)
    {
        StartCoroutine(ScreenAnimation(toScreen));
    }

    //This method is called to change between screens using the state number
    public void ChangeScreen(int toScreen)
    {
        switch (toScreen)
        {
            case 0:
                StartCoroutine(ScreenAnimation(States.LoginPanel));
                break;
            case 1:
                StartCoroutine(ScreenAnimation(States.PlayPanel));
                break;
            case 2:
                StartCoroutine(ScreenAnimation(States.ResultPanel));
                break;
            default:
                Debug.Log("Out of Range");
                break;
        }
    }

    //This is the corutine called by the ChangeScreen method to animate the transition between two screens
    private IEnumerator ScreenAnimation(States newScreen)
    {
        if (newScreen != screenState)
        {
            float increment = ((int)newScreen - (int)screenState) / 40f;

            for (int i = 0; i < 20; i++)
            {
                screenScroll.horizontalNormalizedPosition += increment;

                yield return new WaitForSeconds(animDelay / 20f);
            }

            screenState = newScreen;
        }
            this.onScrollEnd?.Invoke();
    }
}
