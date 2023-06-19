using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : SingletonMonoBehavior<GameManager>
{
	public GameInput GameInput { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		GameInput = new();
	}

	#region Testing Game Pause
	private void Update()
	{
		if (Keyboard.current.pKey.wasPressedThisFrame)
		{
			if (Time.timeScale == 0f)
			{
				Time.timeScale = 1f;
				Debug.Log("un paused");
			}
			else
			{
				Time.timeScale = 0f;
				Debug.Log("paused");
			}
		}
	}
	#endregion
}
