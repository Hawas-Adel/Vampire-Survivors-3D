using System.Collections.Generic;
using UnityEngine;

public abstract class TrackedCollectionMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
	public static readonly List<T> Instances = new();

	protected virtual void OnEnable() => Instances.Add(this as T);
	protected virtual void OnDisable() => Instances.Remove(this as T);
}
