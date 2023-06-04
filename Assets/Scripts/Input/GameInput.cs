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

	public GameInput()
	{
		GameControls = new GameControls();
		EnableGamePlayControls();
	}

	private void EnableGamePlayControls() => GameControls.Gameplay.Enable();
	private void DisableGamePlayControls() => GameControls.Gameplay.Disable();
}
