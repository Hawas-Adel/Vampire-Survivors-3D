using System;
using UnityEngine;

public class Player : SingletonMonoBehavior<Player>, ICaster, IEntity
{
	[SerializeField, Min(0f), Header("Stats")] private float MaxHealth = 1000f;
	[SerializeField, Min(0f)] private float HealthRegen = 5f;

	[SerializeField, Min(0f), Space] private float MaxMana = 500f;
	[SerializeField, Min(0f)] private float ManaRegen = 10f;

	[SerializeField, Min(0f), Space] private float Strength;
	[SerializeField, Min(0f)] private float Dexterity;
	[SerializeField, Min(0f)] private float Intelligence;

	[SerializeField, Min(0f), Space] private float MovementSpeed = 5f;
	[SerializeField, Min(0f)] private float Armor;
	[SerializeField, Min(0f)] private float CriticalChance;
	[SerializeField, Min(0f)] private float LifeSteal;

	public StatsHandler StatsHandler { get; private set; }

	string ITargetable.TeamID { get; } = nameof(Player);
	Action<object> ITargetable.OnHit { get; }

	Transform ICaster.Transform => transform;

	Action<IDamageable, float, bool> IDamageSource.OnDamageDealt { get; set; }
	Action<IDamageSource> IDamageable.OnDeath { get; set; }
	Action<IDamageSource, float, bool> IDamageable.OnDamageTaken { get; set; }

	protected override void Awake()
	{
		base.Awake();
		StatsHandler = new StatsHandler();
		StatsHandler.Initialize(MaxHealth, HealthRegen, MaxMana, ManaRegen,
			Strength, Dexterity, Intelligence,
			MovementSpeed, Armor, CriticalChance, LifeSteal);
	}
}
