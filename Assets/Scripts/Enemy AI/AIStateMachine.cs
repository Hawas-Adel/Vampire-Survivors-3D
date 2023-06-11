using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
	public AIState CurrentState { get; private set; }

	public event System.Action<AIState> OnStateChanged;
}
