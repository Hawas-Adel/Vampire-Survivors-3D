using System.Collections.Generic;
using UnityEngine;

public partial class AOE : MonoBehaviour
{
	private class AOERepeatingCallBackData
	{
		private System.Action<ITargetable> OnTick;
		private float TickDuration;
		private float LastTickTimeStamp;

		public AOERepeatingCallBackData(System.Action<ITargetable> onTick, float tickDuration)
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

	public void AddRepeatingCallback(System.Action<ITargetable> onTick, float TickDuration) => repeatingCallBacks.Add(new(onTick, TickDuration));

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
