using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {
    public InputField MailInput;
    public Button buttontoplay;
    public Image ValidMailImage;
    // public Image greenborder;

    // Use this for initialization
    void Start () {
        buttontoplay.interactable = false;
        ValidMailImage.gameObject.SetActive(false);
       // greenborder.gameObject.SetActive(false);
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

               // this.greenborder.gameObject.SetActive(true);

                
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
}
