using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private int answeredQuestions = 0;
    private int score = 0;
    [Header("Total ammount of questions to answer in a game")]
    public int questionsAmmount = 5;
    public ScrollSnap ScrollControl;


    #region Singleton

    public static SpriteManager instance;

    #endregion

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

    //Checks if the game has ended - Private
    private void IsGameFinished()
    {
        if (answeredQuestions >= questionsAmmount)
        {
            EndGame();
        }else
        {
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        ///QuestionsLoader.NextQuestion();
    }

    //Shows the result panel and calls the result panel to show the results
    private void EndGame()
    {
        ScrollControl.ChangeScreen(ScrollSnap.States.ResultPanel);
        ///ResultPanel.ShowResults(answeredQuestions, score);
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
        ///QuestionsLoader.NewGame;
    }
}
