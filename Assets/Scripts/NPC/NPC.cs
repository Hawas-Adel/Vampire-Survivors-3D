using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NPC : TrackedCollectionMonoBehavior<NPC>, IEntity, ICaster, ISpawnableEnemy, IEXP_Source
{
	[SerializeField, Min(0f), Foldout("Stats")] private float MaxHealth = 1000f;
	[SerializeField, Min(0f), Foldout("Stats")] private float HealthRegen = 5f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MaxMana = 500f;
	[SerializeField, Min(0f), Foldout("Stats")] private float ManaRegen = 10f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MovementSpeed = 5f;
	[SerializeField, Min(0f), Foldout("Stats")] private float Armor;
	[SerializeField, Min(0f), Foldout("Stats")] private float CriticalChance;
	[SerializeField, Min(0f), Foldout("Stats")] private float LifeSteal;

	private NavMeshAgent navMeshAgent;

	public StatsHandler StatsHandler { get; private set; }

	string ITargetable.TeamID { get; } = nameof(NPC);
	Action<object> ITargetable.OnHit { get; }

	[field: SerializeField] public bool IsAlive { get; set; } = true;

	Action<IDamageSource, float, bool> IDamageable.OnDamageTaken { get; set; }
	Action<IDamageSource> IDamageable.OnDeath { get; set; }
	Action<IDamageable, float, bool> IDamageSource.OnDamageDealt { get; set; }
	Action<IDamageable> IDamageSource.OnKill { get; set; }

	Transform ICaster.Transform => transform;

	public UnityAction<Vector3> OnMove { get; set; }

	[field: SerializeField, Min(0f), Header("Spawn")] public float ThreatLevel { get; private set; } = 1f;
	[field: SerializeField, Min(0f)] public float SpawnWeight { get; private set; } = 100f;

	[field: SerializeField, Min(0f), Header("EXP")] public int EXPReward { get; private set; } = 100;

	private void Awake()
	{
		StatsHandler = new StatsHandler();
		StatsHandler.InitializeHealthStats(MaxHealth, HealthRegen);
		StatsHandler.InitializeManaStats(MaxMana, ManaRegen);
		StatsHandler.InitializeSpecialStats(0f, 0f, 0f);
		StatsHandler.InitializeSkillCastingRelatedStats();
		StatsHandler.InitializeCriticalHitStats(CriticalChance);
		StatsHandler.InitializeMiscStats(MovementSpeed, Armor, LifeSteal);

		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update() => OnMove?.Invoke(navMeshAgent.velocity);
}
