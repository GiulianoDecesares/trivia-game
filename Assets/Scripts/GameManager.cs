using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject resultPanel;
    private ResultPanel resultPanelScript;

    [HideInInspector]
    public int answeredQuestions = 0;

    [HideInInspector]
    public int score = 0;

    [Header("Total ammount of questions to answer in a game")]
    public int questionsAmmount = 5;

    [Header("Time given to answer each question")]
    public int timeToAnswer = 15;

    public ScrollSnap ScrollControl;

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

    #endregion

    #region Public Methods
    //Use when a question is answered with a boolean value showing if the correct answer was pressed.
    public void AnsweredQuestion(bool isCorrect)
    {
        answeredQuestions++;
        if (isCorrect)
        {
            score++;
        }
        IsGameFinished();
    }

    //When Login Button is pressed it shows the Play panel.
    public void OnLoginButtonPressed()
    {
        ScrollControl.ChangeScreen(ScrollSnap.States.PlayPanel);
    }

    //Restarts the game.
    public void ResetGame()
    {
        answeredQuestions = 0;
        score = 0;
        ScrollControl.ChangeScreen(ScrollSnap.States.PlayPanel);
        QuestionManager.instance.ResetQuestionsRepeatedCount();
        QuestionManager.instance.ResetCategoryRepeatedCount();
    }
    #endregion

    #region Private Methods
    //Checks if the game has ended - Private
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
        ///PlayPanel.NextQuestion();
    }

    //Shows the result panel and calls the result panel to show the results
    private void EndGame()
    {
        ScrollControl.ChangeScreen(ScrollSnap.States.ResultPanel);
        resultPanelScript.ShowResults(score, answeredQuestions);
    }

    #endregion

    #region Unity Events

    private void Start()
    {
        resultPanelScript = resultPanel.GetComponent<ResultPanel>();
    }

    #endregion
}
