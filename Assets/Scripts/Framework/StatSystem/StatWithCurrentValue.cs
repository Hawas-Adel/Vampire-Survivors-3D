using UnityEngine;

public class StatWithCurrentValue : Stat
{
	public float MaxValue => Mathf.Max(0f, GetValue());

	public float CurrentValue { get; private set; }
	public float CurrentValue01 => CurrentValue / MaxValue;

	public event System.Action OnCurrentValueChanged;

	public void ModifyCurrentValue(float modValue)
	{
		CurrentValue += modValue;
		ReCalibrateCurrentValue();
	}

	public override void Initialize(string ID, float baseValue)
	{
		base.Initialize(ID, baseValue);
		CurrentValue = MaxValue;
		OnValueChanged += ReCalibrateCurrentValue;
	}

	private void ReCalibrateCurrentValue()
	{
		CurrentValue = Mathf.Clamp(CurrentValue, 0f, MaxValue);
		OnCurrentValueChanged?.Invoke();
	}
}
