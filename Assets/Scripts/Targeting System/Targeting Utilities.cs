using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class TargetingUtilities
{
	public static bool AreAllies(ITargetable entity1, ITargetable entity2) => entity1.TeamID == entity2.TeamID;

	private static ITargetable GetTarget(Collider collider) => collider.GetComponentInParent<ITargetable>();
	public static ITargetable GetTarget(Collider collider, params ITargetable[] ignoredTargets)
	{
		ITargetable target = GetTarget(collider);
		if (ignoredTargets.Any(ignoredTarget => AreAllies(target, ignoredTarget)))
		{
			return null;
		}

		return target;
	}

	private static ITargetable[] GetTargets(Collider[] colliders) => colliders.
		Select(collider => collider.GetComponentInParent<ITargetable>()).
		Distinct().
		Where(targetable => targetable is not null).
		ToArray();

	public static ITargetable[] GetTargetableEntities(Collider[] colliders, params ITargetable[] IgnoredTargetable) => GetTargets(colliders).
		Where(targetable1 => IgnoredTargetable.
			Any(targetable2 => !AreAllies(targetable1, targetable2))).
		ToArray();

	public static void ApplyHitBehavior(this ITargetable[] targets, object hitSource, UnityAction<ITargetable> onHitAction)
	{
		foreach (ITargetable item in targets)
		{
			item.ApplyHitBehavior(hitSource, onHitAction);
		}
	}
}
