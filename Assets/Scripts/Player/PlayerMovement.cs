using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField][Min(0f)] private float MovementSpeed = 5f;

	private new Rigidbody rigidbody;

	private void Awake() => rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		Vector3 movementInput = GameManager.Instance.GameInput.MovementInputXZ;
		rigidbody.MovePosition(rigidbody.position + (MovementSpeed * Time.fixedDeltaTime * movementInput));
	}
}
