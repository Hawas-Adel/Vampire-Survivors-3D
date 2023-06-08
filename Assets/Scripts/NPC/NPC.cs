using UnityEngine;

public class NPC : MonoBehaviour, IStatsHolder, ITargetable
{
	public StatsHandler StatsHandler { get; private set; } = new();

	string ITargetable.TeamID { get; } = nameof(NPC);
}
