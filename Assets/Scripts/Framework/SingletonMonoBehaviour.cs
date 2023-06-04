using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	protected virtual void Awake()
	{
		if (Instance != null)
		{
			Destroy(Instance.gameObject);
		}

		Instance = this as T;
	}
}
