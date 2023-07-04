using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class AOE : MonoBehaviour
{
	public static AOE Create(Vector3 position, Quaternion rotation, string name = "AOE")
	{
		AOE AOE = new GameObject(name).AddComponent<AOE>();
		AOE.transform.SetPositionAndRotation(position, rotation);
		AOE.enabled = false;
		return AOE;
	}

	private float lifeTime;
	private HashSet<ITargetable> TargetsInsideAOE = new();
	private ITargetable[] IgnoredTargets = System.Array.Empty<ITargetable>();

	public event System.Action<ITargetable> OnEnter;
	public event System.Action<ITargetable> OnExit;

	public void SetIgnoredTargets(params ITargetable[] ignoredTargets) => IgnoredTargets = ignoredTargets;

	private partial Collider[] GetAllColliderInAOE();

	private void FixedUpdate()
	{
		ITargetable[] currentTargets = TargetingUtilities.GetTargetableEntities(GetAllColliderInAOE(), IgnoredTargets);

		foreach (var item in currentTargets.Except(TargetsInsideAOE).ToArray())
		{
			item.ApplyHitBehavior(this, OnEnter);
		}

		foreach (var item in TargetsInsideAOE.Except(currentTargets).ToArray())
		{
			OnExit?.Invoke(item);
		}

		TargetsInsideAOE = currentTargets.ToHashSet();
	}

	private void OnEnable() => FixedUpdate();

	private void OnDisable()
	{
		foreach (var item in TargetsInsideAOE)
		{
			OnExit?.Invoke(item);
		}
	}

	public void SetLifeTime(float lifeTime) => this.lifeTime = lifeTime;

	public void Activate()
	{
		enabled = true;
		this.DelayedCallBack(() => Destroy(gameObject), lifeTime);
	}
}
