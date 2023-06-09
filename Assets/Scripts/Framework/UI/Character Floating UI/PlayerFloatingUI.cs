using UnityEngine;
using UnityEngine.UI;

public class PlayerFloatingUI : UIElement<IEntity>
{
	[SerializeField] private Image HealthBarFillImage;
	[SerializeField] private Image ManaBarFillImage;

	private void Start() => Bind(GetComponentInParent<IEntity>());

	protected override void _Bind(IEntity value)
	{
		StatWithCurrentValue healthStat = BoundValue.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);
		healthStat.OnCurrentValueChanged += UpdateHealthBarFill;
		UpdateHealthBarFill(healthStat);

		StatWithCurrentValue manaStat = BoundValue.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana);
		manaStat.OnCurrentValueChanged += UpdateManaBarFill;
		UpdateManaBarFill(manaStat);
	}

	private void UpdateHealthBarFill(StatWithCurrentValue stat) => HealthBarFillImage.fillAmount = stat.CurrentValue01;
	private void UpdateManaBarFill(StatWithCurrentValue stat) => ManaBarFillImage.fillAmount = stat.CurrentValue01;
}
