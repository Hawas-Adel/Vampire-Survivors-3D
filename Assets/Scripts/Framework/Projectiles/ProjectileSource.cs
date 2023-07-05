using System.Collections.Generic;
using UnityEngine;

public partial class ProjectileSource : MonoBehaviour
{
	public static ProjectileSource Create(Vector3 position, Quaternion rotation, string name = "[Projectiles Source]")
	{
		ProjectileSource ProjectileSource = new GameObject($"{name}").AddComponent<ProjectileSource>();
		ProjectileSource.transform.SetPositionAndRotation(position, rotation);
		ProjectileSource.enabled = false;
		return ProjectileSource;
	}

	public List<Projectile> FiredProjectiles = new();

	public List<Projectile> Fire()
	{
		List<Projectile> firedProjectiles = new();
		for (uint i = 0; i < projectilesCount; i++)
		{
			Projectile projectile = Instantiate(projectilesPrefab, transform);
			projectile.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			projectile.transform.Rotate(transform.up, firingPatternAngleOffsetPredicate.Invoke(i));
			projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
			OnProjectileCreated?.Invoke(projectile);
			projectile._OnTriggerEnter += collider => OnProjectileHitACollider(projectile, collider);
			firedProjectiles.Add(projectile);
			Destroy(projectile.gameObject, lifeTime);
			lifeTimeStartTimeStamp = Time.time;
		}

		FiredProjectiles.AddRange(firedProjectiles);
		Destroy(gameObject, lifeTime);
		return firedProjectiles;
	}

	private void OnProjectileHitACollider(Projectile projectile, Collider collider)
	{
		if (IsImplicitlyIgnoredCollider(collider))
		{
			return;
		}

		ITargetable target = TargetingUtilities.GetTarget(collider);
		if (target != null)
		{
			if (TargetingUtilities.ShouldIgnoreTarget(target, ignoredTargets))
			{
				return;
			}

			target.ApplyHitBehavior(this, target => OnHitTarget?.Invoke(projectile, target));
			OnPostHit?.Invoke(projectile);
			return;
		}

		if (ignoredEnvironmentCollidersPredicate.Invoke(collider))
		{
			return;
		}

		OnHitEnvironment?.Invoke(projectile, collider);
		OnPostHit?.Invoke(projectile);
	}

	private bool IsImplicitlyIgnoredCollider(Collider collider) => collider.GetComponentInParent<Projectile>();
}
