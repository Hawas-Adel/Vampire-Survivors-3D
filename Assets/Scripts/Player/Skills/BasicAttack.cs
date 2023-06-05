using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Basic Attack")]
public class BasicAttack : Skill
{
	[field: SerializeField, Min(0f)] public float Range { get; private set; } = 1.5f;
	[field: SerializeField, Min(0f)] public float AttackVerticalOffset { get; private set; } = 1.5f;

	public override (float normalizedDelay, Action<SkillManager, Vector3> action)[] SkillCastCallbacks { get; }

	public BasicAttack() : base()
	{
		SkillCastCallbacks = new (float normalizedDelay, Action<SkillManager, Vector3> action)[]
		{
			(0.5f, PerformAttack),
		};
	}

	private void PerformAttack(SkillManager caster, Vector3 targetPoint)
	{
		Vector3 attackCenter = GetPointInCastDirection(caster, targetPoint, Range / 2f);
		attackCenter += AttackVerticalOffset * Vector3.up;
		foreach (var item in Physics.OverlapSphere(attackCenter, Range / 2f))
		{
			Debug.Log(item, item);
		}

		#region Visuals
		Transform sphereVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
		Destroy(sphereVisual.GetComponent<Collider>());
		sphereVisual.position = attackCenter;
		sphereVisual.localScale = Range / 2f * Vector3.one;
		Destroy(sphereVisual.gameObject, 0.1f);
		#endregion
	}
}
