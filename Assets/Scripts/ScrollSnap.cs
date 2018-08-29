using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    [SerializeField] private ScrollRect mainScroll;

    [SerializeField] private List<Panel> allPanelsList;

    [SerializeField] private AnimationCurve scrollAcceleration;

    public float scrollSpeed;

    public Panel.PanelType currentPanel = Panel.PanelType.LOGIN;

    public System.Action onScrollEnd;

    private void Start()
    {
        this.mainScroll.normalizedPosition = new Vector2(0f, 0f);
    }

    private Panel SearchForPanel(Panel.PanelType thisPanel)
    {
        Panel result = null;

        foreach(Panel panel in this.allPanelsList)
        {
            if(panel.panelType == thisPanel)
            {
                result = panel;
                break;
            }
        }

        if(result == null)
        {
            Debug.LogError("Null panel returned");
        }

        return result;
    }

    private IEnumerator SwipeForoward(float scrollToSwipe)
    {
        /*
        normalizedScrollTo ----------------------------- 1
        actual scroll ---------------------------------- x 

        ergo -> (actual scroll * 1f) / normalized scroll to
        */

        while(this.mainScroll.normalizedPosition.x < scrollToSwipe)
        {
            mainScroll.normalizedPosition += new Vector2(0.0001f, 0);

            float actualNormalizedScroll = this.mainScroll.normalizedPosition.x / scrollToSwipe;

            Debug.Log(this.scrollAcceleration.Evaluate(actualNormalizedScroll / scrollToSwipe));

            mainScroll.normalizedPosition += new Vector2(scrollSpeed, 0) 
                * this.scrollAcceleration.Evaluate(actualNormalizedScroll / scrollToSwipe) 
                * Time.deltaTime;

            yield return null;
        }

        this.onScrollEnd?.Invoke();
    }

    private IEnumerator SwipeBack(float scrollToSwipe)
    {
        while(this.mainScroll.normalizedPosition.x > scrollToSwipe)
        {
            mainScroll.normalizedPosition -= new Vector2(0.0001f, 0);

            float actualNormalizedScroll = this.mainScroll.normalizedPosition.x / scrollToSwipe;

            Debug.Log(this.scrollAcceleration.Evaluate(actualNormalizedScroll / scrollToSwipe));

            mainScroll.normalizedPosition -= new Vector2(scrollSpeed, 0)
                * this.scrollAcceleration.Evaluate(actualNormalizedScroll / scrollToSwipe)
                * Time.deltaTime;

            yield return null;
        }

        this.onScrollEnd?.Invoke();
    }

    private IEnumerator SwipeTo(Panel.PanelType thisPanel)
    {
        Panel targetPanel = this.SearchForPanel(thisPanel);

        Panel currentPanel = this.SearchForPanel(this.currentPanel);

        if(targetPanel != null)
        {
            int sibilingIndex = targetPanel.GetSibilingIndex();

            int currentSibilingIndex = currentPanel.GetSibilingIndex();

            float normalizedScrollTo = 0f;

            if(thisPanel == this.allPanelsList[this.allPanelsList.Count - 1].panelType)
                normalizedScrollTo = ((1f / this.allPanelsList.Count) * (sibilingIndex + 1f));
            else if(thisPanel == this.allPanelsList[0].panelType)
                normalizedScrollTo = ((1f / this.allPanelsList.Count) * (sibilingIndex));
            else
                normalizedScrollTo = ((1f / this.allPanelsList.Count) * (sibilingIndex + 0.5f));

            if(sibilingIndex < currentSibilingIndex)
            {
                StartCoroutine(this.SwipeBack(normalizedScrollTo));
            }
            else
            {
                StartCoroutine(this.SwipeForoward(normalizedScrollTo));
            }

            if(normalizedScrollTo > 0)
            {
                this.currentPanel = thisPanel; 
            }
        }
        else
        {
            Debug.LogError("No target panel");
        }

        yield return null;
    }

    public void AnimatedSwipeTo(Panel.PanelType thisPanel)
    {
        StartCoroutine(this.SwipeTo(thisPanel));
    }
}
