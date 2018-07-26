using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoginManager : MonoBehaviour {
    public InputField mailInput;
    
    public Button buttontoplay;
    public Image chekMailImage;
    public Image border;

    private string _fileName;
    public string AllEmails = "";

    void Start () {
        _fileName = Application.persistentDataPath + "/Emails.txt";
        LoadMails();
        buttontoplay.interactable = false;
        border.gameObject.SetActive(false);
        
    }
	
    public void CheckMailIfValid()
    {
        if (mailInput.text.EndsWith(".com") && (mailInput.text.Contains("@")) && !(mailInput.text.Contains("exampleemail@gmailcom")))
            {                
                this.buttontoplay.interactable = true;
                this.chekMailImage.sprite = SpriteManager.instance.GetSpriteByName("ok_button_login");
                this.border.gameObject.SetActive(true);
                this.border.color = new Color(0f, 1f, 0f, 1f); // Set border color to green
            }
            else
            {
                buttontoplay.interactable = false;
                this.chekMailImage.sprite = SpriteManager.instance.GetSpriteByName("x_button_login");
                this.border.gameObject.SetActive(true);
                this.border.color = new Color(1f, 0f, 0f, 1f); // Set border color to red
            }
    }   

    //Internal Use - To load the Mails saved in Emails.txt    
    private void LoadMails()
    {
        if (File.Exists(_fileName))
        {
            AllEmails = File.ReadAllText(_fileName);
        }
        else
        {
            File.Create(_fileName);
            AllEmails = "";
        }
    }

    //This is used by the "Play Now" button in the main screen to save the e-mail address to a file.
    public void EmailInput()
    {
        if (AllEmails == "")
        {
            AllEmails = mailInput.text;
        }
        else
        {
            AllEmails = AllEmails + ";" + mailInput.text;
        }
        SaveMails();
    }

    //Internal Use - To save a new e-mail address in the file Emails.txt   
    private void SaveMails()
    {
        File.WriteAllText(_fileName, AllEmails);
        mailInput.text = "";
    }
}
