using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCard : MonoBehaviour
{
    [HideInInspector]
    public bool isCorrect;
    public Text optionText;

    [SerializeField] private Animator showUpAnimator;

    private void Start()
    {
        this.showUpAnimator.SetTrigger("Launch");
    }

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
}
