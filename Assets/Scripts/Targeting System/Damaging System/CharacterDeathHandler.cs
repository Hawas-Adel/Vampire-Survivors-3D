using UnityEngine;

public class CharacterDeathHandler : MonoBehaviour
{
	[SerializeField] private GameObject[] GameObjectsToDisable;
	[SerializeField] private Behaviour[] ComponentsToDisable;

	private void Awake() => GetComponent<IDamageable>().OnDeath += OnCharacterDeath;

	private void OnCharacterDeath(IDamageSource _)
	{
		foreach (var item in GameObjectsToDisable)
		{
			item.gameObject.SetActive(false);
		}

		foreach (var item in ComponentsToDisable)
		{
			item.enabled = false;
		}
	}
}
