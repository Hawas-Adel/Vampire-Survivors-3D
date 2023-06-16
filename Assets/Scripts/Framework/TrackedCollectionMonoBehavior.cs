using System.Collections.Generic;
using UnityEngine;

public abstract class TrackedCollectionMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
	public static readonly List<T> All = new();

	protected virtual void OnEnable() => All.Add(this as T);
	protected virtual void OnDisable() => All.Remove(this as T);
}
