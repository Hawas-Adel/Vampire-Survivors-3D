using UnityEngine;

public class Player : MonoBehaviour, IStatsHolder, ICaster
{
	public StatsHandler StatsHandler { get; private set; } = new();

	string ITargetable.TeamID { get; } = nameof(Player);

	Transform ICaster.Transform => transform;
}
