using System;

public class Stat
{
	public string ID { get; private set; }

	public event Action<Stat> OnValueChanged;

	public float BaseValue { get; private set; } = 0f;
	public void ApplyBaseValueModifier(float value)
	{
		BaseValue += value;
		OnValueChanged?.Invoke(this);
	}
	public void RemoveBaseValueModifier(float value)
	{
		BaseValue -= value;
		OnValueChanged?.Invoke(this);
	}

	public float IncreasedValue { get; private set; } = 1f;
	public void ApplyIncreasedValueModifier(float value)
	{
		IncreasedValue += value;
		OnValueChanged?.Invoke(this);
	}
	public void RemoveIncreasedValueModifier(float value)
	{
		IncreasedValue -= value;
		OnValueChanged?.Invoke(this);
	}

	public float BonusValue { get; private set; } = 0f;
	public void ApplyBonusValueModifier(float value)
	{
		BonusValue += value;
		OnValueChanged?.Invoke(this);
	}
	public void RemoveBonusValueModifier(float value)
	{
		BonusValue -= value;
		OnValueChanged?.Invoke(this);
	}

	public float GetValue(float additionalBaseValue = 0f, float additionalIncreasedValue = 0f, float additionalBonusValue = 0f) => ((BaseValue + additionalBaseValue) * (IncreasedValue + additionalIncreasedValue)) + (BonusValue + additionalBonusValue);

	public virtual void Initialize(string ID, float baseValue)
	{
		this.ID = ID;
		BaseValue = baseValue;
	}
}
