using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float MovementSpeed = 5f;
	private new Rigidbody rigidbody;

	private void Awake() => rigidbody = GetComponent<Rigidbody>();

	private void Start()
	{
		Stat movementSpeedStat = GetComponent<IStatsHolder>().StatsHandler.GetStat<Stat>(StatID._Movement_Speed);
		movementSpeedStat.OnValueChanged += SyncMovementSpeedWithStat;
		SyncMovementSpeedWithStat(movementSpeedStat);
	}
	private void OnDestroy() => GetComponent<IStatsHolder>().StatsHandler.GetStat<Stat>(StatID._Movement_Speed).OnValueChanged -= SyncMovementSpeedWithStat;

	private void SyncMovementSpeedWithStat(Stat stat) => MovementSpeed = stat.GetValue();

	private void FixedUpdate()
	{
		Vector3 movementInput = GameManager.Instance.GameInput.MovementInputXZ;
		rigidbody.velocity = MovementSpeed * movementInput;
	}
}
