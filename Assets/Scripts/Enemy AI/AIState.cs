using UnityEngine;

[RequireComponent(typeof(AIStateMachine))]
public abstract class AIState : MonoBehaviour
{
	private AIStateMachine stateMachine;

	private void Awake() => stateMachine = GetComponent<AIStateMachine>();

	public abstract bool CanEnterState();
	public abstract bool CanExitState();

	public virtual void OnEnterState(AIState previousState) { }
	public virtual void WhileInState() { }
	public virtual void OnExitState(AIState nextState) { }
}
