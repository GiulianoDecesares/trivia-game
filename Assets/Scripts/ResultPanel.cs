using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {
    [SerializeField] private Button shareButton;
    [SerializeField] private Button playAgainButton;

    public Text resultText;
    public Text correctText;
    public Text incorrectText;
    public Text percentText;

    public Slider logoSlider;

    private string shareMessage = "¡Mirá cuanto sé sobre Mar del Plata!";
    private int percentage;
    private int answeredQuestions;
    private int correctAnswers;

    [Range(0.005f,0.02f)]
    public float animationDelay=0.05f;

    private string veryGood= "Felicitaciones!! Sabés mucho de nuestra ciudad!";
    private string fair= "Bueno… podría ser peor… pero mejor también!! Seguí jugando para conocer más de nuestra ciudad!";
    private string bad= "Estás al horno!! Es importante conocer cómo está nuestra ciudad. Seguí jugando!";

    #region Public Methods
    public void ResetResultPanel()
    {
        this.playAgainButton.interactable = true;
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
        this.shareButton.interactable = false;
        StartCoroutine(takeScreenshotAndSave());
    }

    // Public method called by the play again button to restart the game
    public void OnPlayAgainPressed()
    {
        this.playAgainButton.interactable = false;
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
            if(percentage >= 70)
            {
                resultText.text = TextBreak(veryGood);
            }
            else
            {
                if(percentage >= 40)
                {
                    resultText.text = TextBreak(fair);
                }
                else
                {
                    if (percentage < 40)
                    {
                        resultText.text = TextBreak(bad);
                    }
                }
            }
        
    }

    #endregion
}
