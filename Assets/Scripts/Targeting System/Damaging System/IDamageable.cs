public interface IDamageable : ITargetable, IStatsHolder
{
	System.Action<IDamageSource, float> OnDamageTaken { get; }

	public float TakeDamage(IDamageSource Attacker, float baseDamage)
	{
		Stat attackerDamage = Attacker.StatsHandler.GetStat<Stat>(StatID._Damage);
		Stat targetArmor = StatsHandler.GetStat<Stat>(StatID._Armor);
		StatWithCurrentValue targetHealth = StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);

		float finalDamage = GetFinalDamage(attackerDamage.GetValue(baseDamage), targetArmor.GetValue());
		targetHealth.ModifyCurrentValue(-finalDamage);

		UnityEngine.Debug.Log($"{Attacker} dealt {finalDamage} to {this} | Health Remaining = {targetHealth.CurrentValue}", this as UnityEngine.Object);

		OnDamageTaken?.Invoke(Attacker, finalDamage);
		Attacker.OnDamageDealt?.Invoke(this, finalDamage);
		IDamageSource.OnGlobalDamageDealt?.Invoke(Attacker, this, finalDamage);

		return finalDamage;
	}

	float GetFinalDamage(float damage, float armor) => damage;//TODO : figure out how armor affects damage
}
