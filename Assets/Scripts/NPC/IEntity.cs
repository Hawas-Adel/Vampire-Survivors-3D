using UnityEngine;
using UnityEngine.Events;

public interface IEntity : IDamageSource, IDamageable, ICaster
{
	public UnityAction<Vector3> OnMove { get; set; }
}
