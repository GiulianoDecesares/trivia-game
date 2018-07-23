using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCardFactory : MonoBehaviour 
{
	[SerializeField] private GameObject questionCardPrefab;

#region Singleton

	public static QuestionCardFactory instance;

	private void Awake()
	{
		instance = this;
	}

#endregion

	public enum Categories 
	{ 
		DWELLING, 
		ENVIRONMENT, 
		HEALTH, 
		INFORMATION_ACCESS,
		MOBILITY_LOGISTICS,
		MUNICIPALITYS_ECONOMIC_FINANCIAL_MANAGEMENT,
		PRODUCTION_EMPLOYMENT_TOURISM,
		SOCIAL,
		TERRITORIAL_ASPECTS
	};

	public GameObject CreateQuestionCardByCategory(Categories thisCategory, RectTransform parent)
	{
		GameObject resultCard = Instantiate(this.questionCardPrefab, parent);

		switch(thisCategory)
		{
			case Categories.DWELLING:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_dwelling"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_dwelling_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.DWELLING;

			    break;

			case Categories.ENVIRONMENT:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_environment"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_environment_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.ENVIRONMENT;

                break;

			case Categories.HEALTH:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_health"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_health_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.HEALTH;

                break;

			case Categories.INFORMATION_ACCESS:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_information_access"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_information_access_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.INFORMATION_ACCESS;

                break;

			case Categories.MOBILITY_LOGISTICS:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_mobility_logistics"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_mobility_logistics_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.MOBILITY_LOGISTICS;

                break;

			case Categories.MUNICIPALITYS_ECONOMIC_FINANCIAL_MANAGEMENT:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_municipalitys_economic_financial_management"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_municipalitys_economic_financial_management_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.MUNICIPALITYS_ECONOMIC_FINANCIAL_MANAGEMENT;

                break;

			case Categories.PRODUCTION_EMPLOYMENT_TOURISM:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_production_employment_tourism"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_production_employment_tourism_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.PRODUCTION_EMPLOYMENT_TOURISM;

                break;

			case Categories.SOCIAL:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_social"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_social_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.SOCIAL;

                break;

			case Categories.TERRITORIAL_ASPECTS:
				resultCard.GetComponent<QuestionCard>().
					SetCathegoryIcon(SpriteManager.instance.GetSpriteByName("category_territorial_aspects"));
				resultCard.GetComponent<QuestionCard>().
					SetHeaderSprite(SpriteManager.instance.GetSpriteByName("category_territorial_aspects_banner"));
                resultCard.GetComponent<QuestionCard>().Category = Categories.TERRITORIAL_ASPECTS;

                break;
		}


		return resultCard;
	}
}
