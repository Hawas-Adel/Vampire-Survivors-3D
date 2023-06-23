using UnityEngine;

public interface IDamageable : ITargetable, IStatsHolder
{
	public bool IsAlive { get; set; }

	System.Action<IDamageSource, float, bool> OnDamageTaken { get; set; }
	System.Action<IDamageSource> OnDeath { get; set; }

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
		UnityEngine.Debug.Log($"{Attacker} dealt {finalDamage} damage to {this} | Health Remaining = {targetHealth.CurrentValue}", this as UnityEngine.Object);

		OnDamageTaken?.Invoke(Attacker, finalDamage, isCriticalHit);
		Attacker.OnDamageDealt?.Invoke(this, finalDamage, isCriticalHit);
		IDamageSource.OnGlobalDamageDealt?.Invoke(Attacker, this, finalDamage, isCriticalHit);

		if (targetHealth.CurrentValue == 0f)
		{
			HandleDefaultComponentsBehaviorOnDeath();
			IsAlive = false;
			OnDeath?.Invoke(Attacker);
			Attacker.OnKill?.Invoke(this);
		}

		return finalDamage;
	}

	float GetFinalDamage(float damage, float armor) => damage;//TODO : figure out how armor affects damage

	void HandleDefaultComponentsBehaviorOnDeath()
	{
		foreach (Collider item in (this as Component).GetComponentsInChildren<Collider>())
		{
			item.enabled = false;
		}

		if ((this as Component).TryGetComponent(out Rigidbody rigidbody))
		{
			rigidbody.isKinematic = true;
		}
	}
}
