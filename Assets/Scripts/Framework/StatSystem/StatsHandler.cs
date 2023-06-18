using System.Collections.Generic;
using System.Linq;

public class StatsHandler
{
	private const float _DefaultCriticalDamageMultiplier = 2f;

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

	public void InitializeHealthStats(float _Health, float _Health_Regen)
	{
		AddStat<StatWithCurrentValue>(StatID._Health, _Health);
		AddStat<Stat>(StatID._Health_Regen, _Health_Regen);
	}

	public void InitializeManaStats(float _Mana, float _Mana_Regen)
	{
		AddStat<StatWithCurrentValue>(StatID._Mana, _Mana);
		AddStat<Stat>(StatID._Mana_Regen, _Mana_Regen);
	}

	public void InitializeSpecialStats(float _Strength, float _Dexterity, float _Intelligence)
	{
		AddStat<Stat>(StatID._Strength, _Strength);
		AddStat<Stat>(StatID._Dexterity, _Dexterity);
		AddStat<Stat>(StatID._Intelligence, _Intelligence);
	}

	public void InitializeSkillCastingRelatedStats()
	{
		AddStat<Stat>(StatID._Casting_Speed, 0f);
		AddStat<Stat>(StatID._Mana_Cost, 0f);
		AddStat<Stat>(StatID._Cooldown, 0f);
		AddStat<Stat>(StatID._Range, 0f);

		AddStat<Stat>(StatID._AOE_Size, 0f);
		AddStat<Stat>(StatID._Duration, 0f);

		AddStat<Stat>(StatID._Damage, 0f);

		AddStat<Stat>(StatID._Projectile_Count, 0f);
		AddStat<Stat>(StatID._Projectile_Speed, 0f);
	}

	public void InitializeCriticalHitStats(float _Critical_Chance)
	{
		AddStat<Stat>(StatID._Critical_Chance, _Critical_Chance);
		AddStat<Stat>(StatID._Critical_Multiplier, _DefaultCriticalDamageMultiplier);
	}

	public void InitializeMiscStats(float _Movement_Speed, float _Armor, float _Life_Steal)
	{
		AddStat<Stat>(StatID._Movement_Speed, _Movement_Speed);
		AddStat<Stat>(StatID._Armor, _Armor);
		AddStat<Stat>(StatID._Life_Steal, _Life_Steal);
	}
}
