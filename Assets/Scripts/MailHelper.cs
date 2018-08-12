using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailHelper : MonoBehaviour 
{
	[SerializeField] private string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSd38IZy7YnX7psf26MrJuC3UOlM1uheLpbNg5q6G9RPmwqKIQ/formResponse";

	private IEnumerator Post(string thisMailString)
	{
		WWWForm form = new WWWForm();

		form.AddField("entry.810188429", thisMailString);

		byte[] rawData = form.data;

		WWW www = new WWW(BASE_URL, rawData);

		yield return www;
	}

	public void Send(string thisMail)
	{
		StartCoroutine(Post(thisMail));
	}

	public bool IsValidMail(string thisMail)
	{
		bool resultState = false;

		if((thisMail.EndsWith(".com")|| thisMail.EndsWith(".com.ar"))&& (thisMail.Contains("@"))&& !(thisMail.Contains("exampleemail@gmail.com"))&&(thisMail.Length>8))
		{
			resultState = true;
		}

		return resultState;
	}
}