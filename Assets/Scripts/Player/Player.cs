using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : SingletonMonoBehavior<Player>, ICaster, IEntity, ILeveledEntity, IUpgradeHolder
{
	[SerializeField, Min(0f), Foldout("Stats")] private float MaxHealth = 1000f;
	[SerializeField, Min(0f), Foldout("Stats")] private float HealthRegen = 5f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MaxMana = 500f;
	[SerializeField, Min(0f), Foldout("Stats")] private float ManaRegen = 10f;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float Strength;
	[SerializeField, Min(0f), Foldout("Stats")] private float Dexterity;
	[SerializeField, Min(0f), Foldout("Stats")] private float Intelligence;

	[SerializeField, Min(0f), Foldout("Stats"), Space] private float MovementSpeed = 5f;
	[SerializeField, Min(0f), Foldout("Stats")] private float Armor;
	[SerializeField, Min(0f), Foldout("Stats")] private float CriticalChance;
	[SerializeField, Min(0f), Foldout("Stats")] private float LifeSteal;

	public StatsHandler StatsHandler { get; private set; }
	string ITargetable.TeamID { get; } = nameof(Player);
	Action<object> ITargetable.OnHit { get; }

	Transform ICaster.Transform => transform;

	public UnityAction<Vector3> OnMove { get; set; }

	bool IDamageable.IsAlive { get; set; } = true;
	Action<IDamageable, float, bool> IDamageSource.OnDamageDealt { get; set; }
	Action<IDamageSource> IDamageable.OnDeath { get; set; }
	Action<IDamageSource, float, bool> IDamageable.OnDamageTaken { get; set; }
	Action<IDamageable> IDamageSource.OnKill { get; set; }

	[ShowNativeProperty] int ILeveledEntity.CurrentEXP { get; set; }
	[ShowNativeProperty] int ILeveledEntity.CurrentLevel { get; set; }
	Action ILeveledEntity.OnLevelUp { get; set; }
	List<Upgrade> IUpgradeHolder.ActiveUpgrades { get; set; } = new();

	private new Rigidbody rigidbody;

	protected override void Awake()
	{
		base.Awake();
		StatsHandler = new StatsHandler();
		StatsHandler.InitializeHealthStats(MaxHealth, HealthRegen);
		StatsHandler.InitializeManaStats(MaxMana, ManaRegen);
		StatsHandler.InitializeSpecialStats(Strength, Dexterity, Intelligence);
		StatsHandler.InitializeSkillCastingRelatedStats();
		StatsHandler.InitializeCriticalHitStats(CriticalChance);
		StatsHandler.InitializeMiscStats(MovementSpeed, Armor, LifeSteal);

		rigidbody = GetComponent<Rigidbody>();

		(this as ILeveledEntity).Initialize();
	}

	private void Update() => OnMove?.Invoke(rigidbody.velocity);
}
