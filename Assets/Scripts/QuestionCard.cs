using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestionCard : MonoBehaviour
{
    [SerializeField] private Text questionText;
    [SerializeField] private Text categoryText;
    [SerializeField] private Text counterText;
    [SerializeField] private Slider timeSlider;

    private int seconds = 0;
    private Animator anim;

    [SerializeField] private Image headerSprite;
    [SerializeField] private Image categoryIcon;

    public QuestionManager.Categories category;

    #region Mono Bahaviour Methods
    
    void Start()
    {
        TimeManager.OnTick += Tick;

        anim = gameObject.GetComponent<Animator>();
        timeSlider.normalizedValue = 0f;
        seconds = 0;
        counterText.text = GameManager.instance.answeredQuestions.ToString() + "/" + GameManager.instance.questionsAmmount.ToString();
    }

    void OnDisable()
    {
        TimeManager.OnTick -= Tick;
    }

    #endregion

    #region Private Methods

    private void Tick () {
        seconds++;
        timeSlider.normalizedValue = (seconds * 1f) / GameManager.instance.timeToAnswer;
	}

    #endregion

    #region Public Methods

    public void SetQuestionText(string newQuestion)
    {
        questionText.text = newQuestion;
    }

    public void SetTitleText(string thisTitle)
    {
        this.categoryText.text = thisTitle;
    }

    public void OnAnimationEnded()
    {
        TimeManager.instance.StartCountdown();
    }

    public void StartShowUpAnimation()
    {
        anim.SetTrigger("Launch");
    }

    public void SetHeaderSprite(Sprite thisSprite)
    {
        if(thisSprite)
        {
            this.headerSprite.sprite = thisSprite;
        }
    }

    public void SetCategoryIcon(Sprite thisSprite)
    {
        if (thisSprite)
        {
            this.categoryIcon.sprite = thisSprite;
        }
    }

    #endregion
}
