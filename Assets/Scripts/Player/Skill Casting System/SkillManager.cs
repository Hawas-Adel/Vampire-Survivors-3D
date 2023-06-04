using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public List<Skill> Skills;
	public List<Coroutine> CurrentCastingCoroutines = new();

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

		if (!CanCast())
		{
			return;
		}

		Cast(skill);
	}

	private bool CanCast() => CurrentCastingCoroutines.Count == 0;


	private void Cast(Skill skill)
	{
		foreach (var (normalizedDelay, action) in skill.SkillCastCallbacks)
		{
			CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(this), normalizedDelay * skill.CastDuration));
		}

		this.DelayedCallBack(CurrentCastingCoroutines.Clear, skill.CastDuration);
	}
}
