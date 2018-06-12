using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Generic singleton implementation, derived from http://wiki.unity3d.com/index.php/Singleton
 */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new Object();

	private static bool applicationQuitting = false;

	private void OnDestroy()
	{
		applicationQuitting = true;
	}

	public static T Instance
	{
		get
		{
			if (applicationQuitting)
			{
				Debug.LogWarning("[Singleton] Tried to access Singleton on application exit");
				return null;
			}

			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));

					if (FindObjectsOfType(typeof(T)).Length > 0)
					{
						Debug.LogError("[Singleton] Multiple instances of object type " + typeof(T));
						return _instance;
					}

					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "(singleton) " + typeof(T);
						DontDestroyOnLoad(singleton);
						Debug.Log("[Singleton] Created an instance of " + typeof(T));
					}
				}
			}
			return _instance;
		}
	}
}
