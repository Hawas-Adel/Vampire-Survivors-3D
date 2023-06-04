using UnityEngine;

public class GameInput
{
	private GameControls GameControls;

	public Vector2 MovementInput => GameControls.Gameplay.Movement.ReadValue<Vector2>();
	public Vector3 MovementInputXZ
	{
		get
		{
			var input = MovementInput;
			return new Vector3(input.x, 0f, input.y);
		}
	}

	public event System.Action<int> OnPlayerSkillCastInput;

	public GameInput()
	{
		GameControls = new GameControls();
		AddSkillCastingListners();
		EnableGamePlayControls();
	}

	public void EnableGamePlayControls() => GameControls.Gameplay.Enable();
	public void DisableGamePlayControls() => GameControls.Gameplay.Disable();

	private void AddSkillCastingListners()
	{
		GameControls.Gameplay.CastSkill1.performed += _ => OnPlayerSkillCastInput?.Invoke(0);
		GameControls.Gameplay.CastSkill2.performed += _ => OnPlayerSkillCastInput?.Invoke(1);
		GameControls.Gameplay.CastSkill3.performed += _ => OnPlayerSkillCastInput?.Invoke(2);
		GameControls.Gameplay.CastSkill4.performed += _ => OnPlayerSkillCastInput?.Invoke(3);
	}
}
