using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CategoryIconNames
{
    public const string CULTURE_AND_EDUCATION = "category_culture_and_education";
    public const string ENVIRONMENT = "category_environment";
    public const string HEALTH = "category_health";
    public const string LIVING_PLACE = "category_living_place";
    public const string MOBILITY_AND_LOGISTICS = "category_mobility_logistics";
    public const string PRODUCTION_EMPLOYMENT_AND_TOURISM = "category_production_employment_tourism";
    public const string PUBLIC_INFORMATION_ACCESS = "category_information_access";
    public const string SOCIAL = "category_social";
    public const string TERRITORY = "category_territorial_aspects";
}

public class SpriteManager : MonoBehaviour 
{
#region Singleton

	public static SpriteManager instance;

#endregion

	[SerializeField] private List<Sprite> sprites = new List<Sprite>();

	[SerializeField] private Dictionary<string, Sprite> spritesByName = new Dictionary<string, Sprite>();

	private void Awake()
	{
		// Initialize singleton

		instance = this;

		// Generate spritesByName to look up for sprites 
		for(int index = 0; index < this.sprites.Count; index++)
		{
			if(this.sprites[index] != null) 
			{
				if(!this.spritesByName.ContainsKey(this.sprites[index].name))
				{
					this.spritesByName[this.sprites[index].name] = this.sprites[index];
				}
				else
				{
					Debug.LogWarning("Duplicated sprite " + this.sprites[index].name, this.gameObject);
				}
			}
			else
			{
				Debug.Log("Sprites sub index equals null");
			}
		}
	}

	public Sprite GetSpriteByName(string thisName)
	{
		Sprite result = null;

		if(!string.IsNullOrEmpty(thisName))
		{
			if(this.spritesByName.ContainsKey(thisName))
			{
				this.spritesByName.TryGetValue(thisName, out result);
			}
			else
			{
				Debug.LogError("Error :: sprite manager does not contain " + thisName + " sprite. Result 'll be null");
			}
		}
		else
		{
			Debug.LogError("Error :: null sprite name at GetSpriteByName");
		}

		return result;
	}
}
