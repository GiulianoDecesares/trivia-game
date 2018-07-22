using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs; // Prefabs array populated manually from inspector

    private Dictionary<string, GameObject> prefabsByName = new Dictionary<string, GameObject>();

    public bool isInitialized
    {
        get;
        private set;
    }

    public float yieldPeriod = 0.03f;

#region Sigleton

    public static PrefabManager instance { get; private set; }
    private void Awake() 
    { 
        instance = this; 
        StartCoroutine(this.Initialize()); 
    }

#endregion

    public IEnumerator Initialize() 
    {
        if(!this.isInitialized)
        {
            float lastYieldTime = Time.realtimeSinceStartup;

            for(int index = 0; index < this.prefabs.Count; index++) // Use for instead of foreach cause garbaje collector
            {
                if(this.prefabs[index] != null) 
                {
                    if(this.prefabsByName.ContainsKey(prefabs[index].name))
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

        if(!string.IsNullOrEmpty(prefabName))
        {
            this.prefabsByName.TryGetValue(prefabName, out returnGameObject);
        }
        else
        {
            Debug.LogError("Prefab name exception :: null or empty prefab name parameter");
            Debug.Break();
        }

        if(returnGameObject == null)
            Debug.LogWarning("Prefab look up exception :: " + prefabName + " prefab not found");
            // Load prefab from disk ??

        return returnGameObject;
    }
}
