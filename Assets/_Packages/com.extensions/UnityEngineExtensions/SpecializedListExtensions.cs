using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SpecializedListExtensions
{
    
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
}