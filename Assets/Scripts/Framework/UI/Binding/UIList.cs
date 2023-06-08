using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public abstract class UIList<T> : MonoBehaviour
{
	[SerializeField] private UIElement<T> elementPrefab;

	protected T[] BoundValues;
	public ReadOnlyCollection<UIElement<T>> GeneratedUIElements { get; private set; }

	public void Bind(T[] values)
	{
		_UnBind(BoundValues);
		BoundValues = values;
		_Bind(BoundValues);
	}

	protected virtual void _UnBind(T[] values) => transform.DestroyAllChildren(true);

	protected virtual void _Bind(T[] values)
	{
		List<UIElement<T>> newElements = new();
		for (int i = 0; i < values.Length; i++)
		{
			var element = Instantiate(elementPrefab, transform);
			element.Bind(values[i]);
			newElements.Add(element);
		}

		GeneratedUIElements = newElements.AsReadOnly();
	}

	public bool IsBoundToAValue() => BoundValues is null;
}
