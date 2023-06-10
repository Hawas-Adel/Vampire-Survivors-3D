using UnityEngine;

public interface ICaster : ITargetable, IStatsHolder
{
	public Transform Transform { get; }
}
