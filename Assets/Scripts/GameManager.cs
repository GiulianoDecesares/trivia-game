using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor;
#endif

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

    private void LoadGameUI()
    {
        if(DeviceManager.instance.isTablet)
        {
            GameObject mainUI = Instantiate(PrefabManager.instance.GetPrefabByName("MainUI"));

            if(mainUI)
            {
                MainUIManager.instance.InvokeAndAddPanel(PrefabManager.instance.GetPrefabByName("LogInGridItem"));
                MainUIManager.instance.InvokeAndAddPanel(PrefabManager.instance.GetPrefabByName("QuestionsGridItem"));
                MainUIManager.instance.InvokeAndAddPanel(PrefabManager.instance.GetPrefabByName("ResultGridItem"));
            }
            else
            {
                Debug.LogError("Error :: tablet UI not found", this.gameObject);
                Debug.Break();
            }
        }
        else if(DeviceManager.instance.isPhone)
        {
            GameObject phoneUI = Instantiate(PrefabManager.instance.GetPrefabByName("PhoneUI"));

            if(phoneUI)
            {
                MainUIManager.instance.InvokeAndAddPanel(PrefabManager.instance.GetPrefabByName("PhoneLogInGridItem"));
            }
            else
            {
                Debug.LogError("Error :: phone UI not found", this.gameObject);
                Debug.Break();
            }
        }
        

        
    }

    //Loads TriviaGame.txt and Emails.txt into memory on start
    public void Start()
    {
        // Instantiation of game UI 
        LoadGameUI(); 

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
