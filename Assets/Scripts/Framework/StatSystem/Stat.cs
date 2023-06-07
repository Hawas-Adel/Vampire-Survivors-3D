using System;

public class Stat
{
	public string ID { get; private set; }

	public event Action OnValueChanged;

	public float BaseValue { get; private set; } = 0f;
	public void ApplyBaseValueModifier(float value)
	{
		BaseValue += value;
		OnValueChanged?.Invoke();
	}
	public void RemoveBaseValueModifier(float value)
	{
		BaseValue -= value;
		OnValueChanged?.Invoke();
	}

	public float IncreasedValue { get; private set; } = 1f;
	public void ApplyIncreasedValueModifier(float value)
	{
		IncreasedValue += value;
		OnValueChanged?.Invoke();
	}
	public void RemoveIncreasedValueModifier(float value)
	{
		IncreasedValue -= value;
		OnValueChanged?.Invoke();
	}

	public float BonusValue { get; private set; } = 0f;
	public void ApplyBonusValueModifier(float value)
	{
		BonusValue += value;
		OnValueChanged?.Invoke();
	}
	public void RemoveBonusValueModifier(float value)
	{
		BonusValue -= value;
		OnValueChanged?.Invoke();
	}

	public float GetValue(float additionalBaseValue = 0f, float additionalIncreasedValue = 0f, float additionalBonusValue = 0f) => ((BaseValue + additionalBaseValue) * (IncreasedValue + additionalIncreasedValue)) + (BonusValue + additionalBonusValue);

	public virtual void Initialize(string ID, float baseValue)
	{
		this.ID = ID;
		BaseValue = baseValue;
	}
}
