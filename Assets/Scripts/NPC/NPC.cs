using UnityEngine;

public class NPC : MonoBehaviour, IStatsHolder, ITargetable
{
	[SerializeField, Min(0f), Header("Stats")] private float MaxHealth = 1000f;
	[SerializeField, Min(0f)] private float HealthRegen = 5f;

	[SerializeField, Min(0f), Space] private float MaxMana = 500f;
	[SerializeField, Min(0f)] private float ManaRegen = 10f;

	[SerializeField, Min(0f), Space] private float MovementSpeed = 5f;
	[SerializeField, Min(0f)] private float Armor;
	[SerializeField, Min(0f)] private float CriticalChance;
	[SerializeField, Min(0f)] private float LifeSteal;

	public StatsHandler StatsHandler { get; private set; }

	string ITargetable.TeamID { get; } = nameof(NPC);

	private void Awake()
	{
		StatsHandler = new StatsHandler();
		StatsHandler.Initialize(MaxHealth, HealthRegen, MaxMana, ManaRegen,
			0f, 0f, 0f,
			MovementSpeed, Armor, CriticalChance, LifeSteal);
	}
}
