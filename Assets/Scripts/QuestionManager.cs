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

    public bool enableDeepDebug;

    [SerializeField] public int allQuestionsAmount { get; private set; }

    public List<QuestionAndAnswers> cultureAndEducationQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> environmentQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> healthQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> livingPlaceQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> mobilityAndLogisticsQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> municipalityEconomicManagementQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> productionEmploymentAndTourismQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> publicInformationAccessQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> socialQuestions = new List<QuestionAndAnswers>();
    public List<QuestionAndAnswers> territoryQuestions = new List<QuestionAndAnswers>();

    [HideInInspector] public enum Categories
    {
        CULTURE_AND_EDUCATION,
        ENVIRONMENT,
        HEALTH,
        LIVING_PLACE,
        MOBILITY_AND_LOGISTICS,
        MUNICIPALITY_ECONOMIC_MANAGEMENT,
        PRODUCTION_EMPLOYMENT_AND_TOURISM,
        PUBLIC_INFORMATION_ACCES,
        SOCIAL,
        TERRITORY
    };
    
    #endregion

    #region Singleton

    public static QuestionManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        this.PopulateAllLists(this.enableDeepDebug);
    }

    #endregion

    #region Mono Behaviour Methods

    private void Start()
    {
        this.allQuestionsAmount = this.AllQuestionsAmount();
    }   

    #endregion

    #region Private Methods

    private void PopulateAllLists(bool enableDeepDebug)
    {
        this.cultureAndEducationQuestions = this.PopulateList(this.cultureAndEducation, false);
        this.environmentQuestions = this.PopulateList(this.environment, false);
        this.healthQuestions = this.PopulateList(this.health, false);
        this.livingPlaceQuestions = this.PopulateList(this.livingPlace, false);
        this.mobilityAndLogisticsQuestions = this.PopulateList(this.mobilityAndLogistics, false);
        this.municipalityEconomicManagementQuestions = this.PopulateList(this.municipalityEconomicManagement, false);
        this.productionEmploymentAndTourismQuestions = this.PopulateList(this.productionEmploymentAndTourism, false);
        this.publicInformationAccessQuestions = this.PopulateList(this.publicInformationAccess, false);
        this.socialQuestions = this.PopulateList(this.social, false);
        this.territoryQuestions = this.PopulateList(this.territory, false);

        if(enableDeepDebug)
        {
            this.DebugQuestionsList(this.cultureAndEducationQuestions);
            this.DebugQuestionsList(this.environmentQuestions);
            this.DebugQuestionsList(this.healthQuestions);
            this.DebugQuestionsList(this.livingPlaceQuestions);
            this.DebugQuestionsList(this.mobilityAndLogisticsQuestions);
            this.DebugQuestionsList(this.municipalityEconomicManagementQuestions);
            this.DebugQuestionsList(this.productionEmploymentAndTourismQuestions);
            this.DebugQuestionsList(this.publicInformationAccessQuestions);
            this.DebugQuestionsList(this.socialQuestions);
            this.DebugQuestionsList(this.territoryQuestions);
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

    public Categories GetRandomCategory(bool getRepeatedEnabled)
    {
        return Categories.CULTURE_AND_EDUCATION; // This isn't working!
    }

    public QuestionAndAnswers GetRandomQuestionByCategory(Categories category, bool getRepeatedEnabled)
    {
        QuestionAndAnswers result = new QuestionAndAnswers();

        if(getRepeatedEnabled)
        {
            // Simple get repeated behaviour 
        }
        else
        {
            // Not so simple bloody get unrepeated question behaviour
        }

        return result;
    }

    public int AllQuestionsAmount()
    {
        int result = 0;

        result = this.cultureAndEducationQuestions.Count
            + this.environmentQuestions.Count
            + this.healthQuestions.Count
            + this.livingPlaceQuestions.Count
            + this.mobilityAndLogisticsQuestions.Count
            + this.municipalityEconomicManagementQuestions.Count
            + this.productionEmploymentAndTourismQuestions.Count
            + this.publicInformationAccessQuestions.Count
            + this.socialQuestions.Count
            + this.territoryQuestions.Count;

        return result;
    }

    public int ByCategoryQuestionsAmount(Categories category)
    {
        int result = 0;

        switch (category)
        {
            case Categories.CULTURE_AND_EDUCATION:
                result = this.cultureAndEducationQuestions.Count;
                break;
            case Categories.ENVIRONMENT:
                result = this.environmentQuestions.Count;
                break;
            case Categories.HEALTH:
                result = this.healthQuestions.Count;
                break;
            case Categories.LIVING_PLACE:
                result = this.livingPlaceQuestions.Count;
                break;
            case Categories.MOBILITY_AND_LOGISTICS:
                result = this.mobilityAndLogisticsQuestions.Count;
                break;
            case Categories.MUNICIPALITY_ECONOMIC_MANAGEMENT:
                result = this.municipalityEconomicManagementQuestions.Count;
                break;
            case Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM:
                result = this.productionEmploymentAndTourismQuestions.Count;
                break;
            case Categories.PUBLIC_INFORMATION_ACCES:
                result = this.publicInformationAccessQuestions.Count;
                break;
            case Categories.SOCIAL:
                result = this.socialQuestions.Count;
                break;
            case Categories.TERRITORY:
                result = this.territoryQuestions.Count;
                break;
        }

        return result;
    }

    #endregion

}
