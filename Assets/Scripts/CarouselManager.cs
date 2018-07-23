using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselManager : MonoBehaviour 
{
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private RectTransform centerPlaceholderRect;
    [SerializeField] private int questionCardAmount;
    [SerializeField] private float intercardSpacing;

    private void SetUpQuestionCards()
    {
        // Get the prefab to instance
        GameObject questionCardPrefab = 
            QuestionCardFactory.instance.CreateQuestionCardByCategory(QuestionCardFactory.Categories.DWELLING, this.contentRect);

        // Build spacing vector using parameter from inspector
        Vector2 spacing = new Vector2(this.intercardSpacing, 0f);

        // Calculate offset taking the half width of a question card
        Vector2 offset = new Vector2((float)(questionCardPrefab.GetComponent<RectTransform>().rect.width), 0f);

        // Instantiation of prefabs 
        for(int index = 0; index < this.questionCardAmount; index++)
        {
            Vector2 newPosition = (offset + spacing) * index; // Multiply position by index a.k.a items amount in content
            GameObject questionCardInstance = Instantiate(questionCardPrefab, this.contentRect); // Instantiation
            questionCardInstance.GetComponent<RectTransform>().anchoredPosition = newPosition; // Set new position
        }
    }

    private void Start()
    {
        this.SetUpQuestionCards();        
    }


}
