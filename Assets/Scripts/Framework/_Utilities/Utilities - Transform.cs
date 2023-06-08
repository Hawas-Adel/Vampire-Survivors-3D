using UnityEngine;

public static partial class Utilities
{
	public static void DestroyAllChildren(this Transform transform, bool unParent = false)
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			var child = transform.GetChild(i);
			Object.Destroy(child.gameObject);
			if (unParent)
			{
				child.SetParent(null);
			}
		}
	}
}
