using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class TargetingUtilities
{
	public static bool AreAllies(ITargetable entity1, ITargetable entity2) => entity1.TeamID == entity2.TeamID;

	public static ITargetable GetTarget(Collider collider) => collider.GetComponentInParent<ITargetable>();
	public static ITargetable GetTarget(Collider collider, params ITargetable[] ignoredTargets)
	{
		ITargetable target = GetTarget(collider);
		if (ShouldIgnoreTarget(target, ignoredTargets))
		{
			return null;
		}

		return target;
	}

	public static bool ShouldIgnoreTarget(ITargetable target, params ITargetable[] ignoredTargets) => ignoredTargets.Any(ignoredTarget => AreAllies(target, ignoredTarget));

	private static ITargetable[] GetTargets(Collider[] colliders) => colliders.
		Select(collider => GetTarget(collider)).
		Distinct().
		Where(targetable => targetable is not null).
		ToArray();

	public static ITargetable[] GetTargets(Collider[] colliders, params ITargetable[] IgnoredTargetable) => GetTargets(colliders).
		Where(targetable1 => !ShouldIgnoreTarget(targetable1, IgnoredTargetable)).
		ToArray();

	public static void ApplyHitBehavior(this ITargetable[] targets, object hitSource, UnityAction<ITargetable> onHitAction)
	{
		foreach (ITargetable item in targets)
		{
			item.ApplyHitBehavior(hitSource, onHitAction);
		}
	}
}
