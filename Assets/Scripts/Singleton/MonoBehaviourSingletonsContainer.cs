using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoBehaviourSingletonsContainer
{
	public static IList<MonoBehaviour> instances = new List<MonoBehaviour>();
	public static bool destroyAll = false;

	public static void DestroyAllInstances(bool immediate = false, List<MonoBehaviour> exceptions = null) 
	{
		List<MonoBehaviour> list = new List<MonoBehaviour> ();

		foreach (MonoBehaviour instance in instances) 
		{
			list.Add(instance);
		}
		
		foreach (MonoBehaviour instance in list) 
		{
			if (instance != null && (exceptions == null 
			|| !exceptions.Contains (instance) 
			|| (exceptions.Contains (instance) && destroyAll))) 
			{
				if (immediate) 
				{
					GameObject.DestroyImmediate(instance.gameObject);
				} 
				else 
				{
					GameObject.Destroy (instance.gameObject);
				}
			}
		}

		instances.Clear ();
	}
}
