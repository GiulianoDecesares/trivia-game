using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCard : MonoBehaviour {
    [HideInInspector]
    public bool isCorrect;
    public Text optionText;

	// Use this for initialization
	public void LoadOptionCard (string newOptionText, bool newIsCorrect)
    {
        isCorrect = newIsCorrect;
        optionText.text = newOptionText;
	}
	
}
