using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Insert Your Email InputField Here")]
    public InputField Email;
    [Space]
    [Header("This is the actual Score")]
    public int Score=0;
    [Space]
    [Header("Index of the actual category")]
    public int CurrentCategoryIndex = 0;

    private string gameDataProjectFilePath = "/Emails.txt";
    private string AllEmails = "";
    #region Sigleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void Start()
    {
        LoadMails();
    } 
//This is called to randomly select a category index between 0 and the total ammount of categories that should be entered in TotalCategories
    public void CategoryRandomSelection (int TotalCategories) {
        int NewCategory = Random.Range(0, TotalCategories);
        CurrentCategoryIndex = NewCategory;
	}

//This is used by the "Play Now" button in the main screen to save the e-mail address to a file.
    public void EmailInput()
    {
        if (AllEmails == "")
        {
            AllEmails = Email.text;
        }
        else
        {
            AllEmails = AllEmails + ";" + Email.text;
        }
        SaveMails();
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
    //Internal Use - To save a new e-mail address in the file Emails.txt   
    private void SaveMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, AllEmails); 
    }

    //Called by the Results Script to reset the values and start over.
    public void ResetGame () {
        Score = 0;
        Email.text = "";
	}
}
