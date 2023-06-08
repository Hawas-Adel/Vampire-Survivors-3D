using UnityEngine;

[RequireComponent(typeof(IStatsHolder))]
public class RegenHandler : MonoBehaviour
{
	[SerializeField, Min(0f)] private float RegenTickTime = 0.25f;

	private StatWithCurrentValue Health;
	private StatWithCurrentValue Mana;
	private Stat HealthRegen;
	private Stat ManaRegen;

	private void Start()
	{
		IStatsHolder statsHolder = GetComponent<IStatsHolder>();
		Health = statsHolder.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);
		Mana = statsHolder.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana);
		HealthRegen = statsHolder.StatsHandler.GetStat<Stat>(StatID._Health_Regen);
		ManaRegen = statsHolder.StatsHandler.GetStat<Stat>(StatID._Mana_Regen);

		InvokeRepeating(nameof(RegenerationLogic), Random.Range(0f, RegenTickTime), RegenTickTime);
	}

	private void RegenerationLogic()
	{
		Health.ModifyCurrentValue(HealthRegen.GetValue() * RegenTickTime);
		Mana.ModifyCurrentValue(ManaRegen.GetValue() * RegenTickTime);
	}
}
