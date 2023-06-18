using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{
	[SerializeField, Min(0.1f)] private float TickTimePeriod = 0.5f;
	[SerializeField] public NavMeshAgent NavMeshAgent;

	[ShowNativeProperty] public AIState CurrentState { get; private set; }
	public event System.Action<AIState, AIState> OnStateChanged;

	private AIState[] managedStates;

	private void Awake() => managedStates = GetComponents<AIState>();

	private void Start()
	{
		OnStateMachineTick();
		InvokeRepeating(nameof(OnStateMachineTick), Random.Range(0f, TickTimePeriod), TickTimePeriod);

		Stat movementSpeedStat = GetComponent<IStatsHolder>().StatsHandler.GetStat<Stat>(StatID._Movement_Speed);
		movementSpeedStat.OnValueChanged += SyncMovementSpeedWithStat;
		SyncMovementSpeedWithStat(movementSpeedStat);
	}
	private void OnDestroy() => GetComponent<IStatsHolder>().StatsHandler.GetStat<Stat>(StatID._Movement_Speed).OnValueChanged -= SyncMovementSpeedWithStat;

	private void SyncMovementSpeedWithStat(Stat stat) => NavMeshAgent.speed = stat.GetValue();

	private void OnStateMachineTick()
	{
		if (CurrentState && !CurrentState.CanExitState())
		{
			return;
		}

		TransitionTo(GetValidNextState());
	}

	public AIState GetValidNextState(params AIState[] ignoredStates)
	{
		(AIState state, float weight)[] validStatesWithWeights = managedStates.Except(ignoredStates).
			Where(state => state.CanEnterState()).
			Select(state => (state, weight: state.GetWeight())).
			Where(State_Weight_Pair => State_Weight_Pair.weight > 0f).ToArray();

		float totalWeight = validStatesWithWeights.Sum(State_Weight_Pair => State_Weight_Pair.weight);
		float randomWeight = Random.Range(0f, totalWeight);
		foreach (var (state, weight) in validStatesWithWeights)
		{
			randomWeight -= weight;
			if (randomWeight <= 0f)
			{
				return state;
			}
		}

		return null;
	}

	public void TransitionTo(AIState nextState)
	{
		AIState previousState = CurrentState;
		CurrentState = nextState;

		if (previousState)
		{
			previousState.OnExitState(nextState);
		}

		if (CurrentState)
		{
			CurrentState.OnEnterState(CurrentState);
		}

		OnStateChanged?.Invoke(previousState, CurrentState);
	}

	private void Update()
	{
		if (CurrentState)
		{
			CurrentState.WhileInState();
		}
	}
}
