using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Basic Attack")]
public class BasicAttack : Skill
{
	[field: SerializeField, Min(0f), Space] public float Range { get; private set; } = 1.5f;
	[field: SerializeField, Min(0f)] public float Damage { get; private set; } = 20f;
	[field: SerializeField, Min(0f)] public float AttackVerticalOffset { get; private set; } = 1.5f;

	public override (float normalizedDelay, Action<ICaster, Vector3> action)[] SkillCastCallbacks { get; }

	public BasicAttack() : base()
	{
		SkillCastCallbacks = new (float normalizedDelay, Action<ICaster, Vector3> action)[]
		{
			(0.5f, PerformAttack),
		};
	}

	private void PerformAttack(ICaster caster, Vector3 targetPoint)
	{
		float range = caster.StatsHandler.GetStat<Stat>(StatID._Range).GetValue(Range);
		Vector3 attackCenter = GetPointInCastDirection(caster, targetPoint, range / 2f);
		attackCenter += AttackVerticalOffset * Vector3.up;
		TargetingUtilities.GetTargetableEntities(Physics.OverlapSphere(attackCenter, range / 2f), caster).ApplyHitBehavior(targetable => DealDamage(caster, targetable));
		SpawnVFX(attackCenter, range);
	}

	private void DealDamage(ICaster caster, ITargetable targetable)
	{
		if (targetable is IDamageable damageable)
		{
			damageable.TakeDamage(caster, Damage);
		}
	}

	private void SpawnVFX(Vector3 attackCenter, float range)
	{
		Transform sphereVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
		Destroy(sphereVisual.GetComponent<Collider>());
		sphereVisual.position = attackCenter;
		sphereVisual.localScale = range * Vector3.one;
		Destroy(sphereVisual.gameObject, 0.1f);
	}
}
