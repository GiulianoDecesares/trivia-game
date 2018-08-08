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
}