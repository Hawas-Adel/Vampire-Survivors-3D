using System.Linq;
using UnityEngine;

public static class TargetingUtilities
{
	public static bool AreAllies(ITargetable entity1, ITargetable entity2) => entity1.TeamID == entity2.TeamID;

	private static ITargetable[] GetTargetables(Collider[] colliders) => colliders.
		Select(collider => collider.GetComponentInParent<ITargetable>()).
		Distinct().
		Where(targetable => targetable is not null).
		ToArray();

	public static ITargetable[] GetTargetableEntities(Collider[] colliders, params ITargetable[] IgnoredTargetables) => GetTargetables(colliders).
		Where(targetable1 => IgnoredTargetables.
			Any(targetable2 => !AreAllies(targetable1, targetable2))).
		ToArray();
}
