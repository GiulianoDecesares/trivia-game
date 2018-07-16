using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour 
{

#region Sigleton

    public static DeviceManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

#endregion

	public bool isPhone
	{
		get
		{
			// Calculate the aspect ratio 
			
			float screenWidth = Screen.width / Screen.dpi;
     		float screenHeight = Screen.height / Screen.dpi;
     		float aspectRatio = screenWidth / screenHeight;

			return aspectRatio == (9f / 16f);
		}

		private set {}
	}

	public bool isTablet
	{
		get
		{
			// Calculate the aspect ratio 

			float screenWidth = Screen.width / Screen.dpi;
     		float screenHeight = Screen.height / Screen.dpi;
     		float aspectRatio = screenWidth / screenHeight;

			return aspectRatio == (3f / 4f);
		}

		private set {}
	}
}
