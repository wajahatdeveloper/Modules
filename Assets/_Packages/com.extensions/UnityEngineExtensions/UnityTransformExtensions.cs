using System.Collections.Generic;
using System;
using UnityEngine;

/* *****************************************************************************
 * File:    UnityTransformExtensions.cs
 * Author:  Philip Pierce - Monday, September 29, 2014
 * Description:
 *  Extensions for transforms and vector3
 *  
 * History:
 *  Monday, September 29, 2014 - Created
 * ****************************************************************************/

/// <summary>
/// Extensions for transforms and vector3
/// </summary>
public static class UnityTransformExtensions
{
			#region Position...

		public static void SetXYZ(this Transform transform, float x, float y, float z)
		{
			transform.position = new Vector3(x, y, z);
		}

		public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
		{
			transform.localPosition = new Vector3(x, y, z);
		}

		public static void SetXY(this Transform transform, float x, float y)
		{
			transform.position = new Vector3(x, y, transform.position.z);
		}

		public static void SetLocalXY(this Transform transform, float x, float y)
		{
			transform.localPosition = new Vector3(x, y, transform.localPosition.z);
		}

		public static void SetX(this Transform transform, float x)
		{
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}

		public static void SetLocalX(this Transform transform, float x)
		{
			transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		}

		public static void SetY(this Transform transform, float y)
		{
			transform.position = new Vector3(transform.position.x, y, transform.position.z);
		}

		public static void SetLocalY(this Transform transform, float y)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		}

		public static void SetZ(this Transform transform, float z)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, z);
		}

		public static void SetLocalZ(this Transform transform, float z)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		}

		public static Vector2 Position2(this Transform transform)
		{
			return transform.position;
		}

		public static void IncLocalX(this Transform transform, float dx)
		{
			transform.localPosition = new Vector3(transform.localPosition.x + dx, transform.localPosition.y,
			                                      transform.localPosition.z);
		}

		public static void IncLocalY(this Transform transform, float dy)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + dy,
			                                      transform.localPosition.z);
		}

		public static void IncLocalZ(this Transform transform, float dz)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
			                                      transform.localPosition.z + dz);
		}

		#endregion

		public static void SetScaleX(this Transform transform, float scaleX)
		{
			transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
		}

		public static void SetScaleY(this Transform transform, float scaleY)
		{
			transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
		}

		public static void SetScaleZ(this Transform transform, float scaleZ)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);
		}

		public static void SetRotation(this Transform transform, float angle)
		{
			transform.rotation = new Quaternion();
			transform.Rotate(Vector3.forward, angle);
		}


    #region SetPositionX

    /// <summary>
    /// Sets the X position value
    /// </summary>
    /// <param name="t"></param>
    /// <param name="newX"></param>
    public static void SetPositionX(this Transform t, float newX)
    {
       t.position = t.position.SetX(newX);
    }
    #endregion

    #region SetPositionY

    /// <summary>
    /// Sets the Y position value
    /// </summary>
    /// <param name="t"></param>
    /// <param name="newY"></param>
    public static void SetPositionY(this Transform t, float newY)
    {
        t.position = t.position.SetY(newY);
    }
    #endregion

    #region SetPositionZ

    /// <summary>
    /// Sets the Z position value
    /// </summary>
    /// <param name="t"></param>
    /// <param name="newZ"></param>
    public static void SetPositionZ(this Transform t, float newZ)
    {
        t.position = t.position.SetZ(newZ);
    }
    #endregion

    #region GetPositionX

    /// <summary>
    /// Returns X of position
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float GetPositionX(this Transform t)
    {
        return t.position.x;
    }
    #endregion

    #region GetPositionY

    /// <summary>
    /// Returns Y of position
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float GetPositionY(this Transform t)
    {
        return t.position.y;
    }
    #endregion

    #region GetPositionZ

    /// <summary>
    /// Returns Z of position
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float GetPositionZ(this Transform t)
    {
        return t.position.z;
    }
	#endregion

	/// <summary>
	/// Returns the Vecto3 distance between these two transforms
	/// </summary>
	/// <param name="start"></param>
	/// <param name="dest"></param>
	/// <remarks>
	/// Suggested by: Vipsu
	/// Link: http://forum.unity3d.com/members/vipsu.138664/
	/// </remarks>
	public static float DistanceTo(this Transform start, Transform dest)
	{
		return Vector3.Distance(start.position, dest.position);
	}
	
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