using NaughtyAttributes;
using System;
using UnityEngine;

public class NPC : TrackedCollectionMonoBehavior<NPC>, IEntity, ICaster, ISpawnableEnemy
{
	[SerializeField, Min(0f), Foldout("Stats")] private float MaxHealth = 1000f;
	[SerializeField, Min(0f), Foldout("Stats")] private float HealthRegen = 5f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MaxMana = 500f;
	[SerializeField, Min(0f), Foldout("Stats")] private float ManaRegen = 10f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MovementSpeed = 5f;
	[SerializeField, Min(0f), Foldout("Stats")] private float Armor;
	[SerializeField, Min(0f), Foldout("Stats")] private float CriticalChance;
	[SerializeField, Min(0f), Foldout("Stats")] private float LifeSteal;

	public StatsHandler StatsHandler { get; private set; }

	string ITargetable.TeamID { get; } = nameof(NPC);
	Action<object> ITargetable.OnHit { get; }

	[field: SerializeField] public bool IsAlive { get; set; } = true;

	Action<IDamageSource, float, bool> IDamageable.OnDamageTaken { get; set; }
	Action<IDamageSource> IDamageable.OnDeath { get; set; }
	Action<IDamageable, float, bool> IDamageSource.OnDamageDealt { get; set; }

	Transform ICaster.Transform => transform;

	[field: SerializeField, Min(0f)] public float ThreatLevel { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float SpawnWeight { get; private set; } = 100f;

	private void Awake()
	{
		StatsHandler = new StatsHandler();
		StatsHandler.Initialize(MaxHealth, HealthRegen, MaxMana, ManaRegen,
			0f, 0f, 0f,
			MovementSpeed, Armor, CriticalChance, LifeSteal);
	}
}
