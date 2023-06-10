using UnityEngine;

public interface IDamageable : ITargetable, IStatsHolder
{
	System.Action<IDamageSource, float, bool> OnDamageTaken { get; }

	public float TakeDamage(IDamageSource Attacker, float baseDamage, float baseCriticalChance = 0f)
	{
		Stat attackerDamage = Attacker.StatsHandler.GetStat<Stat>(StatID._Damage);
		Stat targetArmor = StatsHandler.GetStat<Stat>(StatID._Armor);
		StatWithCurrentValue targetHealth = StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);

		float finalDamage = GetFinalDamage(attackerDamage.GetValue(baseDamage), targetArmor.GetValue());

		Stat attackerCriticalChance = Attacker.StatsHandler.GetStat<Stat>(StatID._Critical_Chance);
		bool isCriticalHit = Random.value <= attackerCriticalChance.GetValue(baseCriticalChance);
		if (isCriticalHit)
		{
			Stat attackerCriticalMultiplier = Attacker.StatsHandler.GetStat<Stat>(StatID._Critical_Multiplier);
			finalDamage *= attackerCriticalMultiplier.GetValue();
		}

		targetHealth.ModifyCurrentValue(-finalDamage);
		UnityEngine.Debug.Log($"{Attacker} dealt {finalDamage} to {this} | Health Remaining = {targetHealth.CurrentValue}", this as UnityEngine.Object);

		OnDamageTaken?.Invoke(Attacker, finalDamage, isCriticalHit);
		Attacker.OnDamageDealt?.Invoke(this, finalDamage, isCriticalHit);
		IDamageSource.OnGlobalDamageDealt?.Invoke(Attacker, this, finalDamage, isCriticalHit);

		return finalDamage;
	}

	float GetFinalDamage(float damage, float armor) => damage;//TODO : figure out how armor affects damage
}
