using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    [SerializeField] private ScrollRect mainScroll;
    [SerializeField] private RectTransform center;

    [SerializeField] private List<Panel> allPanelsList;

    public float scrollSpeed;

    public Panel.PanelType currentPanel = Panel.PanelType.RESULT;

    public System.Action onScrollEnd;

    private void Start()
    {
        this.mainScroll.normalizedPosition = new Vector2(0.98f, 0f);

        StartCoroutine(SwipeTo(Panel.PanelType.LOGIN));
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

    private IEnumerator SwipeForoward(float normalizedScrollTo)
    {
        while(this.mainScroll.normalizedPosition.x < normalizedScrollTo)
        {
            Debug.Log("Main scroll position " + this.mainScroll.normalizedPosition);
            Debug.Log("Normalized scrool to " + normalizedScrollTo);

            mainScroll.normalizedPosition += new Vector2(scrollSpeed, 0) * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator SwipeBack(float scrolltoSwipe)
    {
        while(this.mainScroll.normalizedPosition.x >= (1 - scrolltoSwipe))
        {
            Debug.Log("Main scroll position " + this.mainScroll.normalizedPosition);
            Debug.Log("Normalized scrool to " + scrolltoSwipe);

            mainScroll.normalizedPosition -= new Vector2(scrollSpeed, 0) * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator SwipeTo(Panel.PanelType thisPanel)
    {
        Panel targetPanel = this.SearchForPanel(thisPanel);
        Debug.Log("Target panel type is " + targetPanel.panelType);

        Panel currentPanel = this.SearchForPanel(this.currentPanel);
        Debug.Log("Current panel type is " + currentPanel.panelType);

        if(targetPanel)
        {
            int sibilingIndex = targetPanel.GetSibilingIndex();
            Debug.Log("Target panel sibiling index : " + sibilingIndex);

            int currentSibilingIndex = currentPanel.GetSibilingIndex();
            Debug.Log("Current panel sibiling index : " + currentSibilingIndex);

            float normalizedScrollTo = (1 / this.allPanelsList.Count) * sibilingIndex + 1;
            Debug.Log("Normalized scroll to : " + normalizedScrollTo);

            if(sibilingIndex < currentSibilingIndex)
            {
                Debug.Log("Must swipe back");
                StartCoroutine(this.SwipeBack(normalizedScrollTo));
            }
            else
            {
                Debug.Log("Must swipe foroward");
                StartCoroutine(this.SwipeForoward(normalizedScrollTo));
            }


            this.currentPanel = thisPanel;
        }

        this.onScrollEnd?.Invoke();

        yield return null;
    }

    public void AnimatedSwipeTo(Panel.PanelType thisPanel)
    {
        StartCoroutine(this.SwipeTo(thisPanel));
    }
}
