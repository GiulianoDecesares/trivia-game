﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour 
{
	[SerializeField] private InputField mailInputField;
	[SerializeField] private Button playButton;
    [SerializeField] private RectTransform checkSpritePlaceholder;
    [SerializeField] private GameObject mailInputStroke;

	private MailHelper mailHelper;

    private enum VisualIndications { MAIL_REJECTED, MAIL_ACCEPTED };

    private void Awake()
    {
        this.mailHelper = this.GetComponent<MailHelper>();
        this.playButton.interactable = false;
        this.mailInputStroke.gameObject.SetActive(false);

        this.playButton.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0f, -10f);
    }

    private void ShowVisualIndications(VisualIndications thisState)
    {
        this.mailInputStroke.gameObject.SetActive(true);

        switch(thisState)
        {
            case VisualIndications.MAIL_ACCEPTED:

                this.checkSpritePlaceholder.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("OkCheckMail");
                this.mailInputStroke.GetComponent<Image>().color = new Color(0f, 255f, 0f, 255f);
                this.mailInputField.GetComponentInChildren<Text>().color = new Color(0f, 255f, 0f, 255f);

                this.playButton.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0f, 15f);

                this.playButton.interactable = true;

                break;

            case VisualIndications.MAIL_REJECTED:

                this.checkSpritePlaceholder.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("XCheckMail");
                this.mailInputStroke.GetComponent<Image>().color = new Color(255f, 0f, 0f, 255f);
                this.mailInputField.GetComponentInChildren<Text>().color = new Color(255f, 0f, 0f, 255f);
                this.mailInputField.text = "Please enter a valid mail...";

                this.playButton.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0f, -10f);

                this.playButton.interactable = false;

                break;
        }
    }

    public void OnEndEdit()
    {
        if(!string.IsNullOrEmpty(this.mailInputField.text))
        {
            if(this.mailHelper.IsValidMail(this.mailInputField.text))
            {
                // Valid mail case

                // Show all the visual indications
                this.ShowVisualIndications(VisualIndications.MAIL_ACCEPTED);

                // Save the mails on google disabled for testing proposes
                // NOTE : this is working
                /// this.mailHelper.Send(this.mailInputField.text);
            }
            else
            {
                // Invalid mail case

                // Show all the visual indications
                this.ShowVisualIndications(VisualIndications.MAIL_REJECTED);
            }
        }
        else
        {
            // Empty mail input field case
            this.ShowVisualIndications(VisualIndications.MAIL_REJECTED);
        }
    }

    
}