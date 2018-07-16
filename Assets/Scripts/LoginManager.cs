using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoginManager : MonoBehaviour {
    public InputField MailInput;
    public Button buttontoplay;
    public Image ValidMailImage;
    public Image greenborder;

    private string gameDataProjectFilePath = "/Emails.txt";
    private string AllEmails = "";

    void Start () {
        buttontoplay.interactable = false;
        ValidMailImage.gameObject.SetActive(false);
        greenborder.gameObject.SetActive(false);
        LoadMails();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void checkmailifvalid()
    {
        if (MailInput.text.EndsWith(".com") && (MailInput.text.Contains("@")) && !(MailInput.text.Contains("exampleemail@gmailcom")))
            {                
                this.ValidMailImage.gameObject.SetActive(true);
                this.buttontoplay.interactable = true;
                this.greenborder.gameObject.SetActive(true);                
            }
            else
            {
                ValidMailImage.gameObject.SetActive(false);
                buttontoplay.interactable = false;
            }
    }   

    public void ButtonBehaviour()
    {

    }

    //Internal Use - To load the Mails saved in Emails.txt    
    private void LoadMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            AllEmails = File.ReadAllText(filePath);
        }
        else
        {
            File.Create(filePath);
            AllEmails = "";
        }
    }

    //This is used by the "Play Now" button in the main screen to save the e-mail address to a file.
    public void EmailInput()
    {
        if (AllEmails == "")
        {
            AllEmails = MailInput.text;
        }
        else
        {
            AllEmails = AllEmails + ";" + MailInput.text;
        }
        SaveMails();
    }

    //Internal Use - To save a new e-mail address in the file Emails.txt   
    private void SaveMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, AllEmails);
        MailInput.text = "";
    }
}
