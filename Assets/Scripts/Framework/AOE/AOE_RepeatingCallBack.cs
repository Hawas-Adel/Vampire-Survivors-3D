using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class AOE : MonoBehaviour
{
	private class AOERepeatingCallBackData
	{
		private UnityAction<ITargetable> OnTick;
		private float TickDuration;
		private float LastTickTimeStamp;

		public AOERepeatingCallBackData(UnityAction<ITargetable> onTick, float tickDuration)
		{
			OnTick = onTick;
			TickDuration = tickDuration;
			LastTickTimeStamp = float.MinValue;
		}

		public bool ShouldInvokeThisFrame() => Time.time - LastTickTimeStamp >= TickDuration;

		public void Invoke(HashSet<ITargetable> targets)
		{
			LastTickTimeStamp = Time.time;
			foreach (var item in targets)
			{
				item.ApplyHitBehavior(this, OnTick);
			}
		}
	}

	private List<AOERepeatingCallBackData> repeatingCallBacks = new();

	public void AddRepeatingCallback(UnityAction<ITargetable> onTick, float TickDuration) => repeatingCallBacks.Add(new(onTick, TickDuration));

	private void Update()
	{
		foreach (var item in repeatingCallBacks)
		{
			if (item.ShouldInvokeThisFrame())
			{
				item.Invoke(TargetsInsideAOE);
			}
		}
	}
}
