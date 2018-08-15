using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    #region Private Accessors

    private List<RectTransform> optionButtonList = new List<RectTransform>();
    private List<OptionCard> gameObjectButtonList = new List<OptionCard>();
    QuestionCard targetQuestionCardScript;
    #endregion

    #region Public Accessors

    public Carousel carouselBehaviour;

    public RectTransform optionContainer;

    
    // asociar timemanager
    #endregion

    #region Public Methods

    private void OnEnable()
    {
        this.carouselBehaviour.onSwipeFinished += this.StartAnimation;
       TimeManager.OnTimeOut += this.AfterTimeOut;
    }

    private void OnDisable()
    {
        this.carouselBehaviour.onSwipeFinished -= this.StartAnimation;
        TimeManager.OnTimeOut -= this.AfterTimeOut;
    }

    public void StartPlayFlow()
    {
        if(this.optionButtonList.Count > 0)
        {
            for (int index = 0; index < this.optionButtonList.Count; index++)
            {
                gameObjectButtonList[index].onButtonPressed -= AfterOptionIsPressed;
                Destroy(this.optionButtonList[index].gameObject);
            }

            this.optionButtonList.Clear();
            this.gameObjectButtonList.Clear();
        }

        QuestionManager.Categories targetCategory = QuestionManager.instance.GetRandomCategory();
        targetQuestionCardScript = carouselBehaviour.StartSwipeToCategory(targetCategory).GetComponent<QuestionCard>();
        QuestionAndAnswers targetQuestionData = QuestionManager.instance.GetRandomQuestionByCategory(targetCategory);

        targetQuestionCardScript.SetQuestionText(targetQuestionData.question);

        int RandomButton = Random.Range(0,4);
        int wrongAnswerIndex = 0;
        for (int index = 0; index < 4; index++)
        {
            
            if (index == RandomButton)
            {
                OptionCard correctOptionButton = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), this.optionContainer).GetComponent<OptionCard>();
                correctOptionButton.SetOptionCardQuestion(targetQuestionData.rightAnswer, true);
        
                this.optionButtonList.Add(correctOptionButton.GetComponent<RectTransform>());
                this.gameObjectButtonList.Add(correctOptionButton);

                correctOptionButton.onButtonPressed += this.AfterOptionIsPressed;
        
            }
            else 
            {
                OptionCard wrongOptionButton = Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), this.optionContainer).GetComponent<OptionCard>();
                wrongOptionButton.SetOptionCardQuestion(targetQuestionData.wrongAnswers[wrongAnswerIndex], false);
            
                this.optionButtonList.Add(wrongOptionButton.GetComponent<RectTransform>());
                this.gameObjectButtonList.Add(wrongOptionButton);

                wrongOptionButton.onButtonPressed += this.AfterOptionIsPressed;
                wrongAnswerIndex++;
            }
            
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(this.OnOptionButtonAnimate());
    }

    public void AfterOptionIsPressed(bool boolofbutton)
    {
        for (int ind = 0; ind < this.gameObjectButtonList.Count; ind++)
        {
            if ((gameObjectButtonList[ind].isCorrect) && (gameObjectButtonList[ind].optionButton.interactable == true))
            {
                gameObjectButtonList[ind].optionButton.interactable = false;
                gameObjectButtonList[ind].GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("CorrectOptionButton");    
            } 
            else 
            {
                if ((gameObjectButtonList[ind].isCorrect == false) && (gameObjectButtonList[ind].optionButton.interactable == true))
                {
                gameObjectButtonList[ind].optionButton.interactable = false;
                gameObjectButtonList[ind].GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("DisabledButton");
                }
            }
        }
        StartCoroutine(this.OnOptionButtonPressed(boolofbutton));
    }

    public void AfterTimeOut()
    {
        StartCoroutine(this.OnTimeOut());
    }
    private IEnumerator OnOptionButtonAnimate()
    {
        foreach(RectTransform optionCard in this.optionButtonList)
        {
            optionCard.GetComponent<Animator>().SetTrigger("Launch");
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator OnOptionButtonPressed(bool PressedButton)
    {   

        if (PressedButton)
        {
            targetQuestionCardScript.SetQuestionText("Respuesta correcta");    
            yield return new WaitForSeconds(1.5f);
            GameManager.instance.OnCorrectAnswer();
        }
        else
        { 
            targetQuestionCardScript.SetQuestionText("Respuesta incorrecta");    
            yield return new WaitForSeconds(1.5f);
            GameManager.instance.OnWrongAnswer();
        }

    }
    private IEnumerator OnTimeOut()
    {
        for (int index = 0;index < this.gameObjectButtonList.Count;index++)
        {
            if ((gameObjectButtonList[index].isCorrect) && (gameObjectButtonList[index].optionButton.interactable = true))
            {
                gameObjectButtonList[index].optionButton.interactable = false;
                gameObjectButtonList[index].GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("CorrectOptionButton");    
            } 
            else if (!(gameObjectButtonList[index].isCorrect) && (gameObjectButtonList[index].optionButton.interactable = true))
            {
                gameObjectButtonList[index].optionButton.interactable = false;
                gameObjectButtonList[index].GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("DisabledButton");
            }

        }
        targetQuestionCardScript.SetQuestionText("Te quedaste sin tiempo");
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.OnWrongAnswer();

    }
    

    #endregion
}