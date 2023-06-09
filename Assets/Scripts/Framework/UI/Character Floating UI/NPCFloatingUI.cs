using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCFloatingUI : UIElement<IEntity>
{
	[SerializeField] private TextMeshProUGUI NameText;
	[SerializeField] private Image HealthBarFillImage;

	private void Start() => Bind(GetComponentInParent<IEntity>());

	protected override void _Bind(IEntity value)
	{
		NameText.text = (value as MonoBehaviour).name;
		StatWithCurrentValue healthStat = BoundValue.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Health);
		healthStat.OnCurrentValueChanged += UpdateHealthBarFill;
		UpdateHealthBarFill(healthStat);
	}

	private void UpdateHealthBarFill(StatWithCurrentValue stat) => HealthBarFillImage.fillAmount = stat.CurrentValue01;
}
