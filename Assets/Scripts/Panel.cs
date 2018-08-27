using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    #region Publig Fields

    public enum PanelType { LOGIN, PLAY, RESULT };

    public PanelType panelType;

    #endregion


    #region Public Methods

    public int GetSibilingIndex()
    {
        return this.GetComponent<RectTransform>().GetSiblingIndex();
    }

    #endregion
}
