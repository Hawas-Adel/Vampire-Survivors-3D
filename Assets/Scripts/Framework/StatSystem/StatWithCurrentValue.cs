using UnityEngine;

public class StatWithCurrentValue : Stat
{
	public float MaxValue => Mathf.Max(0f, GetValue());

	public float CurrentValue { get; private set; }
	public float CurrentValue01 => CurrentValue / MaxValue;

	public event System.Action<StatWithCurrentValue> OnCurrentValueChanged;

	public void ModifyCurrentValue(float modValue)
	{
		float newCurrentValue = Mathf.Clamp(CurrentValue + modValue, 0f, MaxValue);
		if (newCurrentValue == CurrentValue)
		{
			return;
		}

		CurrentValue = newCurrentValue;
		ReCalibrateCurrentValue(this);
	}

	public override void Initialize(string ID, float baseValue)
	{
		base.Initialize(ID, baseValue);
		CurrentValue = MaxValue;
		OnValueChanged += ReCalibrateCurrentValue;
	}

	private void ReCalibrateCurrentValue(Stat _)
	{
		CurrentValue = Mathf.Clamp(CurrentValue, 0f, MaxValue);
		OnCurrentValueChanged?.Invoke(this);
	}
}
