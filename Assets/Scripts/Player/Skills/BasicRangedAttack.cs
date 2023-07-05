using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Skills/Basic Ranged Attack")]
public class BasicRangedAttack : Skill
{
	[field: SerializeField, Min(0f)] public float Damage { get; private set; } = 20f;
	[field: SerializeField, Min(0f)] public float AttackVerticalOffset { get; private set; } = 1.5f;
	[field: SerializeField] public Projectile Projectile { get; private set; }
	[field: SerializeField, Min(0f)] public float ProjectileSpeed { get; private set; } = 8f;
	[field: SerializeField, Min(0f)] public int ProjectileCount { get; private set; } = 1;
	[field: SerializeField, Min(0f)] public float ProjectileFiringArcAngle { get; private set; } = 20f;

	public override (float normalizedDelay, UnityAction<ICaster, Vector3> action)[] SkillCastCallbacks { get; }

	public BasicRangedAttack() : base()
	{
		SkillCastCallbacks = new (float normalizedDelay, UnityAction<ICaster, Vector3> action)[]
		{
			(0f, PerformBasicAttackAnimation),
			(0.5f, PerformRangedAttack),
		};
	}

	private void PerformRangedAttack(ICaster caster, Vector3 targetPoint)
	{
		uint projectileCount = (uint)Mathf.Max(1, Mathf.FloorToInt(caster.StatsHandler.GetStat<Stat>(StatID._Projectile_Count).GetValue(ProjectileCount)));
		float projectileSpeed = caster.StatsHandler.GetStat<Stat>(StatID._Projectile_Speed).GetValue(ProjectileSpeed);
		float damage = caster.StatsHandler.GetStat<Stat>(StatID._Damage).GetValue(Damage);

		ProjectileSource PS = ProjectileSource.Create(caster.Transform.position + (AttackVerticalOffset * Vector3.up), Quaternion.LookRotation(GetCastDirection(caster, targetPoint), Vector3.up));
		PS.SetProjectilePrefabAndSpeed(Projectile, projectileSpeed);
		if (projectileCount > 1)
		{
			PS.SetProjectilesCount(projectileCount, _ => Random.Range(-ProjectileFiringArcAngle / 2f, ProjectileFiringArcAngle / 2f));
		}

		PS.SetIgnoredTargets(caster);
		PS.OnHitTarget += (_, target) => DealDamage(caster, target, damage);
		PS.Fire();
	}

	public override bool CanAICast(ICaster caster) => throw new System.NotImplementedException();
}
