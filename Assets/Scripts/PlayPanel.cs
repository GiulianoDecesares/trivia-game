using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    #region Private Accessors

    private List<RectTransform> optionButtonList = new List<RectTransform>();

    #endregion

    #region Public Accessors

    public Carousel carouselBehaviour;
    public RectTransform optionContainer;

    #endregion

    #region Public Methods

    public void StartPlayFlow()
    {
        if(this.optionButtonList.Count > 0)
        {
            for (int index = 0; index < this.optionButtonList.Count; index++)
            {
                Destroy(this.optionButtonList[index].gameObject);
            }

            this.optionButtonList.Clear();
        }

        QuestionManager.Categories targetCategory = QuestionManager.instance.GetRandomCategory();
        QuestionCard targetQuestionCardScript = carouselBehaviour.StartSwipeToCategory(targetCategory).GetComponent<QuestionCard>();
        QuestionAndAnswers targetQuestionData = QuestionManager.instance.GetRandomQuestionByCategory(targetCategory);

        targetQuestionData.ShowInConsole();

        targetQuestionCardScript.SetQuestionText(targetQuestionData.question);

        for (int index = 0; index < targetQuestionData.wrongAnswers.Count; index++)
        {
            OptionCard wrongOptionButton = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), this.optionContainer).GetComponent<OptionCard>();
            wrongOptionButton.SetOptionCardQuestion(targetQuestionData.wrongAnswers[index], false);
            wrongOptionButton.SetOptionCardCallback(GameManager.instance.OnWrongAnswer);

            this.optionButtonList.Add(wrongOptionButton.GetComponent<RectTransform>());
        }

        OptionCard correctOptionButton = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), this.optionContainer).GetComponent<OptionCard>();
        correctOptionButton.SetOptionCardQuestion(targetQuestionData.rightAnswer, true);
        correctOptionButton.SetOptionCardCallback(GameManager.instance.OnCorrectAnswer);

        this.optionButtonList.Add(correctOptionButton.GetComponent<RectTransform>());
    }

    #endregion


    //// UI components needed
    //[SerializeField] private RectTransform optionLayoutRect;

    //   // Button List
    //   List<GameObject> AnswerButtons = new List<GameObject>();

    //   // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //   // Construir los botones
    //   public void BuildButtons ()  // Sigo medio bolado con armar componentes de unity con prefabs, creo que no tiene bien determinada su posicion en el canvas por que no se que es RectTransform basicamente
    //{




    //	GameObject button1 = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), optionLayoutRect );
    //       button1.GetComponent<OptionCard>().isCorrect = true;
    //       GameObject button2 = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), optionLayoutRect );
    //       button2.GetComponent<OptionCard>().isCorrect = false;
    //       GameObject button3 = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), optionLayoutRect );
    //       button3.GetComponent<OptionCard>().isCorrect = false;
    //       GameObject button4 = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), optionLayoutRect );
    //       button4.GetComponent<OptionCard>().isCorrect = false;

    //       AnswerButtons.Add(button1);
    //       AnswerButtons.Add(button2);
    //       AnswerButtons.Add(button3);
    //       AnswerButtons.Add(button4);
    //   }

    //   public void LoadButtons(string ranswer, string wanswer1, string wanswer2, string wanswer3)
    //   {
    //       string[] answers = new string[4] { ranswer, wanswer1, wanswer2, wanswer3 };
    //       for (int i = 0; i < AnswerButtons.Count; i++)
    //       {
    //           AnswerButtons[i].text = answers[i];
    //       }

    //   }



    //   public void SortButtons ()
    //   {
    //       foreach (GameObject button in AnswerButtons)
    //       {
    //           Vector2 auxPosition = button.transform.position;
    //           int ranButton = Random.Range(0, AnswerButtons.Count);
    //           button.transform.position = AnswerButtons[ranButton].transform.position;
    //           AnswerButtons[ranButton].transform.position = auxPosition;
    //       }

    //   }
    //   public void EnableButtons()
    //   {
    //       foreach(GameObject button in AnswerButtons)
    //       {
    //           button.isEnabled = true;
    //       }
    //   }


    //   public void ResetButtons ()
    //   {

    //       foreach (GameObject button in AnswerButtons)
    //       {
    //           button.isEnabled = false;
    //           button.text = "";
    //       }
    //   }

    //   public void DisabledButtons ()
    //   {
    //       foreach (GameObject button in AnswerButtons)
    //       {
    //           button.isEnabled = false;
    //       }
    //   }






    //    // Comportamiento de los botones. 

    //    public void CheckChoice()
    //    {
    //    	if (true)
    //    	{
    //    		rightchoice();
    //    	} else
    //    	{
    //    		wrongchoice();
    //    	}
    //    }

    //   // Posibles resultados             
    //    public void rightchoice()
    //    {

    //    }

    //    public void wrongchoice()
    //    {

    //   }

    //    public void TimeOutResult()
    //    {

    //    }

}