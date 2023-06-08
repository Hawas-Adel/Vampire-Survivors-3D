using UnityEngine;
using UnityEngine.UI;

public class NPCFloatingUI : UIElement<IEntity>
{
	[SerializeField] private Image HealthBarFillImage;

	private void Start() => Bind(GetComponentInParent<IEntity>());

	protected override void _Bind(IEntity value)
	{
		var healthStat = BoundValue.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);
		healthStat.OnCurrentValueChanged += UpdateHealthBarFill;
		UpdateHealthBarFill(healthStat);
	}

	private void UpdateHealthBarFill(StatWithCurrentValue stat) => HealthBarFillImage.fillAmount = stat.CurrentValue01;
}
