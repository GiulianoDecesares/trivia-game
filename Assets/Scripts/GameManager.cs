using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ResultPanel resultPanelScript;
    [SerializeField] private PlayPanel playPanelScript;
    [SerializeField] private LoginPanel logInPanelScript;
    [SerializeField] private ScrollSnap ScrollControl;

    [HideInInspector] public int answeredQuestions = 0;

    [HideInInspector] public int score = 0;

    [Header("Total ammount of questions to answer in a game")]
    public int questionsAmmount = 5;

    [Header("Time given to answer each question, in seconds")]
    public int timeToAnswer = 30;
    
    #region Singleton

    public static GameManager instance = null;

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

    private void OnEnable()
    {
        this.ScrollControl.onScrollEnd += this.OnScrollSnapEndScrolling;
    }

    private void OnDisable()
    {
        this.ScrollControl.onScrollEnd -= this.OnScrollSnapEndScrolling;
    }

    #endregion // Singleton

    private void OnScrollSnapEndScrolling()
    {
        if (ScrollControl.screenState == ScrollSnap.States.PlayPanel)
        {
            this.playPanelScript.StartPlayFlow();
        }else
        {
            if (ScrollControl.screenState == ScrollSnap.States.ResultPanel)
            {
                resultPanelScript.ShowResults(score, answeredQuestions);
            }
        }
        
    }

    #region Public Methods

    // Use when a question is answered with a boolean value showing if the correct answer was pressed.

    public void OnCorrectAnswer()
    {
        this.answeredQuestions++;
        this.score++;
        this.IsGameFinished();
    }

    public void OnWrongAnswer()
    {
        this.answeredQuestions++;
        this.IsGameFinished();
    }
    
    // When Login Button is pressed it shows the Play panel
    public void OnLoginButtonPressed()
    {
        ScrollControl.ChangeScreen(ScrollSnap.States.PlayPanel);
    }

    // Restarts the game
    public void ResetGame()
    {
        logInPanelScript.ResetLoginPanel();
        answeredQuestions = 0;
        score = 0;
        ScrollControl.ChangeScreen(ScrollSnap.States.LoginPanel);
    }

    #endregion

    #region Private Methods

    // Checks if the game has ended
    private void IsGameFinished()
    {
        if (answeredQuestions >= questionsAmmount)
        {
            EndGame();
        }
        else
        {
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        playPanelScript.StartPlayFlow();
    }

    // Shows the result panel and calls the result panel to show the results
    private void EndGame()
    {
        ScrollControl.ChangeScreen(ScrollSnap.States.ResultPanel);
        playPanelScript.OnGameEnd();
        resultPanelScript.ResetResultPanel();
    }

    #endregion
}
