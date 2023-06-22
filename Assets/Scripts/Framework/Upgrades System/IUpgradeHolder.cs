using System.Collections.Generic;

public interface IUpgradeHolder
{
	List<Upgrade> ActiveUpgrades { get; set; }

	public void AddUpgrade(Upgrade upgrade)
	{
		upgrade.Apply(this);
		ActiveUpgrades.Add(upgrade);
	}
	public void RemoveUpgrade(Upgrade upgrade)
	{
		if (!ActiveUpgrades.Remove(upgrade))
		{
			return;
		}

		upgrade.Remove(this);
	}
}
