using UnityEngine;

public abstract class Skill : ScriptableObject
{
	[field: SerializeField, Min(0f)] public float CastDuration { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 1f;
	[field: SerializeField, Min(1)] public int MaxCharges { get; private set; } = 1;

	public abstract (float normalizedDelay, System.Action<SkillManager, Vector3> action)[] SkillCastCallbacks { get; }

	protected static Vector3 GetCastDirection(SkillManager caster, Vector3 targetPoint) => (targetPoint - caster.transform.position).normalized;
	protected static Vector3 GetPointInCastDirection(SkillManager caster, Vector3 targetPoint, float distance)
	{
		var dir = GetCastDirection(caster, targetPoint);
		return caster.transform.position + (dir * distance);
	}
}
