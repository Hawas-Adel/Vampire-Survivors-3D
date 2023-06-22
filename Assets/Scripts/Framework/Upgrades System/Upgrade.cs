using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
	public abstract void Apply(IUpgradeHolder holder);
	public abstract void Remove(IUpgradeHolder holder);
}
