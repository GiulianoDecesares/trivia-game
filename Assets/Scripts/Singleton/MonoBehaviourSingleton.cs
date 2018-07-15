#if UNITY_EDITOR
	using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoBehaviourSingleton<Type> : MonoBehaviour where Type : MonoBehaviour 
{
	private static Type instance = null;
	public static Type Instance 
	{
		get {
			if( instance == null ) 
			{
				instance = GameObject.FindObjectOfType(typeof(Type)) as Type;
			}
			if( instance == null ) 
			{
				instance = new GameObject().AddComponent(typeof(Type)) as Type;
				instance.name = typeof(Type).ToString();
				MonoBehaviourSingletonsContainer.instances.Add(instance);
			}
			
			return instance;
		}
	}

	public void SetDirty() 
	{
		#if UNITY_EDITOR

			if (this) 
			{
				EditorUtility.SetDirty(this);
			}

		#endif
	}
}