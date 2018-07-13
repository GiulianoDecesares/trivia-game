using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {
    [Header("Insert Your Email InputField Here")]
    public InputField Email;
    [Space]
    [Header("This is the actual Score")]
    public int Score=0;
    [Space]
    [Header("Index of the actual category")]
    public int CurrentCategoryIndex = 0;

    private string gameDataProjectFilePath = "/Emails.txt";
    private string QuestionsDataPath = "/Resources/JsonData/TriviaGame.txt";
    private string AllEmails = "";
    public string LoadedFromJson = "";
    [HideInInspector]
    public QuestionList QuestionsList;

    #region Sigleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    //Loads TriviaGame.txt and Emails.txt into memory on start
    public void Start()
    {
        LoadMails();
        LoadQuestions();
    } 

//This function returns a random index of the Questions List
    public int QuestionRandomSelection () {
        int QuestionIndex = Random.Range(0, QuestionsList.list.Count);
        return QuestionIndex;
	}

//This is used by the "Play Now" button in the main screen to save the e-mail address to a file.
    public void EmailInput()
    {
        if (AllEmails == "")
        {
            AllEmails = Email.text;
        }
        else
        {
            AllEmails = AllEmails + ";" + Email.text;
        }
        SaveMails();
    }

    //Internal Use - To load the Mails saved in Emails.txt    
   private void LoadMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            AllEmails = File.ReadAllText(filePath);
        }
        else
        {
            File.Create(filePath);
            AllEmails = "";
        }
    }

    //Internal Use - To save a new e-mail address in the file Emails.txt   
    private void SaveMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, AllEmails); 
    }

    //Called by the Results Script to reset the values and start over.
    public void ResetGame () {
        Score = 0;
        Email.text = "";
	}
    public void ScoreRightQuestion()
    {
        Score++;
    }

    public void LoadQuestions()
    {
        LoadedFromJson = File.ReadAllText(Application.dataPath + QuestionsDataPath);
        QuestionsList = JsonUtility.FromJson<QuestionList>(LoadedFromJson);
    }
}




[System.Serializable]
public class Questions
{
    public string Question;
    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string Answer4;
    public int CorrectAnswer;
    public string Category;
}

[System.Serializable]
public class QuestionList
{
        public List<Questions> list;
}
