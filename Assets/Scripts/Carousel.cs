using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carousel : MonoBehaviour
{
    #region Private fields and accessors

    [SerializeField] private RectTransform centerPlaceholderRect;
    [SerializeField] private RectTransform contentRect;

    [SerializeField] private int intercardSpacing;

    [SerializeField] private float reelSpeed;

    [SerializeField] private AnimationCurve accelerationCurve;

    [SerializeField] [Range(2, 10)] private int minRandomLapsValue;
    [SerializeField] [Range(10, 30)] private int maxRandomLapsValue;

    private List<GameObject> categoryCardsList = new List<GameObject>();
    private List<RectTransform> categoryCardsRectList = new List<RectTransform>();
    
    #endregion

    #region Private methods

    private void InstantiateQuestionCardsGroup()
    {
        for (int index = 0; index < 10; index++)
        {
            GameObject cardInstance = Instantiate(PrefabManager.instance.GetQuestionCardByCategory((QuestionManager.Categories)index), this.contentRect);
            this.categoryCardsList.Add(cardInstance);
            this.categoryCardsRectList.Add(cardInstance.GetComponent<RectTransform>());
        }
    }

    private void SetUpCards(int lapsNumber)
    {
        for (int amountMultyplier = 0; amountMultyplier < lapsNumber; amountMultyplier++)
        {
            this.InstantiateQuestionCardsGroup();
        }

        for (int index = 0; index < this.categoryCardsList.Count; index++)
        {
            // Build spacing vector using parameter from inspector
            Vector2 spacing = new Vector2(this.intercardSpacing, 0f);

            // Calculate offset taking the half width of a question card
            Vector2 offset = new Vector2((float)(categoryCardsList[index].GetComponent<RectTransform>().rect.width), 0f);

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

    private IEnumerator SwipeToCategory(QuestionManager.Categories thisCategory, int lapsAmount)
    {
        bool isScrolling = true;

        RectTransform targetCard = this.FindQuestionCardByCategory(thisCategory, lapsAmount);

        Vector2 intercardSpacingVector = new Vector2(this.intercardSpacing, 0f);

        float initialDistance = Vector2.Distance(targetCard.anchoredPosition, this.centerPlaceholderRect.anchoredPosition);

        while (isScrolling)
        {
            for (int index = 0; index < this.categoryCardsList.Count; index++)
            {
                RectTransform cardRect = this.categoryCardsRectList[index];

                float actualDistance = Vector2.Distance(targetCard.anchoredPosition, this.centerPlaceholderRect.anchoredPosition);

                // Vector containing the width of the selected card
                Vector2 cardWidthVector = new Vector2((float)(cardRect.rect.width), 0f);

                // New position to swipe
                Vector2 newPosition = cardRect.anchoredPosition - (intercardSpacingVector + cardWidthVector);

                // Change position
                cardRect.anchoredPosition = Vector2.Lerp(cardRect.localPosition, newPosition, 
                    this.accelerationCurve.Evaluate(actualDistance / initialDistance) * Time.deltaTime);
            }

            if (this.CardIsInCenter(targetCard))
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

    private RectTransform FindQuestionCardByCategory(QuestionManager.Categories thisCategory, int lapsAmount)
    {
        RectTransform result = null;
        int targetCardCounter = 0;
        
        for(int index = 0; index < this.categoryCardsList.Count; index++)
        {
            if(this.categoryCardsList[index].GetComponent<QuestionCard>().category == thisCategory)
            {
                targetCardCounter++;
                result = this.categoryCardsList[index].GetComponent<RectTransform>();
            }

            if (targetCardCounter == lapsAmount)
                break;
        }

        if(!result)
        {
            Debug.LogError("Card can't be found! Null return value", this.gameObject);
        }
        
        return result;
    }

    #endregion

    #region Mono Behaviour

    private void Start()
    {
        int lapsAmount = Random.Range(this.minRandomLapsValue, this.maxRandomLapsValue);
        
        this.SetUpCards(lapsAmount);

        this.StartSwipeToCategory(QuestionManager.Categories.HEALTH, lapsAmount);
    }

    #endregion

    #region Public Methods

    public void StartSwipeToCategory(QuestionManager.Categories thisCategory, int lapsAmount)
    {
        StartCoroutine(this.SwipeToCategory(thisCategory, lapsAmount));
    }

    #endregion

}
