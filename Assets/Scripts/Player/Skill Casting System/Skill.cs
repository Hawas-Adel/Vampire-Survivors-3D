using System.Linq;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
	[field: SerializeField, Min(0f)] public float CastDuration { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 1f;
	[field: SerializeField, Min(1)] public int MaxCharges { get; private set; } = 1;

	private (float cooldownStartTimeStamp, float cooldownEndTimeStamp)[] chargeUsableTimes;

	public Skill() => chargeUsableTimes = new (float cooldownStartTimeStamp, float cooldownEndTimeStamp)[MaxCharges];

	public abstract (float normalizedDelay, System.Action<SkillManager, Vector3> action)[] SkillCastCallbacks { get; }

	public bool CanCast(SkillManager caster) => HasAvailableCharge();

	public bool HasAvailableCharge() => chargeUsableTimes.Any(chargeTime => chargeTime.cooldownEndTimeStamp <= Time.time);

	public void StartChargeCooldown()
	{
		int chargeIndex = System.Array.FindIndex(chargeUsableTimes, chargeTime => chargeTime.cooldownEndTimeStamp <= Time.time);
		float chargeCooldownStartTimeStamp = Mathf.Max(Time.time, chargeUsableTimes.Max(chargeTime => chargeTime.cooldownEndTimeStamp));
		chargeUsableTimes[chargeIndex] = (Time.time, Time.time + Cooldown);
	}

	protected static Vector3 GetCastDirection(SkillManager caster, Vector3 targetPoint) => (targetPoint - caster.transform.position).normalized;
	protected static Vector3 GetPointInCastDirection(SkillManager caster, Vector3 targetPoint, float distance)
	{
		var dir = GetCastDirection(caster, targetPoint);
		return caster.transform.position + (dir * distance);
	}
}
