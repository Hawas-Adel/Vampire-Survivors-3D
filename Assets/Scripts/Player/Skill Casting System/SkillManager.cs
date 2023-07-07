using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ICaster), typeof(PlayerAim))]
public class SkillManager : MonoBehaviour
{
	public List<Skill> Skills;

	public ICaster Caster { get; private set; }

	private PlayerAim playerAim;
	private readonly List<Coroutine> CurrentCastingCoroutines = new();

	private void Awake()
	{
		Caster = GetComponent<ICaster>();
		playerAim = GetComponent<PlayerAim>();
		Skills = Skills.Select(skill => skill.Clone()).ToList();
	}

	private void OnEnable() => GameManager.Instance.GameInput.OnPlayerSkillCastInput += TryCastSkill;
	private void OnDisable() => GameManager.Instance.GameInput.OnPlayerSkillCastInput -= TryCastSkill;

	private void TryCastSkill(int skillIndex)
	{
		Debug.Log($"Attempting to cast Skill at Index {skillIndex}");

		Skill skill = Skills.ElementAtOrDefault(skillIndex);
		if (skill == null)
		{
			return;
		}

		if (!CanCast() || !skill.CanCast(Caster))
		{
			return;
		}

		Cast(skill);
	}

	private bool CanCast() => CurrentCastingCoroutines.Count == 0;

	private void Cast(Skill skill)
	{
		playerAim.enabled = false;
		playerAim.SetAimRotation(Quaternion.LookRotation(playerAim.pointerWorldPosition - playerAim.transform.position, playerAim.transform.up));

		skill.PreCast(Caster);
		Caster.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana).ModifyCurrentValue(-Caster.StatsHandler.GetStat<Stat>(StatID._Mana_Cost).GetValue(skill.ManaCost));
		float castDuration = 1f / Caster.StatsHandler.GetStat<Stat>(StatID._Casting_Speed).GetValue(1f / skill.CastDuration);
		foreach (var (normalizedDelay, action) in skill.SkillCastCallbacks)
		{
			CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(Caster, playerAim.pointerWorldPosition), normalizedDelay * castDuration));
		}

		CurrentCastingCoroutines.Add(this.DelayedCallBack(() => skill.StartChargeCooldown(Caster), castDuration));
		CurrentCastingCoroutines.Add(this.DelayedCallBack(ResetSelf, castDuration));
	}

	private void ResetSelf()
	{
		CurrentCastingCoroutines.Clear();
		playerAim.enabled = true;
	}
}
