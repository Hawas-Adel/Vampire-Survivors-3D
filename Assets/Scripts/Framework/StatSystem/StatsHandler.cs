using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
	private readonly List<Stat> stats = new();
	private readonly List<StatEffect> statEffects = new();

	public T AddStat<T>(string name, float baseValue = 0f) where T : Stat, new()
	{
		T stat = new();
		stat.Initialize(name, baseValue);
		stats.Add(stat);
		return stat;
	}

	public T GetStat<T>(string name) where T : Stat => stats.FirstOrDefault(stat => stat.ID == name) as T;

	public void ApplyStatEffect(StatEffect effect)
	{
		effect.Apply(GetStat<Stat>(effect.StatID));
		statEffects.Add(effect);
	}

	public void RemoveStatEffect(StatEffect effect)
	{
		if (!statEffects.Remove(effect))
		{
			return;
		}

		effect.Remove(GetStat<Stat>(effect.StatID));
	}
}
