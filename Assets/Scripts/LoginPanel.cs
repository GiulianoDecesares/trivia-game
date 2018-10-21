using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour 
{
	[SerializeField] private InputField mailInputField;
    [SerializeField] private GameObject fieldText;

    [SerializeField] private Text mailText;
    [SerializeField] public Button playButton;
    [SerializeField] private RectTransform checkSpritePlaceholder;
    [SerializeField] private GameObject checkSpriteGameObject;
    [SerializeField] private GameObject mailInputStroke;

    [SerializeField] private GameObject welcomeText;

    [SerializeField] private bool welcomeMode;

	private MailHelper mailHelper;

    private enum VisualIndications { MAIL_REJECTED, MAIL_ACCEPTED, MAIL_EMPTY };


    #region Public Methods
    //Resets the login panel to it's starting state

    
    public void ResetLoginPanel()
    {
        this.checkSpriteGameObject.SetActive(false);

        this.mailHelper = this.GetComponent<MailHelper>();
        this.playButton.interactable = false;
        this.mailInputStroke.gameObject.SetActive(false);
        this.mailInputField.text = "";

        this.checkSpritePlaceholder.GetComponent<Image>().sprite = null;
        this.mailInputStroke.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        this.mailInputField.GetComponentInChildren<Text>().color = new Color(50f/255f, 50f / 255f, 50f / 255f, 128f / 255f);
        this.mailInputField.GetComponentInChildren<Text>().text = "exampleemail@gmail.com";
        mailText.color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 180f / 255f);


        if(this.welcomeMode)
        {
            this.playButton.interactable = true;

            this.welcomeText.SetActive(true);

            this.mailInputField.gameObject.SetActive(false);
            this.fieldText.SetActive(false);
        }
        else
        {
            this.playButton.interactable = false;

            this.welcomeText.SetActive(false);

            this.mailInputField.gameObject.SetActive(true);
            this.fieldText.SetActive(true);
        }

    }

    public void OnEndEdit()
    {
        if (!string.IsNullOrEmpty(this.mailInputField.text))
        {
            if (this.mailHelper.IsValidMail(this.mailInputField.text))
            {
                // Valid mail case

                // Show all the visual indications
                this.ShowVisualIndications(VisualIndications.MAIL_ACCEPTED);

                // Save the mails on google disabled for testing proposes

#if !UNITY_EDITOR
                this.mailHelper.Send(this.mailInputField.text); 
#endif
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
            this.ShowVisualIndications(VisualIndications.MAIL_EMPTY);
        }
    }
    #endregion

    #region Private Methods
    private void ShowVisualIndications(VisualIndications thisState)
    {
        this.mailInputStroke.gameObject.SetActive(true);

        switch (thisState)
        {
            case VisualIndications.MAIL_ACCEPTED:

                this.checkSpriteGameObject.SetActive(true);

                this.checkSpritePlaceholder.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("OkCheckMail");
                this.mailInputStroke.GetComponent<Image>().color = new Color(0f, 255f, 0f, 255f);
                // this.mailInputField.GetComponentInChildren<Text>().color = new Color(0f, 255f, 0f, 255f);
                mailText.color = new Color(0f, 1f, 0f, 1f);
                this.playButton.interactable = true;

                break;

            case VisualIndications.MAIL_REJECTED:

                this.checkSpriteGameObject.SetActive(true);

                this.checkSpritePlaceholder.GetComponent<Image>().sprite = SpriteManager.instance.GetSpriteByName("XCheckMail");
                this.mailInputStroke.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
                this.mailInputField.GetComponentInChildren<Text>().color = new Color(255f, 0f, 0f, 255f);
                this.mailInputField.text = "";
                this.mailInputField.GetComponentInChildren<Text>().text = "Ingrese un mail válido...";

                this.playButton.interactable = false;

                break;

            case VisualIndications.MAIL_EMPTY:
                ResetLoginPanel();

                break;
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        ResetLoginPanel();
    }
    private void Start()
    {
      AudioManager.instance.PlayMainMenuTheme();
    }
    private void OnDisable()
    {
        AudioManager.instance.StopMainMenuTheme();
    }
    #endregion
}