using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestionCard : MonoBehaviour {
    public Text questionText;
    public Text categoryText;
    public Text counterText;
    public Slider timeSlider;
    private int seconds = 0;
    private Animator anim;


    // Use this for initialization
    void Start()
    {
        TimeManager.OnTick += Tick;
        anim = gameObject.GetComponent<Animator>();
    }

    void OnDisable()
    {
        TimeManager.OnTick -= Tick;
    }

	void Tick () {
        seconds++;
        timeSlider.normalizedValue = (seconds * 1f) / GameManager.instance.timeToAnswer;
	}

    public void LoadQuestionCard(string newQuestion, string newCategory)
    {
        questionText.text = newQuestion;
        categoryText.text = newCategory;
        timeSlider.normalizedValue = 0f;
        seconds = 0;
        counterText.text = GameManager.instance.answeredQuestions.ToString()+"/"+GameManager.instance.questionsAmmount.ToString();
        StartShowUpAnimation();
    }

    public void OnAnimationEnded()
    {
        TimeManager.instance.StartCountdown();
    }

    private void StartShowUpAnimation()
    {
        anim.SetTrigger("Launch");
    }
}
