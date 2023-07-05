using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Skills/Debug Skill")]
public class DebugSkill : Skill
{
	public override (float normalizedDelay, UnityAction<ICaster, Vector3> action)[] SkillCastCallbacks { get; } = new (float normalizedDelay, UnityAction<ICaster, Vector3> action)[]
	{
		(0f, (_, _) => Debug.Log($"Start Casting | {Time.time}")),
		(0.5f, (_, _) => Debug.Log($"MidWay Casting | {Time.time}")),
		(1f, (_, _) => Debug.Log($"End Casting | {Time.time}")),
	};

	public override bool CanAICast(ICaster caster) => false;
}
