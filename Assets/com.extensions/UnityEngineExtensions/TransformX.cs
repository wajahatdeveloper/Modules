using System;
using System.Collections.Generic;
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

    public static void SetPosition(this Transform transform, Vector3 position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.position = transform.position.SetValues(position, vectorAxesMask);
    }

    public static void SetPosition(this Transform transform, float position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetPosition(new Vector3(position, position, position), vectorAxesMask);
    }

    public static void SetLocalPosition(this Transform transform, Vector3 position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localPosition = transform.localPosition.SetValues(position, vectorAxesMask);
    }

    public static void SetLocalPosition(this Transform transform, float position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalPosition(new Vector3(position, position, position), vectorAxesMask);
    }

    public static void SetEulerAngles(this Transform transform, Vector3 angles, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.eulerAngles = transform.eulerAngles.SetValues(angles, vectorAxesMask);
    }

    public static void SetEulerAngles(this Transform transform, float angle, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetEulerAngles(new Vector3(angle, angle, angle), vectorAxesMask);
    }

    public static void SetLocalEulerAngles(this Transform transform, Vector3 angles, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localEulerAngles = transform.localEulerAngles.SetValues(angles, vectorAxesMask);
    }

    public static void SetLocalEulerAngles(this Transform transform, float angle, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalEulerAngles(new Vector3(angle, angle, angle), vectorAxesMask);
    }

    public static void SetLocalScale(this Transform transform, Vector3 scale, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localScale = transform.localScale.SetValues(scale, vectorAxesMask);
    }

    public static void SetLocalScale(this Transform transform, float scale, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalScale(new Vector3(scale, scale, scale), vectorAxesMask);
    }

    public static Transform[] GetChildren(this Transform parent)
    {
        var array = new Transform[parent.childCount];
        for (var i = 0; i < parent.childCount; i++)
        {
            array[i] = parent.GetChild(i);
        }
        return array;
    }

    public static Transform[] GetChildrenRecursive(this Transform parent)
    {
        var list = new List<Transform>();
        var children = parent.GetChildren();
        for (var i = 0; i < children.Length; i++)
        {
            var transform = children[i];
            list.Add(transform);
            if (transform.childCount > 0)
            {
                list.AddRange(transform.GetChildrenRecursive());
            }
        }
        return list.ToArray();
    }

    public static Transform FindChild(this Transform parent, Predicate<Transform> predicate)
    {
        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (predicate(child))
            {
                return child;
            }
        }
        return null;
    }

    public static Transform FindChildRecursive(this Transform parent, string childName)
    {
        return parent.FindChildRecursive(child => child.name == childName);
    }

    public static Transform FindChildRecursive(this Transform parent, Predicate<Transform> predicate)
    {
        var childrenRecursive = parent.GetChildrenRecursive();
        for (var i = 0; i < childrenRecursive.Length; i++)
        {
            var transform = childrenRecursive[i];
            if (predicate(transform))
            {
                return transform;
            }
        }
        return null;
    }

    public static Transform[] FindChildren(this Transform parent, string childName)
    {
        return parent.FindChildren(child => child.name == childName);
    }

    public static Transform[] FindChildren(this Transform parent, Predicate<Transform> predicate)
    {
        var list = new List<Transform>();
        var children = parent.GetChildren();
        for (var i = 0; i < children.Length; i++)
        {
            var transform = children[i];
            if (predicate(transform))
            {
                list.Add(transform);
            }
        }
        return list.ToArray();
    }

    public static Transform[] FindChildrenRecursive(this Transform parent, string childName)
    {
        return parent.FindChildrenRecursive(child => child.name == childName);
    }

    public static Transform[] FindChildrenRecursive(this Transform parent, Predicate<Transform> predicate)
    {
        var list = new List<Transform>();
        var childrenRecursive = parent.GetChildrenRecursive();
        for (var i = 0; i < childrenRecursive.Length; i++)
        {
            var transform = childrenRecursive[i];
            if (predicate(transform))
            {
                list.Add(transform);
            }
        }
        return list.ToArray();
    }
}
