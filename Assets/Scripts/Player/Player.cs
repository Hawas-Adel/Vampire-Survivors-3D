using UnityEngine;

public class Player : MonoBehaviour, IStatsHolder, ITargetable
{
	public StatsHandler StatsHandler { get; private set; }
	string ITargetable.TeamID { get; } = nameof(Player);
}
