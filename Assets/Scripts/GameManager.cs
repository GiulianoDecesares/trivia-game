using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Space]
    [Header("This is Actual Score and Ammount of Questions Answered")]
    public int Score=0;
    public int QuestionsAnswered=0;
    [Space]
    [Header("The ammount of questions to answer to finish the game")]
    public int AmmountOfQuestionsToAnswer = 5;
    [Space]
    [Header("Index of the actual category")]
    public int CurrentCategoryIndex = 0;

    private string QuestionsDataPath = "/Resources/JsonData/TriviaGame.txt";
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
        LoadQuestions();
    } 

//This function returns a random index of the Questions List
    public int QuestionRandomSelection () {
        int QuestionIndex = Random.Range(0, QuestionsList.list.Count);
        return QuestionIndex;
	}

    //Called by the Results Script to reset the values and start over.
    public void ResetGame () {
        Score = 0;
        QuestionsAnswered = 0;
	}
    public void OnAnsweredQuestion(bool Correct)
    {
        if (Correct)
        {
            Score++;
        }
        QuestionsAnswered++;
        CheckEndOfGame();
    }

    private void CheckEndOfGame()
    {
        if(QuestionsAnswered >= AmmountOfQuestionsToAnswer)
        {
            //Insertar código para mover paneles a Results
            GameObject ResultGrid = GameObject.Find("ResultGridItem").gameObject;
            if (ResultGrid != null)
            {
                ResultGrid.GetComponent<ResutManager>().ShowResults();
            }else
            {
                Debug.Log("Could not find ResultGridItem in the hierarchy");
            }
        }
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
