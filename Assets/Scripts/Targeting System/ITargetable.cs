public interface ITargetable
{
	public string TeamID { get; }

	System.Action<object> OnHit { get; }

	public void ApplyHitBehavior(object hitSource, System.Action<ITargetable> onHitAction)
	{
		OnHit?.Invoke(hitSource);
		if (this is ITargetableBehaviorOverride targetableBehaviorOverride)
		{
			targetableBehaviorOverride.OnOverrideHitBehavior(hitSource, onHitAction);
		}
		else
		{
			onHitAction?.Invoke(this);
		}
	}
}

public interface ITargetableBehaviorOverride : ITargetable
{
	public void OnOverrideHitBehavior(object hitSource, System.Action<ITargetable> originalOnHitAction);
}
