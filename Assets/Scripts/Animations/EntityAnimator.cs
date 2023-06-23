using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Float)] private int VelocityX;
	[SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Float)] private int VelocityZ;

	[SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Trigger), Space] private int Death;

	private IEntity entity;

	private void Awake()
	{
		entity = GetComponentInParent<IEntity>();
		entity.OnMove += UpdateLocomotionAnimation;
		entity.OnDeath += TriggerDeathAnimation;
	}

	private void OnDestroy()
	{
		entity.OnMove -= UpdateLocomotionAnimation;
	}

	private void UpdateLocomotionAnimation(Vector3 velocity)
	{
		Vector3 localVelocity = entity.Transform.InverseTransformDirection(velocity);
		animator.SetFloat(VelocityX, localVelocity.x);
		animator.SetFloat(VelocityZ, localVelocity.z);
	}

	private void TriggerDeathAnimation(IDamageSource _) => animator.SetTrigger(Death);
}
