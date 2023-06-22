using UnityEngine;

public class AOE_Shape_Sphere : AOE_Shape
{
	private readonly Vector3 center;
	private readonly float radius;

	public AOE_Shape_Sphere(Vector3 center, float radius)
	{
		this.center = center;
		this.radius = radius;
	}

	public override Collider[] GetCollidersInShape(AOE aoe) => Physics.OverlapSphere(aoe.transform.TransformPoint(center), radius);
	public override void DrawGizmos(AOE aoe) => Gizmos.DrawWireSphere(aoe.transform.TransformPoint(center), radius);
}
