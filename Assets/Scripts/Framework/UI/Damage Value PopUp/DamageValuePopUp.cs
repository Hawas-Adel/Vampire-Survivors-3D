using TMPro;
using UnityEngine;

public class DamageValuePopUp : UIElement<(IDamageable damageable, float damage, bool IsCritical)>
{
	[SerializeField] private Vector3 PositionOffset = 2f * Vector3.up;
	[SerializeField, Min(0f)] private float LifeTime = 2f;

	[SerializeField, Space] private TextMeshProUGUI DamageText;
	[SerializeField, Space] private Color CriticalDamageColor = Color.red;
	[SerializeField, Min(1f)] private float CriticalFontSizeMultiplier = 1.5f;

	protected override void _Bind((IDamageable damageable, float damage, bool IsCritical) value)
	{
		transform.position = (value.damageable as Component).transform.position + PositionOffset;
		DamageText.text = value.damage.ToString("f0");
		Destroy(gameObject, LifeTime);
		if (!value.IsCritical)
		{
			return;
		}

		DamageText.color = CriticalDamageColor;
		DamageText.fontSize *= CriticalFontSizeMultiplier;
	}
}
