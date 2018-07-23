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

    private GameObject InstanciateQuestionCardByIndex(int thisIndex, RectTransform parent)
    {
        GameObject resultGameObject = null;

        switch(thisIndex)
        {
            case 0:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.DWELLING, parent
                );
            break;

            case 1:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.ENVIRONMENT, parent
                );
            break;

            case 2:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.HEALTH, parent
                );
            break;

            case 3:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.INFORMATION_ACCESS, parent
                );
            break;

            case 4:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.MOBILITY_LOGISTICS, parent
                );
            break;

            case 5:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.MUNICIPALITYS_ECONOMIC_FINANCIAL_MANAGEMENT, parent
                );
            break;

            case 6:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.PRODUCTION_EMPLOYMENT_TOURISM, parent
                );
            break;

            case 7:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.SOCIAL, parent
                );
            break;

            case 8:
                resultGameObject = QuestionCardFactory.instance.CreateQuestionCardByCategory(
                    QuestionCardFactory.Categories.TERRITORIAL_ASPECTS, parent
                );
            break;
        }

        return resultGameObject;
    }

    private void SetUpQuestionCards()
    {
        for(int index = 0; index < this.questionCardAmount; index++)
        {
            GameObject questionCardInstance = this.InstanciateQuestionCardByIndex(index, this.contentRect); // Instantiation

            // Build spacing vector using parameter from inspector
            Vector2 spacing = new Vector2(this.intercardSpacing, 0f);

            // Calculate offset taking the half width of a question card
            Vector2 offset = new Vector2((float)(questionCardInstance.GetComponent<RectTransform>().rect.width), 0f);
        
            Vector2 newPosition = (offset + spacing) * index; // Multiply position by index a.k.a items amount in content
            
            questionCardInstance.GetComponent<RectTransform>().anchoredPosition = newPosition; // Set new position
        }
    }

    private void Start()
    {
        this.SetUpQuestionCards();        
    }


}
