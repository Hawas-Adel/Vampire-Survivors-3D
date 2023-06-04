public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public GameInput GameInput { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		GameInput = new();
	}
}
