using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField][Min(0f)] private float MovementSpeed = 5f;
	[SerializeField][Min(0f)] private float RotationSpeed = 10f;

	private new Rigidbody rigidbody;

	private void Awake() => rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		Vector3 movementInput = GameManager.Instance.GameInput.MovementInputXZ;
		rigidbody.MovePosition(rigidbody.position + (MovementSpeed * Time.fixedDeltaTime * movementInput));
		if (movementInput == Vector3.zero)
		{
			return;
		}

		rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation(movementInput, rigidbody.transform.up), RotationSpeed * Time.fixedDeltaTime));
	}
}
