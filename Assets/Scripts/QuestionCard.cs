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
    [SerializeField] private Image timeSliderFill;

    private int seconds = 0;
    private Animator anim;

    private bool isSubscribedToTime = false;

    [SerializeField] private Image headerSprite;
    [SerializeField] private Image categoryIcon;

    public QuestionManager.Categories category;

    #region Mono Bahaviour Methods
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        timeSlider.normalizedValue = 0f;
        seconds = GameManager.instance.timeToAnswer;
        counterText.text = GameManager.instance.answeredQuestions.ToString() + "/" + GameManager.instance.questionsAmmount.ToString();
    }

    void OnDisable()
    {
        if (this.isSubscribedToTime)
        {
            TimeManager.OnTick -= Tick; 
        }
    }

    #endregion

    #region Private Methods

    private void Tick () {
        seconds--;
        float sliderPosition = (seconds * 1f) / GameManager.instance.timeToAnswer;
        timeSlider.normalizedValue = sliderPosition;
        Colorize(sliderPosition);
        
	}
    private void Colorize(float value)
    {
        if (value < 0.5f)
        {
            timeSliderFill.color = Color.yellow * value * 2f + Color.red * (1 - value * 2f);
        }
        else
        {
            timeSliderFill.color = Color.green * (value - 0.5f) * 2 + Color.yellow * (1 - (value - 0.5f) * 2);
        }
        
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
        this.isSubscribedToTime = true;

        TimeManager.OnTick += Tick;
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
