using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManager : MonoBehaviour 
{
	[SerializeField] private RectTransform optionsLayoutRect;
	[SerializeField] private RectTransform questionsLayoutRect;

	// Use this for initialization
	private void Start () 
	{
		this.SetUpOptions();
	}

	private void SetUpOptions()
	{
		for(int index = 0; index < 4; index++)
		{
			Instantiate(PrefabManager.instance.GetPrefabByName("OptionCard"), this.optionsLayoutRect);
		}
	}
}
