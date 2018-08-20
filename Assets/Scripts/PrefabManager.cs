using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Prefabs array populated manually from inspector
    [SerializeField] private List<GameObject> prefabs; 

    // Prefabs in order by name
    private Dictionary<string, GameObject> prefabsByName = new Dictionary<string, GameObject>();

    public bool isInitialized { get; private set; }

    // Actualizacion period
    public float yieldPeriod = 0.03f;

    #region Sigleton

    public static PrefabManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(this.Initialize());
    }

    #endregion

    #region Private Methods

    private Sprite GetBannerByCategory(QuestionManager.Categories thisCategory)
    {
        Sprite result = null;

        switch (thisCategory)
        {
            case QuestionManager.Categories.CULTURE_AND_EDUCATION:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.CULTURE_AND_EDUCATION + "_banner");
                break;
            case QuestionManager.Categories.ENVIRONMENT:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.ENVIRONMENT + "_banner");
                break;
            case QuestionManager.Categories.HEALTH:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.HEALTH + "_banner");
                break;
            case QuestionManager.Categories.LIVING_PLACE:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.LIVING_PLACE + "_banner");
                break;
            case QuestionManager.Categories.MOBILITY_AND_LOGISTICS:
                SpriteManager.instance.GetSpriteByName(CategoryIconNames.MOBILITY_AND_LOGISTICS + "_banner");
                break;
            case QuestionManager.Categories.PUBLIC_MANAGEMENT:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_MANAGEMENT + "_banner");
                break;
            case QuestionManager.Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PRODUCTION_EMPLOYMENT_AND_TOURISM + "_banner");
                break;
            case QuestionManager.Categories.PUBLIC_INFORMATION_ACCESS:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_INFORMATION_ACCESS + "_banner");
                break;
            case QuestionManager.Categories.SOCIAL:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.SOCIAL + "_banner");
                break;
            case QuestionManager.Categories.TERRITORY:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.TERRITORY + "_banner");
                break;
        }

        return result;
    }

    private Sprite GetCategoryIconByCategory(QuestionManager.Categories thisCategory)
    {
        Sprite result = null;

        switch (thisCategory)
        {
            case QuestionManager.Categories.CULTURE_AND_EDUCATION:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.CULTURE_AND_EDUCATION);
                break;
            case QuestionManager.Categories.ENVIRONMENT:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.ENVIRONMENT);
                break;
            case QuestionManager.Categories.HEALTH:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.HEALTH);
                break;
            case QuestionManager.Categories.LIVING_PLACE:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.LIVING_PLACE);
                break;
            case QuestionManager.Categories.MOBILITY_AND_LOGISTICS:
                SpriteManager.instance.GetSpriteByName(CategoryIconNames.MOBILITY_AND_LOGISTICS);
                break;
            case QuestionManager.Categories.PUBLIC_MANAGEMENT:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_MANAGEMENT);
                break;
            case QuestionManager.Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PRODUCTION_EMPLOYMENT_AND_TOURISM);
                break;
            case QuestionManager.Categories.PUBLIC_INFORMATION_ACCESS:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_INFORMATION_ACCESS);
                break;
            case QuestionManager.Categories.SOCIAL:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.SOCIAL);
                break;
            case QuestionManager.Categories.TERRITORY:
                result = SpriteManager.instance.GetSpriteByName(CategoryIconNames.TERRITORY);
                break;
        }

        return result;
    }

    private GameObject BuildQuestionCardByCategory(QuestionManager.Categories thisCategory)
    {
        GameObject result = this.GetPrefabByName("QuestionCard");
        QuestionCard cardScript = result.GetComponent<QuestionCard>();

        if (result)
        {
            cardScript = result.GetComponent<QuestionCard>();
        }
        else
        {
            Debug.LogError("Null question card prefab", this.gameObject);
        }

        if (cardScript)
        {
            switch (thisCategory)
            {
                case QuestionManager.Categories.CULTURE_AND_EDUCATION:
                    cardScript.SetTitleText("Cultura y educación");
                    cardScript.category = QuestionManager.Categories.CULTURE_AND_EDUCATION;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.CULTURE_AND_EDUCATION));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.CULTURE_AND_EDUCATION + "_banner"));
                    break;
                case QuestionManager.Categories.ENVIRONMENT:
                    cardScript.SetTitleText("Ambiente");
                    cardScript.category = QuestionManager.Categories.ENVIRONMENT;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.ENVIRONMENT));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.ENVIRONMENT + "_banner"));
                    break;
                case QuestionManager.Categories.HEALTH:
                    cardScript.SetTitleText("Salud");
                    cardScript.category = QuestionManager.Categories.HEALTH;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.HEALTH));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.HEALTH + "_banner"));
                    break;
                case QuestionManager.Categories.LIVING_PLACE:
                    cardScript.SetTitleText("Vivienda");
                    cardScript.category = QuestionManager.Categories.LIVING_PLACE;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.LIVING_PLACE));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.LIVING_PLACE + "_banner"));
                    break;
                case QuestionManager.Categories.MOBILITY_AND_LOGISTICS:
                    cardScript.SetTitleText("Mobilidad y logística");
                    cardScript.category = QuestionManager.Categories.MOBILITY_AND_LOGISTICS;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.MOBILITY_AND_LOGISTICS));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.MOBILITY_AND_LOGISTICS + "_banner"));
                    break;
                case QuestionManager.Categories.PUBLIC_MANAGEMENT:
                    cardScript.SetTitleText("Gestión pública");
                    cardScript.category = QuestionManager.Categories.PUBLIC_MANAGEMENT;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_MANAGEMENT));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_MANAGEMENT + "_banner"));
                    break;
                case QuestionManager.Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM:
                    cardScript.SetTitleText("Producción, empleo y turismo");
                    cardScript.category = QuestionManager.Categories.PRODUCTION_EMPLOYMENT_AND_TOURISM;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PRODUCTION_EMPLOYMENT_AND_TOURISM));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PRODUCTION_EMPLOYMENT_AND_TOURISM + "_banner"));
                    break;
                case QuestionManager.Categories.PUBLIC_INFORMATION_ACCESS:
                    cardScript.SetTitleText("Acceso publico a la información");
                    cardScript.category = QuestionManager.Categories.PUBLIC_INFORMATION_ACCESS;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_INFORMATION_ACCESS));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.PUBLIC_INFORMATION_ACCESS + "_banner"));
                    break;
                case QuestionManager.Categories.SOCIAL:
                    cardScript.SetTitleText("Social");
                    cardScript.category = QuestionManager.Categories.SOCIAL;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.SOCIAL));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.SOCIAL + "_banner"));
                    break;
                case QuestionManager.Categories.TERRITORY:
                    cardScript.SetTitleText("Aspectos territoriales");
                    cardScript.category = QuestionManager.Categories.TERRITORY;
                    cardScript.SetCategoryIcon(SpriteManager.instance.GetSpriteByName(CategoryIconNames.TERRITORY));
                    cardScript.SetHeaderSprite(SpriteManager.instance.GetSpriteByName(CategoryIconNames.TERRITORY + "_banner"));
                    break;
            } 
        }
        else
        {
            Debug.LogError("Default value return", this.gameObject);
        }

        return result;
    }

    #endregion

    #region Public Methods

    public IEnumerator Initialize()
    {
        if (!this.isInitialized)
        {
            float lastYieldTime = Time.realtimeSinceStartup;

            // Use for instead of foreach cause garbaje collector
            for (int index = 0; index < this.prefabs.Count; index++)
            {
                if (this.prefabs[index] != null)
                {
                    if (this.prefabsByName.ContainsKey(prefabs[index].name))
                    {
                        Debug.LogWarning("Duplicated prefab named " + prefabs[index].name, this.gameObject);
                    }
                    else
                    {
                        this.prefabsByName[prefabs[index].name] = prefabs[index];
                    }

                    float currentTime = Time.realtimeSinceStartup;

                    if (currentTime - lastYieldTime > this.yieldPeriod)
                    {
                        lastYieldTime = currentTime;

                        yield return null;
                    }
                }
            }

            this.isInitialized = true;

            yield return null;
        }
        else
        {
            Debug.LogWarning(this.GetType().Name + " is already initialized.");
        }
    }

    public GameObject GetPrefabByName(string prefabName)
    {
        GameObject returnGameObject = null;

        if (!string.IsNullOrEmpty(prefabName))
        {
            this.prefabsByName.TryGetValue(prefabName, out returnGameObject);
        }
        else
        {
            Debug.LogError("Prefab name exception :: null or empty prefab name parameter");
        }

        if (returnGameObject == null)
            Debug.LogWarning("Prefab look up exception :: " + prefabName + " prefab not found");
            // IMPROVEMENT :: Load prefab from disk ?

        return returnGameObject;
    }

    public GameObject GetQuestionCardByCategory(QuestionManager.Categories thisCategory)
    { 
        return this.BuildQuestionCardByCategory(thisCategory);
    }

    #endregion
}