using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Includes 3D Geometry functions.
/// </summary>
public static class Geometry3D
{
	public static Vector3 GetRandomPointInsideCollider(this BoxCollider boxCollider)
	{
		Vector3 extents = boxCollider.size / 2f;
		Vector3 point = new Vector3(
			UnityEngine.Random.Range(-extents.x, extents.x),
			UnityEngine.Random.Range(-extents.y, extents.y),
			UnityEngine.Random.Range(-extents.z, extents.z)
		) + boxCollider.center;
		return boxCollider.transform.TransformPoint(point);
	}

	public static Vector2 GetRandomPointInsideCollider(this BoxCollider2D boxCollider)
	{
		Vector2 extents = boxCollider.size / 2f;
		Vector2 point = new Vector2(
			UnityEngine.Random.Range(-extents.x, extents.x),
			UnityEngine.Random.Range(-extents.y, extents.y)
		) + boxCollider.offset;
		return boxCollider.transform.TransformPoint(point);
	}

	/// <summary>
	/// Finds the closest <see cref="Vector3"/> in <paramref name="allTargets"/>.
	/// </summary>
	public static Vector3 FindClosest(this Vector3 origin, IList<Vector3> allTargets)
    {
        if (allTargets == null)
        {
            throw new ArgumentNullException("allTargets");
        }

        switch (allTargets.Count)
        {
            case 0: return Vector3.zero;
            case 1: return allTargets[0];
        }

        float closestDistance = Mathf.Infinity;
        var closest = Vector3.zero;

        foreach (var iteratingTarget in allTargets)
        {
            float distanceSqr = (iteratingTarget - origin).sqrMagnitude;

            if (distanceSqr < closestDistance)
            {
                closestDistance = distanceSqr;
                closest = iteratingTarget;
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest <see cref="Transform"/> in <paramref name="allTargets"/>.
    /// </summary>
    public static Transform FindClosest(this Vector3 origin, IList<Transform> allTargets)
    {
        if (allTargets == null)
        {
            throw new ArgumentNullException("allTargets");
        }

        switch (allTargets.Count)
        {
            case 0: return null;
            case 1: return allTargets[0];
        }

        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (var iteratingTarget in allTargets)
        {
            float distanceSqr = (iteratingTarget.position - origin).sqrMagnitude;

            if (distanceSqr < closestDistance)
            {
                closestDistance = distanceSqr;
                closest = iteratingTarget;
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest <see cref="GameObject"/> in <paramref name="allTargets"/>.
    /// </summary>
    public static GameObject FindClosest(this Vector3 origin, IList<GameObject> allTargets)
    {
        if (allTargets == null)
        {
            throw new ArgumentNullException("allTargets");
        }

        switch (allTargets.Count)
        {
            case 0: return null;
            case 1: return allTargets[0];
        }

        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        foreach (var iteratingTarget in allTargets)
        {
            float distanceSqr = (iteratingTarget.transform.position - origin).sqrMagnitude;

            if (distanceSqr < closestDistance)
            {
                closestDistance = distanceSqr;
                closest = iteratingTarget;
            }
        }

        return closest;
    }

    /// <summary>
    /// <para>Returns the 3D center of all the points given.</para>
    /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
    /// </summary>
    public static Vector3 FindCenter(this IList<Vector3> points, bool weighted)
    {
        switch (points.Count)
        {
            case 0: return Vector3.zero;
            case 1: return points[0];
        }

        if (weighted)
        {
            return points.Aggregate(Vector3.zero, (current, point) => current + point) / points.Count;
        }

        var bound = new Bounds { center = points[0] };
        foreach (var point in points)
        {
            bound.Encapsulate(point);
        }

        return bound.center;
    }

    /// <summary>
    /// <para>Returns the 3D center of all the points given.</para>
    /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
    /// </summary>
    public static Vector3 FindCenter(this IList<GameObject> gameObjects, bool weighted)
    {
        switch (gameObjects.Count)
        {
            case 0: return Vector3.zero;
            case 1: return gameObjects[0].transform.position;
        }

        if (weighted)
        {
            return gameObjects.Aggregate(Vector3.zero,
                        (current, gameObject) => current + gameObject.transform.position) / gameObjects.Count;
        }

        var bound = new Bounds { center = gameObjects[0].transform.position };
        foreach (var gameObject in gameObjects)
        {
            bound.Encapsulate(gameObject.transform.position);
        }

        return bound.center;
    }

    /// <summary>
    /// <para>Returns the 3D center of all the points given.</para>
    /// <para>If <paramref name="weighted"/> is true, center point will be closer to the area that points are denser; if false, center will be the geometric exact center of bounding box of points.</para>
    /// </summary>
    public static Vector3 FindCenter(this IList<Transform> transforms, bool weighted)
    {
        switch (transforms.Count)
        {
            case 0: return Vector3.zero;
            case 1: return transforms[0].position;
        }

        if (weighted)
        {
            return transforms.Aggregate(Vector3.zero, (current, transform) => current + transform.position) /
                    transforms.Count;
        }

        var bound = new Bounds { center = transforms[0].position };
        foreach (var transform in transforms)
        {
            bound.Encapsulate(transform.position);
        }

        return bound.center;
    }

	#region DistanceTo

	/// <summary>
	/// Returns the Vector3 distance between these two GameObjects
	/// </summary>
	/// <param name="go"></param>
	/// <param name="otherGO"></param>
	/// <returns></returns>
	public static float DistanceTo(this GameObject go, GameObject otherGO)
	{
		return Vector3.Distance(go.transform.position, otherGO.transform.position);
	}

	/// <summary>
	/// Returns the Vecto3 distance between these two points
	/// </summary>
	/// <param name="go"></param>
	/// <param name="pos"></param>
	/// <returns></returns>
	public static float DistanceTo(this GameObject go, Vector3 pos)
	{
		return Vector3.Distance(go.transform.position, pos);
	}

	/// <summary>
	/// Returns the Vecto3 distance between these two points
	/// </summary>
	/// <param name="start"></param>
	/// <param name="dest"></param>
	/// <returns></returns>
	public static float DistanceTo(this Vector3 start, Vector3 dest)
	{
		return Vector3.Distance(start, dest);
	}

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

	// DistanceTo
	#endregion
}