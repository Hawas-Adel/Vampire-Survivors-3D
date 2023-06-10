using UnityEngine;

public interface ICaster : ITargetable, IDamageSource
{
	public Transform Transform { get; }
}
