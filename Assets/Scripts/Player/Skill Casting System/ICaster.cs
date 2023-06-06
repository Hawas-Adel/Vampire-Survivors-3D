using UnityEngine;

public interface ICaster : ITargetable
{
	public Transform Transform { get; }
}
