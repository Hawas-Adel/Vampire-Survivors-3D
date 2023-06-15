using UnityEngine;

public class AIIdle : AIState
{
	[SerializeField] private bool RotateToFacePlayer = true;
	[SerializeField] private float RotationSpeed = 10f;

	public override float GetWeight() => 0.01f;

	public override bool CanEnterState() => true;
	public override bool CanExitState() => true;

	public override void WhileInState()
	{
		if (!RotateToFacePlayer)
		{
			return;
		}

		stateMachine.transform.rotation = Quaternion.Slerp(
			stateMachine.transform.rotation,
			Quaternion.LookRotation(
				Player.Instance.transform.position - stateMachine.transform.position,
				stateMachine.transform.up),
			RotationSpeed * Time.deltaTime);
	}
}
