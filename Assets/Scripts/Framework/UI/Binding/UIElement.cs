using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIElement<T> : MonoBehaviour
{
	public UnityAction OnActivate;

	public T BoundValue { get; private set; }

	public void Bind(T value)
	{
		_UnBind(BoundValue);
		BoundValue = value;
		_Bind(BoundValue);
	}

	protected void ReBind() => Bind(BoundValue);

	protected virtual void _UnBind(T value) { }
	protected abstract void _Bind(T value);

	public virtual void OnFocused() { }
	public virtual void OnUnFocused() { }

	public bool IsBoundToAValue() => EqualityComparer<T>.Default.Equals(BoundValue, default);
}
