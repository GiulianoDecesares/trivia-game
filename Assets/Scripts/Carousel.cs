using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Carousel : MonoBehaviour
{

#region Private fields and accessors

    [SerializeField] private RectTransform[] categoryCardsList;
    [SerializeField] private RectTransform centerPlaceholderRect;
    [SerializeField] private RectTransform contentRect;

    [SerializeField] private int intercardSpacing;

    [SerializeField] private float reelSpeed;

    [SerializeField] private AnimationCurve accelerationCurve;
    
    private Vector2 intercardSpacingVector;
    
#endregion

#region Private methods

    private void SetUpCards()
    {
        for(int index = 0; index < this.categoryCardsList.Length; index++)
        {
            // Build spacing vector using parameter from inspector
            Vector2 spacing = new Vector2(this.intercardSpacing, 0f);

            // Calculate offset taking the half width of a question card
            Vector2 offset = new Vector2((float)(categoryCardsList[index].rect.width), 0f);

            // Multiply position by index a.k.a items amount in content
            Vector2 newPosition = (offset + spacing) * index;

            // Set new position
            this.categoryCardsList[index].GetComponent<RectTransform>().anchoredPosition = newPosition;
        }
    }

    private bool CardIsOutOfView(RectTransform thisCard)
    {
        bool resultState = false;

        float maxDistanceAllowed = (thisCard.rect.width / 2) + (this.contentRect.rect.width / 2);
        
        float totalDistanceFromCardToCenter = Vector2.Distance(thisCard.anchoredPosition, this.centerPlaceholderRect.anchoredPosition);

        if (totalDistanceFromCardToCenter >= maxDistanceAllowed)
        {
            resultState = true;
        }

        return resultState;
    }

    private bool CardIsInCenter(RectTransform thisCard)
    {
        return (thisCard.anchoredPosition.x - this.centerPlaceholderRect.anchoredPosition.x) <= 0;
    }

    private IEnumerator SwipeTo()
    {
        bool isScrolling = true;

        while (isScrolling)
        {
            for (int index = 0; index < this.categoryCardsList.Length; index++)
            {
                Vector2 cardWidthVector = new Vector2((float)(categoryCardsList[0].rect.width), 0f);

                Vector2 newPosition = this.categoryCardsList[index].anchoredPosition - (this.intercardSpacingVector + cardWidthVector);

                this.categoryCardsList[index].anchoredPosition =
                    Vector2.Lerp(this.categoryCardsList[index].localPosition, newPosition, this.accelerationCurve.Evaluate(Time.time) * Time.deltaTime);

                Debug.Log("Curve value " + this.accelerationCurve.Evaluate(Time.time));
                Debug.Log("Time " + Time.time);
                Debug.Log("Position : " + this.categoryCardsList[4].position.normalized);
            }

            if (this.CardIsInCenter(this.categoryCardsList[4]))
            {
                isScrolling = false;
                break;
            }
            else
            {
                isScrolling = true;
            }

            yield return null;
        }
    }

#endregion

#region Mono Behaviour

    private void Awake()
    {
        this.intercardSpacingVector = new Vector2(this.intercardSpacing, 0f);

        this.SetUpCards();

        StartCoroutine(this.SwipeTo());
    }

#endregion

}
