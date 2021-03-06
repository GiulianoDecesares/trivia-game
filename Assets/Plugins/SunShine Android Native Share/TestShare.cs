﻿using System.Collections;
using UnityEngine;

public class TestShare : MonoBehaviour
{
    public string ShareMessage = "¡Mira cuanto sé sobre Mar del Plata!";
    public void takeScreenShotAndShare()
    {
        StartCoroutine(takeScreenshotAndSave());
    }



    private IEnumerator takeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();
        string img_name = "ScreenShot.png";
        string destination_path = Application.persistentDataPath + "/" + img_name; ;
        System.IO.File.WriteAllBytes(destination_path, imageBytes);

        //Call Share Function
        shareScreenshot(destination_path);
    }


    private void shareScreenshot(string path)
    {
        SunShineNativeShare.ShareSingleFile(path, SunShineNativeShare.TYPE_IMAGE, ShareMessage, "Share By sunshine");
    }

    public void ShareText()
    {
        SunShineNativeShare.ShareText(ShareMessage, "Share By sunshine");
    }






}
