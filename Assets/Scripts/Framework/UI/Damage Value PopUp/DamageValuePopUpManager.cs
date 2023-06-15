using UnityEngine;

public class DamageValuePopUpManager : SingletonMonoBehavior<DamageValuePopUpManager>
{
	[SerializeField] private DamageValuePopUp DamageValuePopUpPrefab;

	private void OnEnable() => IDamageSource.OnGlobalDamageDealt += SpawnDamagePopUp;
	private void OnDisable() => IDamageSource.OnGlobalDamageDealt -= SpawnDamagePopUp;

	private void SpawnDamagePopUp(IDamageSource source, IDamageable damageable, float damage, bool isCritical) => Instantiate(DamageValuePopUpPrefab, transform).Bind((damageable, damage, isCritical));
}
