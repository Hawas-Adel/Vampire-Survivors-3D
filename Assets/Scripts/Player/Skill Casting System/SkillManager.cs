using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
	}

	private void OnEnable() => GameManager.Instance.GameInput.OnPlayerSkillCastInput += TryCastSkill;
	private void OnDisable() => GameManager.Instance.GameInput.OnPlayerSkillCastInput -= TryCastSkill;

	private void TryCastSkill(int skillIndex)
	{
		Debug.Log($"Attemting to cast Skill at Index {skillIndex}");

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
		foreach (var (normalizedDelay, action) in skill.SkillCastCallbacks)
		{
			CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(Caster, playerAim.pointerWorldPosition), normalizedDelay * skill.CastDuration));
		}

		this.DelayedCallBack(ResetSelf, skill.CastDuration);
		this.DelayedCallBack(skill.StartChargeCooldown, skill.CastDuration);
	}

	private void ResetSelf()
	{
		CurrentCastingCoroutines.Clear();
		playerAim.enabled = true;
	}
}
