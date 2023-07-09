using System.Collections.Generic;
using UnityEngine;

public class AICastSkill : AIState
{
	[SerializeField] private Skill Skill;
	[SerializeField, Min(0f)] private float Weight = 1000f;

	private ICaster caster;
	private readonly List<Coroutine> CurrentCastingCoroutines = new();

	protected override void Awake()
	{
		base.Awake();
		caster = GetComponent<ICaster>();
		Skill = Skill.Clone();
	}

	public override float GetWeight() => Weight;

	public override bool CanEnterState() => !IsCurrentlyCasting() && Skill.CanCast(caster) && Skill.CanAICast(caster);
	public override bool CanExitState() => !IsCurrentlyCasting();
	public override void OnEnterState(AIState previousState) => CastSkill();

	private bool IsCurrentlyCasting() => CurrentCastingCoroutines.Count != 0;

	private void CastSkill()
	{
		Skill.PreCast(caster);
		caster.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana).ModifyCurrentValue(-caster.StatsHandler.GetStat<Stat>(StatID._Mana_Cost).GetValue(Skill.ManaCost));
		float castDuration = 1f / caster.StatsHandler.GetStat<Stat>(StatID._Casting_Speed).GetValue(1f / Skill.CastDuration);
		foreach (var (normalizedDelay, action) in Skill.SkillCastCallbacks)
		{
			CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(caster, Player.Instance.transform.position), normalizedDelay * castDuration));
		}

		CurrentCastingCoroutines.Add(this.DelayedCallBack(ResetSelfAndExitState, castDuration));
		CurrentCastingCoroutines.Add(this.DelayedCallBack(() => Skill.StartChargeCooldown(caster), castDuration));
	}

	private void ResetSelfAndExitState()
	{
		CurrentCastingCoroutines.Clear();
		stateMachine.TransitionTo(stateMachine.GetValidNextState(this));
	}
}
