using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {
    public int QuestionsAnswered;
    public int Percentage = 0;
    [Space]
    [Header("Results Messages to show")]
    public string Text100 = "Excelente";
    public string Text75 = "Muy Bien";
    public string Text50 = "Bien";
    public string Text25 = "Mas o menos";
    public string Text0 = "Mal";
    [Space]
    [Header("Text Output")]
    public Text ResultTextOutput;

    // This fuction calculates the Percentage of right answers and transform it into an integer
    public void ShowResults () {
        Percentage = Mathf.RoundToInt((GameManager.instance.Score*1f / QuestionsAnswered) * 100f);
        DyanamicResultText();

    }
	
	// This resets the game when you press the Play Again Button
	public void PlayAgainButton () {
        GameManager.instance.ResetGame();
	}

    public void ShareButton()
    {

    }
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
