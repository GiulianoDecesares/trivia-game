using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResutManager : MonoBehaviour {
    
    public int Percentage = 0;
    [Space]
    [Header("Results Animation delay")]
    [Range(0.3f, 2f)]
    public float AnimationDelay = 0.5f;
    [Space]
    [Header("Results Messages for the Result Text Output")]
    public string Text100 = "Excelente";
    public string Text75 = "Muy Bien";
    public string Text50 = "Bien";
    public string Text25 = "Mas o menos";
    public string Text0 = "Mal";
    [Space]
    [Header("Text and graphic Output")]
    public Text ResultText;
    public Slider LogoSlider;
    public Text PercentText;
    public Text CorrectAnswers;
    public Text WrongAnswers;

    // This method calculates the Percentage of right answers, transform it into an integer and calls other methods to show it
    public void ShowResults()
    {
        Percentage = Mathf.RoundToInt((GameManager.instance.Score * 1f / GameManager.instance.QuestionsAnswered) * 100f);
        ShowPercentage();
    }

    // This resets the game when you press the Play Again Button
    public void PlayAgainButton()
    {
        GameManager.instance.ResetGame();
    }

    //This is a private method that calls the Logo Animation coroutine if the Logo slider and percent text are present
    private void ShowPercentage()
    {
        if (LogoSlider == null)
        {
            Debug.Log("The slider of the Logo Slider is missing");
        }
        else
        {
            if (PercentText == null)
            {
                Debug.Log("The Percent Text is missing");
            }
            else
            {
                StartCoroutine("LogoAnimation");
            }
        }
    }

    //This is a coroutine that animates the Logo Slider and the Percent Text and shows the Dynamic Result Text and the Correct and Wrong anwers ammounts once the animation is finished
    IEnumerator LogoAnimation()
    {
        ResultText.text = "";
        CorrectAnswers.text = "";
        WrongAnswers.text = "";
        if (Percentage != 0)
        {
            for (int i = 0; i <= Percentage; i++)
            {
                LogoSlider.value = i * 1f;
                PercentText.text = i.ToString() + "%";
                yield return new WaitForSeconds(AnimationDelay / Percentage);
            }
        }
        else
        {
            LogoSlider.value = 0f;
            PercentText.text = "0 %";
        }
        DyanamicResultText();
        ShowAmmountOfAnswers();
        yield return null;
    }

    //This is a private method that shows the ammount of correct and wrong answers 
    private void ShowAmmountOfAnswers()
    {
        if (CorrectAnswers == null)
        {
            Debug.Log("The Correct Answers text is missing");
        }
        else
        {
            if (WrongAnswers == null)
            {
                Debug.Log("The Wrong Answers text is missing");
            }
            else
            {
                CorrectAnswers.text = GameManager.instance.Score.ToString() + " CORRECTAS";
                WrongAnswers.text = (GameManager.instance.QuestionsAnswered - GameManager.instance.Score).ToString() + " INCORRECTAS";
            }
        }
    }

    //This is a private method that shows different strings in the Result Text Output
    private void DyanamicResultText()
    {
        string TextToShow = "";
        if (Percentage == 100)
        {
            TextToShow = Text100;
        }
        else
        {
            if (Percentage > 75)
            {
                TextToShow = Text75;
            }
            else
            {
                if (Percentage > 50)
                {
                    TextToShow = Text50;
                }
                else
                {
                    if (Percentage > 25)
                    {
                        TextToShow = Text25;
                    }
                    else
                    {
                        TextToShow = Text0;
                    }
                }
            }
        }
        if (ResultText != null)
        {
            ResultText.text = TextToShow;
        }
        else
        {
            Debug.Log("The Result Text is missing");
        }
    }
}
