public interface ITargetable
{
	public string TeamID { get; }

	public void ApplyHitBehavior(System.Action<ITargetable> onHitAction)
	{
		if (this is ITargetableBehaviorOverride targetableBehaviorOverride)
		{
			targetableBehaviorOverride.OnOverrideHitBehavior(onHitAction);
		}
		else
		{
			onHitAction.Invoke(this);
		}
	}
}

public interface ITargetableBehaviorOverride : ITargetable
{
	public void OnOverrideHitBehavior(System.Action<ITargetable> originalOnHitAction);
}
