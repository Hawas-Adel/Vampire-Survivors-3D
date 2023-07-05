using UnityEngine;
using UnityEngine.Events;

public partial class ProjectileSource : MonoBehaviour
{
	private Projectile projectilesPrefab;
	private float projectileSpeed;
	private uint projectilesCount = 1;
	private System.Func<uint, float> firingPatternAngleOffsetPredicate = _ => 0f;

	private ITargetable[] ignoredTargets = System.Array.Empty<ITargetable>();
	private System.Func<Collider, bool> ignoredEnvironmentCollidersPredicate = _ => false;

	private float lifeTime = 200f;
	private float lifeTimeStartTimeStamp;

	public event UnityAction<Projectile> OnProjectileCreated;
	public event UnityAction<Projectile, ITargetable> OnHitTarget;
	public event UnityAction<Projectile> OnPostHit = projectile => Destroy(projectile.gameObject);
	public event UnityAction<Projectile, Collider> OnHitEnvironment;


	public void SetProjectilePrefabAndSpeed(Projectile projectilesPrefab, float projectileSpeed)
	{
		this.projectilesPrefab = projectilesPrefab;
		this.projectileSpeed = projectileSpeed;
	}

	public void SetProjectilesCount(uint projectilesCount, System.Func<uint, float> firingPatternAngleOffsetPredicate)
	{
		this.projectilesCount = projectilesCount;
		this.firingPatternAngleOffsetPredicate = firingPatternAngleOffsetPredicate;
	}

	public void SetIgnoredTargets(params ITargetable[] ignoredTargets) => this.ignoredTargets = ignoredTargets;
	public void SetIgnoredEnvironmentCollidersPredicate(System.Func<Collider, bool> ignoredEnvironmentCollidersPredicate) => this.ignoredEnvironmentCollidersPredicate = ignoredEnvironmentCollidersPredicate;

	public void SetLifeTime(float lifeTime) => this.lifeTime = lifeTime;
}
