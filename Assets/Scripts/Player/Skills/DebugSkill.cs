using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Debug Skill")]
public class DebugSkill : Skill
{
	public override (float normalizedDelay, Action<SkillManager, Vector3> action)[] SkillCastCallbacks { get; } = new (float normalizedDelay, Action<SkillManager, Vector3> action)[]
	{
		(0f, (_, _) => Debug.Log($"Start Casting | {Time.time}")),
		(0.5f, (_, _) => Debug.Log($"MidWay Casting | {Time.time}")),
		(1f, (_, _) => Debug.Log($"End Casting | {Time.time}")),
	};
}
