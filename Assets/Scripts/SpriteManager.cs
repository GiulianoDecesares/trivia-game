using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {


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
					Debug.Log("Sprite added! " + this.sprites[index].name);
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
