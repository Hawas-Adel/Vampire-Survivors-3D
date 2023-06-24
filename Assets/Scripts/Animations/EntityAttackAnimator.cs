using NaughtyAttributes;
using UnityEngine;

public abstract class EntityAttackAnimator : MonoBehaviour
{
	[SerializeField] private Animator Animator;
	[SerializeField, Min(1)] private int AnimationLayerIndex = 1;
	[SerializeField, AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Float)] private int AnimationSpeed;
	[SerializeField, AnimatorParam(nameof(Animator), AnimatorControllerParameterType.Trigger)] private int BasicAttackTrigger;

	public void SetAnimationSpeed(float speed) => Animator.SetFloat(AnimationSpeed, speed);

	public void StartBasicAttackAnimation() => Animator.SetTrigger(BasicAttackTrigger);

	private void OnEnable() => Animator.SetLayerWeight(AnimationLayerIndex, 1f);
	private void OnDisable() => Animator.SetLayerWeight(AnimationLayerIndex, 0f);
}
