using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	public event UnityAction<Collider> _OnTriggerEnter;

	private void OnTriggerEnter(Collider other) => _OnTriggerEnter?.Invoke(other);
}
