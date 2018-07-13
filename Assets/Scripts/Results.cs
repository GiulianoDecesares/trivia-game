using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {
    public int QuestionsAnswered;
    public int Percentage = 0;
    [Space]
    [Header("Results Messages for the Result Text Output")]
    public string Text100 = "Excelente";
    public string Text75 = "Muy Bien";
    public string Text50 = "Bien";
    public string Text25 = "Mas o menos";
    public string Text0 = "Mal";
    [Space]
    [Header("Text and graphic Output")]
    public Text ResultTextOutput;
    public Slider ResultPie;
    public Text ResultPercentage;

    // This method calculates the Percentage of right answers, transform it into an integer and calls other methods to show it
    public void ShowResults () {
        Percentage = Mathf.RoundToInt((GameManager.instance.Score*1f / QuestionsAnswered) * 100f);
        DyanamicResultText();
        ShowPercentage();
    }
	
	// This resets the game when you press the Play Again Button
	public void PlayAgainButton () {
        GameManager.instance.ResetGame();
	}

    // Calls the screen capture method and the sharing options - NOT READY YET-
    public void ShareButton()
    {

    }

    //This is a private method that shows the percentage in the Result Percentage text and fills the Result pie slider to the correct percentage 
    private void ShowPercentage()
    {
        if (ResultPie == null)
        {
            Debug.Log("The slider of the Result Pie is missing");
        }else
        {
            if (ResultPercentage == null)
            {
                Debug.Log("The Text to show the Results Percentage is missing");
            }
            else
            {
                ResultPercentage.text = Percentage.ToString()+"%";
                ResultPie.value = Percentage*0.01f;
            }
        }
    }
    //This is a private method that shows different strings in the Result Text Output
    private void DyanamicResultText()
    {
        string TextToShow = "";
        if (Percentage ==100)
        {
            TextToShow = Text100;
        }
        else
        {
            if(Percentage > 75)
            {
                TextToShow = Text75;
            }
            else
            {
                if(Percentage > 50)
                {
                    TextToShow = Text50;
                }
                else
                {
                    if(Percentage > 25)
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
        if (ResultTextOutput != null)
        {
            ResultTextOutput.text = TextToShow;
        }else
        {
            Debug.Log("The Result Text is missing");
        }
    }
}
