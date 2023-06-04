using UnityEngine;

public abstract class Skill : ScriptableObject
{
	[field: SerializeField, Min(0f)] public float CastDuration { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public int MaxCharges { get; private set; } = 1;

	public abstract (float normalizedDelay, System.Action<SkillManager> action)[] SkillCastCallbacks { get; }
}
