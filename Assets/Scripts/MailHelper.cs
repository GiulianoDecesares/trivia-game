using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class MailConfig
{
    public string from;
    public string password;
    
    public string subject;
    public string body;

    public string serverHost;

    public bool useDefaultCredentials;
    public bool useSsl;
    public int port;

    public MailConfig(MailConfig thisConfig = null)
    {
        if(thisConfig != null)
        {
            this.from = thisConfig.from;
            this.password = thisConfig.password;

            this.subject = thisConfig.subject;
            this.body = thisConfig.body;

            this.serverHost = thisConfig.serverHost;

            this.useDefaultCredentials = thisConfig.useDefaultCredentials;
            this.useSsl = thisConfig.useSsl;
            this.port = thisConfig.port; 
        }
    }
}

public class MailHelper : MonoBehaviour 
{
	private readonly string baseURL = "https://docs.google.com/forms/d/e/1FAIpQLSd38IZy7YnX7psf26MrJuC3UOlM1uheLpbNg5q6G9RPmwqKIQ/formResponse";

	private IEnumerator Post(string thisMailString)
	{
		WWWForm form = new WWWForm();

		form.AddField("entry.810188429", thisMailString);

		byte[] rawData = form.data;

		WWW www = new WWW(baseURL, rawData);

		yield return www;
	}

    private MailConfig LoadConfig(string configFileName)
    {
        MailConfig result = null;

        TextAsset configText = Resources.Load("Config/" + configFileName) as TextAsset;

        if(configText)
        {
            result = new MailConfig();

            string configContent = configText.ToString();
            result = JsonUtility.FromJson<MailConfig>(configContent);
        }
        else
        {
            Debug.LogError("File does not exists in current path");
        }

        return result;
    }

	public void PostMail(string thisMail)
	{
		StartCoroutine(Post(thisMail));
	}

    public void TrySendMail(string to)
    {
        MailConfig config = this.LoadConfig("mailConfig");

        if(config != null)
        {
            // Mail message new object creation
            System.Net.Mail.MailMessage MailMessage = new System.Net.Mail.MailMessage();

            // Destinatary 
            MailMessage.To.Add(to);

            // NOTE :: To property is a list of destinataries

            // Mail subject
            MailMessage.Subject = config.subject;
            MailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            // CC (optional)
            // MailMessage.Bcc.Add("destinatariocopia@servidordominio.com"); 

            // Message body
            MailMessage.Body = config.body;
            MailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            MailMessage.IsBodyHtml = false; // No HTML encoding

            // From data
            MailMessage.From = new System.Net.Mail.MailAddress(config.from);

            // Client section with SMTP 
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            // Client credentials
            client.Credentials =
                new System.Net.NetworkCredential(config.from, config.password);

            // Gmail server data
            client.Port = config.port;
            client.EnableSsl = config.useSsl;
            client.UseDefaultCredentials = config.useDefaultCredentials;

            // Magic lambda method line to get this thing going
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;

            client.Host = config.serverHost;

            try
            {
                // Message send
                client.Send(MailMessage);
            }
            catch(System.Net.Mail.SmtpException exception)
            {
                Debug.Log(exception);
            }
            finally
            {
                client.Dispose();
            }
        }
        else
        {
            Debug.LogWarning("Config was null. Skipping mail sending.");
        }
    }

    public bool IsValidMail(string thisMail)
	{
		bool resultState = false;

#if !UNITY_EDITOR

        if((thisMail.EndsWith(".com") 
            || thisMail.Contains(".com.")) 
            && (thisMail.Contains("@")) 
            && !(thisMail.Contains("exampleemail@gmail.com")) 
            && (thisMail.Length>8)&& (!thisMail.Contains("@."))
            && (!thisMail.StartsWith("@"))
            && (!thisMail.StartsWith(".")) 
            && (!thisMail.EndsWith(".")) 
            && (!thisMail.EndsWith("@")))
        {
			resultState = true;
		}

#else
        resultState = true;
#endif

        return resultState;
	}
}