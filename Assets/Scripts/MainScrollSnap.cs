using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScrollSnap : MonoBehaviour {

	public ScrollRect scrollRect;
	public float AnimDelay = 0.2f;
	private int ScreenState = 0;

	//public delegate void PlayButtonPressed();
    //public static event PlayButtonPressed onPlayButtonPressed;
	
	public void scrollBehaviour(int number) 
	{
		StartCoroutine(ScreenAnimation(number));
	}

	IEnumerator ScreenAnimation(int NewScreenIndex)
	{
		if (NewScreenIndex != ScreenState)
		{
			float Increment = (NewScreenIndex - ScreenState) / 40f;

			for (int idx = 0; idx <= 20; idx++)
			{
				scrollRect.horizontalNormalizedPosition += Increment;
				yield return new WaitForSeconds(AnimDelay / 20f);
			}

			ScreenState = NewScreenIndex;
		}

		yield return null;
	}
}

//scripts for each button behaviour ideas ...

// void lastAnswerButtonBehaviour() {
// 		if (lastAnswerButtonPressed != null) 
// 		{
//			scrollBehaviour ();
// 		}
// 	}

// void replayButtonBehaviour(){
// 		if (replayButtonPressed != null) 
// 		{
//			scrollBehaviour ();
// 		}
// 	}