using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionAndAnswers
{
    public string question;

    public List<string> wrongAnswers;

    public string rightAnswer;

    public QuestionAndAnswers()
    {
        this.question = "";
        this.wrongAnswers = new List<string>();
        this.rightAnswer = "";
    }

    public QuestionAndAnswers(QuestionAndAnswers thisObj)
    {
        this.question = thisObj.question;
        this.wrongAnswers = thisObj.wrongAnswers;
        this.rightAnswer = thisObj.rightAnswer;
    }

    public void Clear()
    {
        this.question = "";
        this.rightAnswer = "";

        this.wrongAnswers.Clear();
    }

    public void ShowInConsole()
    {
        Debug.Log("Question: " + this.question + "\n");
        Debug.Log("Wrong answers:");

        for(int index = 0; index < this.wrongAnswers.Count; index++)
        {
            Debug.Log(this.wrongAnswers[index] + "\n");
        }

        Debug.Log("Right answer: " + this.rightAnswer);
    }
}

public class QuestionManager : MonoBehaviour
{
    #region Text Assets

    [SerializeField] private TextAsset cultureAndEducation;
    [SerializeField] private TextAsset environment;
    [SerializeField] private TextAsset health;
    [SerializeField] private TextAsset livingPlace;
    [SerializeField] private TextAsset mobilityAndLogistics;
    [SerializeField] private TextAsset productionEmploymentAndTourism;
    [SerializeField] private TextAsset publicInformationAccess;
    [SerializeField] private TextAsset social;
    [SerializeField] private TextAsset territory;

    #endregion

    #region Public Questions Lists And Accesors
    
    public int allQuestionsAmount { get; private set; }

    // This enables a debug functionality of the load of all questions, in the console
    public bool deepDebug;

    [HideInInspector] public enum Categories
    {
        CULTURE_AND_EDUCATION,
        ENVIRONMENT,
        HEALTH,
        LIVING_PLACE,
        MOBILITY_AND_LOGISTICS,
        PRODUCTION_EMPLOYMENT_AND_TOURISM,
        PUBLIC_INFORMATION_ACCESS,
        SOCIAL,
        TERRITORY
    };

    #endregion

    #region Private Accesors

    private System.Random questionsRandomizer = new System.Random();
    private System.Random categoriesRandomizer = new System.Random();

    // This data structure should not be modified and you should always save all the data of the questions.
    private Dictionary<Categories, List<QuestionAndAnswers>> questionsByCategory = new Dictionary<Categories, List<QuestionAndAnswers>>();

    // These two data structures keep a copy of all the questions and keep track of which of them were answered and which were not.
    // They are volatile as hell.
    private Dictionary<Categories, List<QuestionAndAnswers>> remainingQuestions = new Dictionary<Categories, List<QuestionAndAnswers>>();

    // These two data structures keep a copy of all the categories and keep track of which of them were used and which were not. 
    // They are volatile as hell.
    private List<Categories> remainingCategories = new List<Categories>();
    private List<Categories> categoriesAlreadySelected = new List<Categories>();

    #endregion

    #region Singleton

    public static QuestionManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        this.PopulateDictionary(this.deepDebug);

        this.PopulateCategoriesList();

        this.allQuestionsAmount = this.AllQuestionsAmount();
    }

    #endregion

    #region Private Methods

    private void PopulateCategoriesList()
    {
        foreach(Categories thisCategory in this.questionsByCategory.Keys)
        {
            this.remainingCategories.Add(thisCategory);
        }
    }

    private void PopulateDictionary(bool deepDebug)
    {
        this.questionsByCategory[Categories.CULTURE_AND_EDUCATION] = this.PopulateList(this.cultureAndEducation, false);
        this.questionsByCategory[Categories.ENVIRONMENT] = this.PopulateList(this.environment, false);
        this.questionsByCategory[Categories.HEALTH] = this.PopulateList(this.health, false);
        this.questionsByCategory[Categories.LIVING_PLACE] = this.PopulateList(this.livingPlace, false);
        this.questionsByCategory[Categories.MOBILITY_AND_LOGISTICS] = this.PopulateList(this.mobilityAndLogistics, false);
        this.questionsByCategory[Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM] = this.PopulateList(this.productionEmploymentAndTourism, false);
        this.questionsByCategory[Categories.PUBLIC_INFORMATION_ACCESS] = this.PopulateList(this.publicInformationAccess, false);
        this.questionsByCategory[Categories.SOCIAL] = this.PopulateList(this.social, false);
        this.questionsByCategory[Categories.TERRITORY] = this.PopulateList(this.territory, false);

        this.remainingQuestions[Categories.CULTURE_AND_EDUCATION] = this.PopulateList(this.cultureAndEducation, false);
        this.remainingQuestions[Categories.ENVIRONMENT] = this.PopulateList(this.environment, false);
        this.remainingQuestions[Categories.HEALTH] = this.PopulateList(this.health, false);
        this.remainingQuestions[Categories.LIVING_PLACE] = this.PopulateList(this.livingPlace, false);
        this.remainingQuestions[Categories.MOBILITY_AND_LOGISTICS] = this.PopulateList(this.mobilityAndLogistics, false);
        this.remainingQuestions[Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM] = this.PopulateList(this.productionEmploymentAndTourism, false);
        this.remainingQuestions[Categories.PUBLIC_INFORMATION_ACCESS] = this.PopulateList(this.publicInformationAccess, false);
        this.remainingQuestions[Categories.SOCIAL] = this.PopulateList(this.social, false);
        this.remainingQuestions[Categories.TERRITORY] = this.PopulateList(this.territory, false);

        if (deepDebug)
        {
            foreach(List<QuestionAndAnswers> lists in this.questionsByCategory.Values)
            {
                foreach(QuestionAndAnswers question in lists)
                {
                    question.ShowInConsole();
                }
            }
        }
    }

    private void DebugQuestionsList(List<QuestionAndAnswers> thisList)
    {
        for (int index = 0; index < thisList.Count; index++)
        {
            thisList[index].ShowInConsole();
        }
    }

    private List<QuestionAndAnswers> PopulateList(TextAsset thisCSV, bool enableDebug)
    {
        List<QuestionAndAnswers> result = new List<QuestionAndAnswers>();

        string[,] grid = this.SplitCSVText(thisCSV.text);
        
        int gridColumnAmount = grid.GetUpperBound(0);
        int gridRowAmount = grid.GetUpperBound(1);

        for (int currentRow = 1; currentRow < gridRowAmount; currentRow++)
        {
            QuestionAndAnswers currentQuestion = new QuestionAndAnswers();

            for(int currentColumn = 0; currentColumn < gridColumnAmount; currentColumn++)
            {
                if (currentColumn == 0)
                {
                    // Question case

                    if (enableDebug)
                    {
                        Debug.Log("Question at column " + currentColumn + " and row " + currentRow + " \"" + grid[currentColumn, currentRow] + "\""); 
                    }

                    currentQuestion.question = grid[currentColumn, currentRow];
                }
                else if(currentColumn == gridColumnAmount - 1)
                {
                    // Right answer case

                    if (enableDebug)
                    {
                        Debug.Log("Right answer at column " + currentColumn + " and row " + currentRow + " \"" + grid[currentColumn, currentRow] + "\""); 
                    }

                    currentQuestion.rightAnswer = grid[currentColumn, currentRow];
                }
                else
                {
                    // Wrong answers case

                    if (enableDebug)
                    {
                        Debug.Log("Wrong answer at column " + currentColumn + " and row " + currentRow + " \"" + grid[currentColumn, currentRow] + "\""); 
                    }

                    currentQuestion.wrongAnswers.Add(grid[currentColumn, currentRow]);
                }
            }

            result.Add(currentQuestion);
        }

        return result;
    }

    private string[,] SplitCSVText(string scvRawText)
    {
        string[] lines = scvRawText.Split("\n"[0]);

        // Finds the max width of row

        int width = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);

            width = Mathf.Max(width, row.Length);
        }

        // Creates new 2D string grid to output to

        string[,] outputGrid = new string[width + 1, lines.Length + 1];

        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);

            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
            }
        }

        return outputGrid;
    }

    // Splits a CSV row 
    private string[] SplitCsvLine(string line)
    {
        return line.Split(',');
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Reset all the lists of repeated questions so they can be used again
    /// </summary>
    public void ResetQuestionsRepeatedCount()
    {
        this.remainingQuestions.Clear();
        this.questionsByCategory.Clear();

        this.PopulateDictionary(this.deepDebug);
    }

    /// <summary>
    /// Reset all the lists of repeated categories so they can be used again
    /// </summary>
    public void ResetCategoryRepeatedCount()
    {
        this.remainingCategories.Clear();

        foreach (Categories thisCategory in this.categoriesAlreadySelected)
        {
            this.remainingCategories.Add(thisCategory);
        }
    }

    /// <summary> 
    /// The call to this method with the parameter to avoid repetition of the return value, 
    /// will return a value and instantaneously prevent it from repeating. 
    /// Be sure to use each return value. 
    /// </summary>    /// 
    /// <param name="getRepeatedEnabled"> 
    /// If is set to true, will avoid the repetition of the return value
    /// </param>
    /// <returns>
    /// Returns a category
    /// </returns>
    public Categories GetRandomCategory(bool getRepeatedEnabled = false)
    {
        Categories result;

        if(getRepeatedEnabled)
        {
            // Simple get-repeated behaviour
            result = (Categories)Random.Range(0, 10);
        }
        else
        {
            // Not so simple bloody get-unrepeated behaviour

            if(this.remainingCategories.Count > 0)
            {
                result = this.remainingCategories[this.categoriesRandomizer.Next(this.remainingCategories.Count)];

                this.categoriesAlreadySelected.Add(result);

                this.remainingCategories.Remove(result);
            }
            else
            {
                Debug.LogWarning("No unrepeated categories left, returning repeated random category");

                result = (Categories)Random.Range(0, 10);
            }
        }

        return result;
    }

    /// <summary>
    /// The call to this method with the parameter to avoid repetition of the return value,
    /// will return a QuestionAndAnswers structure and instantaneously prevent it from repeating. 
    /// Be sure to use each return value. 
    /// </summary>
    /// <param name="category">
    /// The category the category from which the random question will be selected
    /// </param>
    /// <param name="getRepeatedEnabled">
    /// If is set to true, will avoid the repetition of the return value
    /// </param>
    /// <returns>
    /// Returns a QuestionAndAnswers structure 
    /// </returns>
    public QuestionAndAnswers GetRandomQuestionByCategory(Categories category, bool getRepeatedEnabled = false)
    {
        QuestionAndAnswers result = new QuestionAndAnswers();

        List<QuestionAndAnswers> currentQuestionsList = null;

        if(getRepeatedEnabled)
        {
            // Simple get-repeated behaviour

            this.questionsByCategory.TryGetValue(category, out currentQuestionsList);

            result = currentQuestionsList[this.questionsRandomizer.Next(currentQuestionsList.Count)];
        }
        else
        {
            // Not so simple bloody get-unrepeated behaviour
            this.remainingQuestions.TryGetValue(category, out currentQuestionsList);

            if(currentQuestionsList != null)
            {
                if(currentQuestionsList.Count > 0)
                {
                    result = currentQuestionsList[this.questionsRandomizer.Next(currentQuestionsList.Count)];

                    currentQuestionsList.Remove(result);

                }
                else
                {
                    Debug.LogWarning("No unrepeated questions remaining. Returning repeated question");

                    // Simple get-repeated behaviour

                    this.questionsByCategory.TryGetValue(category, out currentQuestionsList);

                    result = currentQuestionsList[this.questionsRandomizer.Next(currentQuestionsList.Count)];
                }
            }
            else
            {
                Debug.LogError("Null list returned");
            }
        }

        return result;
    }

    public int AllQuestionsAmount()
    {
        int result = 0;

        foreach(List<QuestionAndAnswers> lists in this.questionsByCategory.Values)
        {
            result += lists.Count;
        }

        return result;
    }

    public int ByCategoryQuestionsAmount(Categories category)
    {
        int result = 0;

        List<QuestionAndAnswers> outValue;

        this.questionsByCategory.TryGetValue(category, out outValue);

        if(outValue != null)
        {
            result = outValue.Count;
        }

        return result;
    }

    #endregion
}
