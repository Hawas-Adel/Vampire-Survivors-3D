using System.Collections;
using UnityEngine;

public static class TweenUtilities
{
	public static Coroutine DelayedCallBack(this MonoBehaviour monoBehaviour, System.Action action, float delay) => monoBehaviour.StartCoroutine(DelayedCallBackCOR(action, delay));
	public static IEnumerator DelayedCallBackCOR(System.Action action, float delay)
	{
		yield return new WaitForSeconds(delay);
		action.Invoke();
	}

	public static Coroutine Tween01(this MonoBehaviour monoBehaviour, float duration, System.Action<float> updateAction) => monoBehaviour.StartCoroutine(Tween01COR(duration, updateAction));
	private static IEnumerator Tween01COR(float duration, System.Action<float> updateAction)
	{
		for (float i = 0f; i < duration; i += Time.deltaTime)
		{
			updateAction.Invoke(i);
			yield return null;
		}

		updateAction.Invoke(1);
	}
}
