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
    [SerializeField] private TextAsset municipalityEconomicManagement;
    [SerializeField] private TextAsset productionEmploymentAndTourism;
    [SerializeField] private TextAsset publicInformationAccess;
    [SerializeField] private TextAsset social;
    [SerializeField] private TextAsset territory;

    #endregion

    #region Public Questions Lists And Accesors
    
    public int allQuestionsAmount { get; private set; }

    public bool deepDebug;

    [HideInInspector] public enum Categories
    {
        CULTURE_AND_EDUCATION,
        ENVIRONMENT,
        HEALTH,
        LIVING_PLACE,
        MOBILITY_AND_LOGISTICS,
        MUNICIPALITY_ECONOMIC_MANAGEMENT,
        PRODUCTION_EMPLOYMENT_AND_TOURISM,
        PUBLIC_INFORMATION_ACCESS,
        SOCIAL,
        TERRITORY
    };

    #endregion

    #region Private Accesors

    private Dictionary<Categories, List<QuestionAndAnswers>> questionsByCategory = new Dictionary<Categories, List<QuestionAndAnswers>>();

    private Dictionary<Categories, List<QuestionAndAnswers>> questionsAlreadyAnswered = new Dictionary<Categories, List<QuestionAndAnswers>>();

    private List<Categories> categoriesAlreadySelected = new List<Categories>();

    #endregion

    #region Singleton

    public static QuestionManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        this.Populate(this.deepDebug);
    }

    #endregion

    #region Mono Behaviour Methods

    private void Start()
    {
        this.allQuestionsAmount = this.AllQuestionsAmount();
    }   

    #endregion

    #region Private Methods

    private void Populate(bool deepDebug)
    {
        this.questionsByCategory[Categories.CULTURE_AND_EDUCATION] = this.PopulateList(this.cultureAndEducation, false);
        this.questionsByCategory[Categories.ENVIRONMENT] = this.PopulateList(this.environment, false);
        this.questionsByCategory[Categories.HEALTH] = this.PopulateList(this.health, false);
        this.questionsByCategory[Categories.LIVING_PLACE] = this.PopulateList(this.livingPlace, false);
        this.questionsByCategory[Categories.MOBILITY_AND_LOGISTICS] = this.PopulateList(this.mobilityAndLogistics, false);
        this.questionsByCategory[Categories.MUNICIPALITY_ECONOMIC_MANAGEMENT] = this.PopulateList(this.municipalityEconomicManagement, false);
        this.questionsByCategory[Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM] = this.PopulateList(this.productionEmploymentAndTourism, false);
        this.questionsByCategory[Categories.PUBLIC_INFORMATION_ACCESS] = this.PopulateList(this.publicInformationAccess, false);
        this.questionsByCategory[Categories.SOCIAL] = this.PopulateList(this.social, false);
        this.questionsByCategory[Categories.TERRITORY] = this.PopulateList(this.territory, false);

        if(deepDebug)
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

    public void ResetQuestionsRepeatedCount()
    {

    }

    public void ResetCategoryRepeatedCount()
    {

    }

    public Categories GetRandomCategory(bool getRepeatedEnabled)
    {
        if(getRepeatedEnabled)
        {

        }
        else
        {

        }

        return Categories.CULTURE_AND_EDUCATION; // This isn't working!
    }

    public QuestionAndAnswers GetRandomQuestionByCategory(Categories category, bool getRepeatedEnabled)
    {
        QuestionAndAnswers result = new QuestionAndAnswers();

        if(getRepeatedEnabled)
        {
            // Simple get-repeated behaviour 
        }
        else
        {
            // Not so simple bloody get-unrepeated question behaviour
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
