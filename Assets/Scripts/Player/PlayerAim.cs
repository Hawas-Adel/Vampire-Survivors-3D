using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
	[SerializeField][Min(0f)] private float RotationSpeed = 10f;

	public Vector3 pointerWorldPosition { get; private set; }

	private new Rigidbody rigidbody;

	private void Awake() => rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		pointerWorldPosition = GetPointerWorldPosition();
		SetAimRotation(Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation(pointerWorldPosition - rigidbody.position, rigidbody.transform.up), RotationSpeed * Time.fixedDeltaTime));
	}

	private Vector3 GetPointerWorldPosition()
	{
		Plane plane = new(Vector3.up, transform.position);
		Ray pointerCameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		plane.Raycast(pointerCameraRay, out float hitDistance);
		return pointerCameraRay.GetPoint(hitDistance);
	}

	public void SetAimRotation(Quaternion rotation) => rigidbody.MoveRotation(rotation);
}
