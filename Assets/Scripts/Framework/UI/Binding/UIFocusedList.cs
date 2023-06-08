using System.Linq;
using UnityEngine.Events;

public class UIFocusedList<T> : UIList<T>
{
	public UIElement<T> CurrentFocusedElement { get; private set; }

	public event UnityAction<UIElement<T>> OnFocusedElementChanged;
	public event UnityAction<T> OnFocusedValueChanged;

	protected override void _Bind(T[] values)
	{
		base._Bind(values);

		foreach (var element in GeneratedUIElements)
		{
			element.OnActivate += () => SetFocusedElement(element);
		}

		SetFocusedElement(GeneratedUIElements[0]);
	}

	public void SetFocusedElement(UIElement<T> element)
	{
		if (CurrentFocusedElement)
		{
			CurrentFocusedElement.OnUnFocused();
		}

		CurrentFocusedElement = element;
		CurrentFocusedElement.OnFocused();
		OnFocusedElementChanged?.Invoke(CurrentFocusedElement);
		OnFocusedValueChanged?.Invoke(CurrentFocusedElement.BoundValue);
	}

	public void SetFocusedElement(T value) => SetFocusedElement(GeneratedUIElements.First(element => element.BoundValue.Equals(value)));
	public void SetFocusedElement(int index) => SetFocusedElement(GeneratedUIElements[index]);
}
