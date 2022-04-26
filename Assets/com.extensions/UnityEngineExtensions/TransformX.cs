using UnityEngine;

/// <summary>
/// Transform extensions
/// </summary>
public static class TransformX
{
	/// <summary>
	/// Returns the 2D position of given transform on XY plane. Uses manual conversion.
	/// </summary>
	public static Vector2 Position2D(this Transform transform)
	{
		var p = transform.position;
		return new Vector2(p.x, p.y);
	}

	/// <summary>
	/// Returns the 2D local position of given transform on XY plane. Uses manual conversion.
	/// </summary>
	public static Vector2 LocalPosition2D(this Transform transform)
	{
		var lp = transform.localPosition;
		return new Vector2(lp.x, lp.y);
	}

	/// <summary>
	/// Moves the transform to a Vector2 on XY coordinates.
	/// </summary>
	public static void SetPosition2D(this Transform transform, Vector2 newPosition)
	{
		transform.position = new Vector3(newPosition.x, newPosition.y, 0);
	}

	/// <summary>
	/// Moves the transform to a Vector2 on local XY coordinates.
	/// </summary>
	public static void SetLocalPosition2D(this Transform transform, Vector2 newLocalPosition)
	{
		transform.localPosition = new Vector3(newLocalPosition.x, newLocalPosition.y, 0);
	}

	/// <summary>
	/// Deep search the heirarchy of the specified transform for the name. Uses width-first search.
	/// </summary>
	/// <param name="t"></param>
	/// <param name="name"></param>
	/// <returns></returns>
	public static Transform DeepSearch(this Transform t, string name)
	{
		Transform dt = t.Find(name);
		if (dt != null)
		{
			return dt;
		}

		foreach (Transform child in t)
		{
			dt = child.DeepSearch(name);
			if (dt != null)
				return dt;
		}
		return null;
	}

	public static void DestroyAllChildren(this Transform t)
	{
		foreach (Transform child in t)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	/// <summary>
	/// opposite of up
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static Vector3 down(this Transform t)
	{
		return -t.up;
	}

	/// <summary>
	/// opposite of right
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static Vector3 left(this Transform t)
	{
		return -t.right;
	}

	/// <summary>
	/// opposite of forward
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static Vector3 backward(this Transform t)
	{
		return -t.forward;
	}

	/// <summary>
	/// Rotates the transform so the forward vector points at target's current position.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="target">Target.</param>
	public static void LookAt2D(this Transform transform, Transform target)
	{
		transform.LookAt2D((Vector2)target.position);
	}

	/// <summary>
	/// Rotates the transform so the forward vector points at worldPosition.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="worldPosition">World position.</param>
	public static void LookAt2D(this Transform transform, Vector3 worldPosition)
	{
		transform.LookAt2D((Vector2)worldPosition);
	}

	/// <summary>
	/// Rotates the transform so the forward vector points at worldPosition.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="worldPosition">World position.</param>
	public static void LookAt2D(this Transform transform, Vector2 worldPosition)
	{
		Vector2 distance = worldPosition - (Vector2)transform.position;
		transform.eulerAngles = new Vector3(
			transform.eulerAngles.x,
			transform.eulerAngles.y,
			Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg);
	}
}
