using System.Linq;
using UnityEngine;

public static class TargetingUtilities
{
	public static bool AreAllies(ITargetable entity1, ITargetable entity2) => entity1.TeamID == entity2.TeamID;

	private static ITargetable[] GetTargets(Collider[] colliders) => colliders.
		Select(collider => collider.GetComponentInParent<ITargetable>()).
		Distinct().
		Where(targetable => targetable is not null).
		ToArray();

	public static ITargetable[] GetTargetableEntities(Collider[] colliders, params ITargetable[] IgnoredTargetable) => GetTargets(colliders).
		Where(targetable1 => IgnoredTargetable.
			Any(targetable2 => !AreAllies(targetable1, targetable2))).
		ToArray();

	public static void ApplyHitBehavior(this ITargetable[] targets, object hitSource, System.Action<ITargetable> onHitAction)
	{
		foreach (ITargetable item in targets)
		{
			item.ApplyHitBehavior(hitSource, onHitAction);
		}
	}
}
