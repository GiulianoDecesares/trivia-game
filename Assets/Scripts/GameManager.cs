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

    private string gameDataProjectFilePath = "/Emails.txt";
    private string AllEmails = "";

    public void Start()
    {
        LoadMails();
    } 

    // Use this for initialization
    public int CategoryRandomSelection (int TotalCategories) {
        int NewCategory = Random.Range(0, TotalCategories);
        return NewCategory;
	}

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

    private void SaveMails()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, AllEmails);
        
    }

    // Update is called once per frame
    public void ResetGame () {
        Score = 0;
	}
}
