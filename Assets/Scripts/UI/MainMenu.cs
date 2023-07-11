using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Button StartGameButton;
	[SerializeField, Scene] private int GamePlayScene;

	[SerializeField, Space] private Button QuitGameButton;

	private void Awake()
	{
		StartGameButton.onClick.AddListener(StartGame);
		QuitGameButton.onClick.AddListener(QuitGame);
	}

	private void StartGame() => SceneManager.LoadScene(GamePlayScene);
	private void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
