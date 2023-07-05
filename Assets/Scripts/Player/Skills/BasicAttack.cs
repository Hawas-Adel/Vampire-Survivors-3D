using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Skills/Basic Attack")]
public class BasicAttack : Skill
{
	[field: SerializeField, Min(0f), Space] public float Range { get; private set; } = 1.5f;
	[field: SerializeField, Min(0f)] public float Damage { get; private set; } = 20f;
	[field: SerializeField, Min(0f)] public float AttackVerticalOffset { get; private set; } = 1.5f;

	public override (float normalizedDelay, UnityAction<ICaster, Vector3> action)[] SkillCastCallbacks { get; }

	public BasicAttack() : base()
	{
		SkillCastCallbacks = new (float normalizedDelay, UnityAction<ICaster, Vector3> action)[]
		{
			(0f, PerformBasicAttackAnimation),
			(0.5f, PerformAttack),
		};
	}

	private void PerformAttack(ICaster caster, Vector3 targetPoint)
	{
		float range = caster.StatsHandler.GetStat<Stat>(StatID._Range).GetValue(Range);
		Vector3 attackCenter = GetPointInCastDirection(caster, targetPoint, range / 2f);
		attackCenter += AttackVerticalOffset * Vector3.up;

		float damage = caster.StatsHandler.GetStat<Stat>(StatID._Damage).GetValue(Damage);

		AOE aoe = AOE.Create(attackCenter, caster.Transform.rotation, $"{caster}'s Basic Attack AOE");
		aoe.SetIgnoredTargets(caster);
		aoe.IncludeAOEShape(new AOE_Shape_Sphere(Vector3.zero, range / 2f));
		aoe.OnEnter += targetable => DealDamage(caster, targetable, damage);
		aoe.Activate();

		SpawnVFX(attackCenter, range);
	}

	private void SpawnVFX(Vector3 attackCenter, float range)
	{
		Transform sphereVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
		Destroy(sphereVisual.GetComponent<Collider>());
		sphereVisual.position = attackCenter;
		sphereVisual.localScale = range * Vector3.one;
		Destroy(sphereVisual.gameObject, 0.1f);
	}

	public override bool CanAICast(ICaster caster) => Vector3.Distance(caster.Transform.position, Player.Instance.transform.position) <= caster.StatsHandler.GetStat<Stat>(StatID._Range).GetValue(Range);
}
