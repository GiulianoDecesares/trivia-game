using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{

#region Private fields and accessors

    [SerializeField] private RectTransform[] categoryCardsList;
    [SerializeField] private RectTransform centerPlaceholderRect;
    [SerializeField] private RectTransform contentRect;

    [SerializeField] private int intercardSpacing;

    [SerializeField] private float reelSpeed;
    
    private Vector2 intercardSpacingVector;

    private bool isScrolling = false;

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
        Debug.Log("Distance to center -> " + Vector2.Distance(thisCard.anchoredPosition, this.centerPlaceholderRect.anchoredPosition));
        return (thisCard.anchoredPosition.x - this.centerPlaceholderRect.anchoredPosition.x) <= 0;
    }

#endregion

#region Mono Behaviour

    private void Awake()
    {
        this.intercardSpacingVector = new Vector2(this.intercardSpacing, 0f);

        this.SetUpCards();
    }

    private void Update()
    {
        //if(this.CardIsInCenter(this.categoryCardsList[4]))
        //{
        //    this.isScrolling = false;
        //}
        //else
        //{
        //    this.isScrolling = true;
        //}

        if(isScrolling)
        {
            for (int index = 0; index < this.categoryCardsList.Length; index++)
            {
                Vector2 cardWidthVector = new Vector2((float)(categoryCardsList[0].rect.width), 0f);

                Vector2 newPosition = this.categoryCardsList[index].anchoredPosition - (this.intercardSpacingVector + cardWidthVector);

                this.categoryCardsList[index].anchoredPosition =
                    Vector2.Lerp(this.categoryCardsList[index].localPosition, newPosition, this.reelSpeed * Time.deltaTime);

                if (this.CardIsOutOfView(this.categoryCardsList[index]))
                {
                    Debug.Log("Card " + index + " is out of view");
                }
                else
                {
                    Debug.Log("Card " + index + " is in view");
                }
            }
        }
    }

#endregion

}
