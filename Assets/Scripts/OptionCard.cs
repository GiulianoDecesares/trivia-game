using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCard : MonoBehaviour
{
    [HideInInspector]
    public bool isCorrect;
    public Text optionText;
    
    public Button optionButton;
    public System.Action<bool> onButtonPressed;
    [SerializeField] private Animator showUpAnimator;

    // Use this for initialization
    public void SetOptionCardQuestion(string newOptionText, bool newIsCorrect)
    {
        isCorrect = newIsCorrect;
        optionText.text = newOptionText;
    }

    public void SetOptionCardCallback(UnityEngine.Events.UnityAction call)
    {
            this.GetComponent<Button>().onClick.AddListener(call);
    }

    void Start ()
    {
        this.GetComponent<Button>().onClick.AddListener(CheckAnswer);
        
    }    

    void CheckAnswer ()
    {
        if (isCorrect)
        {
            this.GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("CorrectOptionButton");  
            
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("WrongOptionButton");
        }
        if (this.onButtonPressed != null)
        {
            this.onButtonPressed(isCorrect);
        }
        TimeManager.instance.StopCountdown();
    }



}
