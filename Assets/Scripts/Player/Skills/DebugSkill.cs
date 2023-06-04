using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Debug Skill")]
public class DebugSkill : Skill
{
	public override (float normalizedDelay, Action<SkillManager> action)[] SkillCastCallbacks { get; } = new (float normalizedDelay, Action<SkillManager> action)[]
	{
		(0f, _ => Debug.Log($"Start Casting | {Time.time}")),
		(0.5f, _ => Debug.Log($"MidWay Casting | {Time.time}")),
		(1f, _ => Debug.Log($"End Casting | {Time.time}")),
	};
}
