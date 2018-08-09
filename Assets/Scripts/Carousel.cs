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

    [SerializeField] [Range(2, 6)] private int minRandomLapsValue;
    [SerializeField] [Range(6, 30)] private int maxRandomLapsValue;

    private List<GameObject> categoryCardsList = new List<GameObject>();
    private List<RectTransform> categoryCardsRectList = new List<RectTransform>();

    public System.Action onSwipeFinished;

    #endregion

    #region Private methods

    private void Start()
    {
        this.SetUpCards(1);
    }

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
        if (this.categoryCardsRectList.Count > 0)
        {
            for (int index = 0; index < this.categoryCardsList.Count; index++)
            {
                Destroy(this.categoryCardsList[index]);
            }

            this.categoryCardsRectList.Clear();
            this.categoryCardsList.Clear();
        }

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
        return (thisCard.anchoredPosition.x - this.centerPlaceholderRect.anchoredPosition.x) <= 8;
    }

    private IEnumerator SwipeToCategory(GameObject targetCard, int lapsAmount)
    {
        RectTransform targetCardRect = targetCard.GetComponent<RectTransform>();

        Vector2 intercardSpacingVector = new Vector2(this.intercardSpacing, 0f);
        float initialDistance = Vector2.Distance(targetCardRect.anchoredPosition, this.centerPlaceholderRect.anchoredPosition);

        while (!this.CardIsInCenter(targetCardRect))
        {
            for (int index = 0; index < this.categoryCardsList.Count; index++)
            {
                RectTransform cardRect = this.categoryCardsRectList[index];

                float actualDistance = Vector2.Distance(targetCardRect.anchoredPosition, this.centerPlaceholderRect.anchoredPosition);

                // Vector containing the width of the selected card
                Vector2 cardWidthVector = new Vector2((cardRect.rect.width), 0f);

                // New position to swipe
                Vector2 newPosition = cardRect.anchoredPosition - (intercardSpacingVector + cardWidthVector);

                // Change position
                cardRect.anchoredPosition = Vector2.Lerp(cardRect.anchoredPosition, newPosition, 
                    this.accelerationCurve.Evaluate((actualDistance / initialDistance)) * Time.deltaTime);
            }

            yield return null;
        }

        targetCard.GetComponent<QuestionCard>()?.StartShowUpAnimation();
        this.onSwipeFinished?.Invoke();

        yield return null;
    }

    private GameObject FindQuestionCardByCategory(QuestionManager.Categories thisCategory, int lapsAmount)
    {
        GameObject result = null;
        int targetCardCounter = 0;
        
        for(int index = 0; index < this.categoryCardsList.Count; index++)
        {
            if(this.categoryCardsList[index].GetComponent<QuestionCard>().category == thisCategory)
            {
                targetCardCounter++;
                result = this.categoryCardsList[index];
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

    #region Public Methods

    /// <summary>
    /// This method swipes the carousel to one card of the parameter category, by a random amount of laps
    /// </summary>
    /// <param name="thisCategory">Category of the card you want to swipe</param>
    /// <returns>Returns the GameObject of the target card</returns>
    public GameObject StartSwipeToCategory(QuestionManager.Categories thisCategory)
    {
        GameObject result = null;

        int lapsAmount = Random.Range(this.minRandomLapsValue, this.maxRandomLapsValue);
        
        this.SetUpCards(lapsAmount);

        result = this.FindQuestionCardByCategory(thisCategory, lapsAmount);

        if (result)
        {
            StartCoroutine(this.SwipeToCategory(result, lapsAmount)); 
        }
        else
        {
            Debug.LogError("Carousel card not found, swipe was disabled");
        }

        return result;
    }

    #endregion

}
