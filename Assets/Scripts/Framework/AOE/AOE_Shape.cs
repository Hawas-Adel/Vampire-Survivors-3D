using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AOE_Shape
{
	public abstract Collider[] GetCollidersInShape(AOE aoe);
	public abstract void DrawGizmos(AOE aoe);
}

public partial class AOE : MonoBehaviour
{
	private readonly List<AOE_Shape> IncludedShapes = new();
	private readonly List<AOE_Shape> ExcludedShapes = new();

	public void IncludeAOEShape(AOE_Shape shape) => IncludedShapes.Add(shape);
	public void ExcludeAOEShape(AOE_Shape shape) => ExcludedShapes.Add(shape);

	private partial Collider[] GetAllColliderInAOE()
	{
		Collider[] ColliderInIncludedShapes = IncludedShapes.SelectMany(shape => shape.GetCollidersInShape(this)).ToArray();
		Collider[] ColliderInExcludedShapes = ExcludedShapes.SelectMany(shape => shape.GetCollidersInShape(this)).ToArray();
		return ColliderInIncludedShapes.Except(ColliderInExcludedShapes).ToArray();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		foreach (var item in IncludedShapes)
		{
			item.DrawGizmos(this);
		}

		Gizmos.color = Color.red;
		foreach (var item in ExcludedShapes)
		{
			item.DrawGizmos(this);
		}
	}
}
