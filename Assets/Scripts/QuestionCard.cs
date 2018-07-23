using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCard : MonoBehaviour 
{
	[SerializeField] private Text title;
	[SerializeField] private Text scoreCounter;
	[SerializeField] private Slider timeBar;
	[SerializeField] private GameObject cathegoryIconGameObject;
	[SerializeField] private GameObject textContentGameObject;
	[SerializeField] private Image headerSprite;
	public Text Title
	{
		set
		{
			this.title = value;
		}

		get
		{
			return this.title;
		}
	}

	public Text ScoreCounter
	{
		set
		{
			this.scoreCounter = value;
		}

		get
		{
			return this.scoreCounter;
		}
	}

	public void SetTimeBarValue(float thisValue)
	{
		this.timeBar.value = thisValue;
	}

	public float GetTimeBarValue()
	{
		return this.timeBar.value;
	}

	public void SetCathegoryIcon(Sprite thisIcon)
	{
		if(thisIcon != null)
		{
			this.cathegoryIconGameObject.GetComponent<Image>().sprite = thisIcon;
		}
		else
		{
			Debug.LogError("Error :: cathegory icon reference is null", this.gameObject);
		}
	}

	public void ShowCathegoryIcon()
	{
		if(!this.cathegoryIconGameObject.activeInHierarchy)
		{
			this.cathegoryIconGameObject.SetActive(true);
		}
	}

	public void HideCathegoryIcon()
	{
		if(this.cathegoryIconGameObject.activeInHierarchy)
		{
			this.cathegoryIconGameObject.SetActive(false);
		}
	}

	public void ShowTextContent()
	{
		if(!this.textContentGameObject.activeInHierarchy)
		{
			this.textContentGameObject.SetActive(true);
		}
	}
	public void HideTextContent()
	{
		if(this.textContentGameObject.activeInHierarchy)
		{
			this.textContentGameObject.SetActive(false);
		}
	}

	public void SetTextInTextContainer(string thisText)
	{
		this.textContentGameObject.GetComponent<Text>().text = thisText;
	}

	public void SetHeaderSprite(Sprite thisSprite)
	{
		this.headerSprite.sprite = thisSprite;
	}
}
