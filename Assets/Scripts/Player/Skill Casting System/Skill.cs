using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : ScriptableObject
{
	[field: SerializeField, Min(0f)] public float CastDuration { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float ManaCost { get; private set; } = 0f;
	[field: SerializeField, Min(1)] public int MaxCharges { get; private set; } = 1;

	private (float cooldownStartTimeStamp, float cooldownEndTimeStamp)[] chargeUsableTimes;

	private void OnEnable() => chargeUsableTimes = new (float cooldownStartTimeStamp, float cooldownEndTimeStamp)[MaxCharges];

	public abstract (float normalizedDelay, UnityAction<ICaster, Vector3> action)[] SkillCastCallbacks { get; }

	public bool CanCast(ICaster caster) => HasAvailableCharge() && HasEnoughMana(caster);
	public abstract bool CanAICast(ICaster caster);

	public virtual void PreCast(ICaster caster) { }

	public bool HasAvailableCharge() => chargeUsableTimes.Any(chargeTime => chargeTime.cooldownEndTimeStamp <= Time.time);
	private bool HasEnoughMana(ICaster caster)
	{
		float manaCost = caster.StatsHandler.GetStat<Stat>(StatID._Mana_Cost).GetValue(ManaCost);
		return caster.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana).CurrentValue >= manaCost;
	}

	public void StartChargeCooldown(ICaster caster)
	{
		int chargeIndex = System.Array.FindIndex(chargeUsableTimes, chargeTime => chargeTime.cooldownEndTimeStamp <= Time.time);
		float chargeCooldownStartTimeStamp = Mathf.Max(Time.time, chargeUsableTimes.Max(chargeTime => chargeTime.cooldownEndTimeStamp));
		chargeUsableTimes[chargeIndex] = (Time.time, Time.time + caster.StatsHandler.GetStat<Stat>(StatID._Cooldown).GetValue(Cooldown));
	}

	protected static Vector3 GetCastDirection(ICaster caster, Vector3 targetPoint) => (targetPoint - caster.Transform.position).normalized;
	protected static Vector3 GetPointInCastDirection(ICaster caster, Vector3 targetPoint, float distance)
	{
		var dir = GetCastDirection(caster, targetPoint);
		return caster.Transform.position + (dir * distance);
	}

	public Skill Clone()
	{
		Skill newSkill = Instantiate(this);
		newSkill.name = name;
		return newSkill;
	}

	protected void PerformBasicAttackAnimation(ICaster caster, Vector3 _)
	{
		EntityAttackAnimator attackAnimator = caster.Transform.GetComponentInChildren<EntityAttackAnimator>();
		if (!attackAnimator)
		{
			return;
		}

		attackAnimator.StartBasicAttackAnimation();
	}

	protected void DealDamage(ICaster caster, ITargetable target, float damage)
	{
		if (target is IDamageable damageable && caster is IDamageSource damageSource)
		{
			damageable.TakeDamage(damageSource, damage);
		}
	}
}
