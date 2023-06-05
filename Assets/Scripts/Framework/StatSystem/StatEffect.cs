public abstract class StatEffect
{
	public string StatID { get; private set; }
	public float Magnitude { get; private set; }

	protected StatEffect(string statID, float magnitude)
	{
		StatID = statID;
		Magnitude = magnitude;
	}

	public abstract void Apply(Stat stat);
	public abstract void Remove(Stat stat);
}

public class BaseValueStatEffect : StatEffect
{
	public BaseValueStatEffect(string statID, float magnitude) : base(statID, magnitude) { }

	public override void Apply(Stat stat) => stat.ApplyBaseValueModifier(Magnitude);
	public override void Remove(Stat stat) => stat.RemoveBaseValueModifier(Magnitude);
}

public class IncreasedValueStatEffect : StatEffect
{
	public IncreasedValueStatEffect(string statID, float magnitude) : base(statID, magnitude) { }

	public override void Apply(Stat stat) => stat.ApplyIncreasedValueModifier(Magnitude);
	public override void Remove(Stat stat) => stat.RemoveIncreasedValueModifier(Magnitude);
}

public class BonusValueStatEffect : StatEffect
{
	public BonusValueStatEffect(string statID, float magnitude) : base(statID, magnitude) { }

	public override void Apply(Stat stat) => stat.ApplyBonusValueModifier(Magnitude);
	public override void Remove(Stat stat) => stat.RemoveBonusValueModifier(Magnitude);
}
