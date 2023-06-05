using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public List<Skill> Skills;

	private PlayerAim playerAim;
	private List<Coroutine> CurrentCastingCoroutines = new();

	private void Awake() => playerAim = GetComponent<PlayerAim>();

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

		if (!CanCast() || !skill.CanCast(this))
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
			CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(this, playerAim.pointerWorldPosition), normalizedDelay * skill.CastDuration));
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
