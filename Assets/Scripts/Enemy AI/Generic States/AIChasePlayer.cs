using UnityEngine;
using UnityEngine.AI;

public class AIChasePlayer : AIState
{
	[SerializeField, Min(0f)] private float ChaseStopDistance = 1.5f;
	[SerializeField, Min(0f)] private float PathUpdateTickTime = 0.25f;

	private NavMeshAgent navMeshAgent;

	protected override void Awake()
	{
		base.Awake();
		navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
	}

	public override float GetWeight() => 100f * base.GetWeight();

	public override bool CanEnterState() => Vector3.Distance(stateMachine.transform.position, Player.Instance.transform.position) > ChaseStopDistance;
	public override bool CanExitState() => true;

	public override void OnEnterState(AIState previousState)
	{
		InvokeRepeating(nameof(UpdatePathToPlayer), Random.Range(0f, PathUpdateTickTime), PathUpdateTickTime);
		navMeshAgent.stoppingDistance = ChaseStopDistance;
		UpdatePathToPlayer();
	}

	private void UpdatePathToPlayer()
	{
		if (Vector3.Distance(stateMachine.transform.position, Player.Instance.transform.position) <= ChaseStopDistance)
		{
			stateMachine.TransitionTo(stateMachine.GetValidNextState(this));
			return;
		}

		navMeshAgent.SetDestination(Player.Instance.transform.position);
	}

	public override void OnExitState(AIState nextState)
	{
		CancelInvoke(nameof(UpdatePathToPlayer));
		navMeshAgent.ResetPath();
	}
}
