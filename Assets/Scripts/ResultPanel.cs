﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : Panel

{
    public Text resultText;
    public Text correctText;
    public Text incorrectText;
    public Text percentText;
    public Slider logoSlider;
    private string shareMessage = "¡Mira cuanto sé sobre Mar del Plata!";
    private int percentage;
    private int answeredQuestions;
    private int correctAnswers;
    [Range(0.005f,0.02f)]
    public float animationDelay=0.05f;
    [Space]
    [Header("Dynamic text")]
    public string Excellent= "¡Felicitaciones! ¡Sabés muchísimo de nuestra ciudad!";
    public string VeryGood= "¡Conocés mucho sobre Mar del Plata!";
    public string Good= "Sabés bastante de nuestra ciudad ; ¡Podés aprender más todavía en nuestro sitio web!";
    public string Fair= "Conocés poco nuestra ciudad ; ¡Te invitamos a nuestro sitio web para aprender más!";
    public string Bad= "Sabés poco y nada. ; ¡Te invitamos a nuestro sitio web para aprender más!";
    public string Pathetic= "Necesitas saber más de nuestra ciudad ; ¡Te invitamos a nuestro sitio web para aprender más!";

    private void Start()
    {
        this.panelType = PanelType.RESULT;
    }

    #region Public Methods

    public void ResetResultPanel()
    {
        resultText.text = "";
        correctText.text = "";
        incorrectText.text = "";
        percentText.text = "";
        logoSlider.value = 0f;
    }

    // Public method to show results on screen by entering the actual score and the ammount of questions answered
    public void ShowResults(int score, int answered) {
        answeredQuestions = answered;
        correctAnswers = score;
        percentage = Mathf.CeilToInt(((correctAnswers * 1f) / answeredQuestions)*100);
        StartCoroutine(RunSlider());
        ResetResultPanel();
    }

    //Public method called by the Share button to share a screenshot
    public void OnShareButtonPressed()
    {
        StartCoroutine(takeScreenshotAndSave());
    }

    // Public method called by the play again button to restart the game
    public void OnPlayAgainPressed()
    {
        GameManager.instance.ResetGame();
    }
    #endregion

    #region Private Methods

    // private corutine to take a screenshot and save it
    private IEnumerator takeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();
        string img_name = "ScreenShot.png";
        string destination_path = Application.persistentDataPath + "/" + img_name; ;
        System.IO.File.WriteAllBytes(destination_path, imageBytes);

        //Call Share Function
        shareScreenshot(destination_path);
    }

    // private method to share the screenshot
    private void shareScreenshot(string path)
    {
        SunShineNativeShare.ShareSingleFile(path, SunShineNativeShare.TYPE_IMAGE, shareMessage, "Compartido por Trivia MDP");
    }

    // private corutine to fill the slider pie
    private IEnumerator RunSlider()
    {
        for (int i = 0; i <= percentage; i++)
        {
            percentText.text = i.ToString() + "%";
            logoSlider.value = i;
            yield return new WaitForSeconds(animationDelay) ;
        }
        ShowSecondaryResults();
        yield return null;
    }

    //private function to add new lines in the text when a semicolon is introduced in the inspector
    private string TextBreak(string textToBreak)
    {
        return textToBreak.Replace(";", "\n");
    }

    //private method to show the secondary results
    private void ShowSecondaryResults()
    {
        correctText.text = correctAnswers.ToString()+" CORRECTAS";
        incorrectText.text = (answeredQuestions - correctAnswers).ToString() + " INCORRECTAS";
        if (percentage == 100)
        {
            resultText.text = TextBreak (Excellent);
        }
        else
        {
            if(percentage >= 80)
            {
                resultText.text = TextBreak (VeryGood);
            }
            else
            {
                if (percentage >= 60)
                {
                    resultText.text = TextBreak (Good);
                }
                else
                {
                    if(percentage >= 40)
                    {
                        resultText.text = TextBreak(Fair);
                    }
                    else
                    {
                        if (percentage >= 20)
                        {
                            resultText.text = TextBreak(Bad);
                        }
                        else
                        {
                            resultText.text = TextBreak (Pathetic);
                        }
                    }
                }
            }
        }
    }

    #endregion
}
