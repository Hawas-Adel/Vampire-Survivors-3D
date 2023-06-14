using UnityEngine;

[RequireComponent(typeof(AIStateMachine))]
public abstract class AIState : MonoBehaviour
{
	protected AIStateMachine stateMachine;

	protected virtual void Awake() => stateMachine = GetComponent<AIStateMachine>();

	public abstract bool CanEnterState();
	public abstract bool CanExitState();

	public virtual float GetWeight() => 100f;

	public virtual void OnEnterState(AIState previousState) { }
	public virtual void WhileInState() { }
	public virtual void OnExitState(AIState nextState) { }
}
