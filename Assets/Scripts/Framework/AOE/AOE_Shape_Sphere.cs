using UnityEngine;

public class AOE_Shape_Sphere : AOE_Shape
{
	private readonly Vector3 center;
	private readonly float radius;

	private readonly Collider[] colliders;

	public AOE_Shape_Sphere(Vector3 center, float radius)
	{
		this.center = center;
		this.radius = radius;
		colliders = new Collider[0];
	}

	public override Collider[] GetCollidersInShape(AOE aoe)
	{
		Physics.OverlapSphereNonAlloc(aoe.transform.TransformPoint(center), radius, colliders);
		return colliders;
	}

	public override void DrawGizmos(AOE aoe) => Gizmos.DrawWireSphere(aoe.transform.TransformPoint(center), radius);
}
