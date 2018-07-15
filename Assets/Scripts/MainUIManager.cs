﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour 
{
	[SerializeField] private GameObject panelsScrollRect;

#region Sigleton
    public static MainUIManager instance;
    private void Awake()
    {
        instance = this;
    }

#endregion

	public void InvokeAndAddPanel(GameObject thisPanel)
	{
		GameObject panelInstance = Instantiate(thisPanel, new Vector3(), new Quaternion(), this.panelsScrollRect.gameObject.transform);
		panelInstance.transform.position = new Vector3(0,0,0);
	}

}
