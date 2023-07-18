using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class VectorExtensions
{
    #region Set X/Y/Z

	// Set X

	public static Vector3 SetX(this Vector3 vector, float x)
	{
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector2 SetX(this Vector2 vector, float x)
	{
		return new Vector2(x, vector.y);
	}

	public static void SetX(this Transform transform, float x)
	{
		transform.position = transform.position.SetX(x);
	}

	// Set Y

	public static Vector3 SetY(this Vector3 vector, float y)
	{
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector2 SetY(this Vector2 vector, float y)
	{
		return new Vector2(vector.x, y);
	}

	public static void SetY(this Transform transform, float y)
	{
		transform.position = transform.position.SetY(y);
	}

	// Set Z

	public static Vector3 SetZ(this Vector3 vector, float z)
	{
		return new Vector3(vector.x, vector.y, z);
	}

	public static void SetZ(this Transform transform, float z)
	{
		transform.position = transform.position.SetZ(z);
	}

	// Set XY

	public static Vector3 SetXY(this Vector3 vector, float x, float y)
	{
		return new Vector3(x, y, vector.z);
	}

	public static void SetXY(this Transform transform, float x, float y)
	{
		transform.position = transform.position.SetXY(x, y);
	}

	// Set XZ

	public static Vector3 SetXZ(this Vector3 vector, float x, float z)
	{
		return new Vector3(x, vector.y, z);
	}

	public static void SetXZ(this Transform transform, float x, float z)
	{
		transform.position = transform.position.SetXZ(x, z);
	}

	// Set YZ

	public static Vector3 SetYZ(this Vector3 vector, float y, float z)
	{
		return new Vector3(vector.x, y, z);
	}

	public static void SetYZ(this Transform transform, float y, float z)
	{
		transform.position = transform.position.SetYZ(y, z);
	}

	//Reset

	/// <summary>
	/// Set position to Vector3.zero.
	/// </summary>
	public static void ResetPosition(this Transform transform)
	{
		transform.position = Vector3.zero;
	}


	// RectTransform 

	public static void SetPositionX(this RectTransform transform, float x)
	{
		transform.anchoredPosition = transform.anchoredPosition.SetX(x);
	}

	public static void SetPositionY(this RectTransform transform, float y)
	{
		transform.anchoredPosition = transform.anchoredPosition.SetY(y);
	}

	public static void OffsetPositionX(this RectTransform transform, float x)
	{
		transform.anchoredPosition = transform.anchoredPosition.OffsetX(x);
	}

	public static void OffsetPositionY(this RectTransform transform, float y)
	{
		transform.anchoredPosition = transform.anchoredPosition.OffsetY(y);
	}

	#endregion


	#region Offset X/Y/Z

	public static Vector3 Offset(this Vector3 vector, Vector2 offset)
	{
		return new Vector3(vector.x + offset.x, vector.y + offset.y, vector.z);
	}


	public static Vector3 OffsetX(this Vector3 vector, float x)
	{
		return new Vector3(vector.x + x, vector.y, vector.z);
	}

	public static Vector2 OffsetX(this Vector2 vector, float x)
	{
		return new Vector2(vector.x + x, vector.y);
	}

	public static void OffsetX(this Transform transform, float x)
	{
		transform.position = transform.position.OffsetX(x);
	}


	public static Vector2 OffsetY(this Vector2 vector, float y)
	{
		return new Vector2(vector.x, vector.y + y);
	}

	public static Vector3 OffsetY(this Vector3 vector, float y)
	{
		return new Vector3(vector.x, vector.y + y, vector.z);
	}

	public static void OffsetY(this Transform transform, float y)
	{
		transform.position = transform.position.OffsetY(y);
	}


	public static Vector3 OffsetZ(this Vector3 vector, float z)
	{
		return new Vector3(vector.x, vector.y, vector.z + z);
	}

	public static void OffsetZ(this Transform transform, float z)
	{
		transform.position = transform.position.OffsetZ(z);
	}


	public static Vector3 OffsetXY(this Vector3 vector, float x, float y)
	{
		return new Vector3(vector.x + x, vector.y + y, vector.z);
	}

	public static void OffsetXY(this Transform transform, float x, float y)
	{
		transform.position = transform.position.OffsetXY(x, y);
	}

	public static Vector2 OffsetXY(this Vector2 vector, float x, float y)
	{
		return new Vector2(vector.x + x, vector.y + y);
	}


	public static Vector3 OffsetXZ(this Vector3 vector, float x, float z)
	{
		return new Vector3(vector.x + x, vector.y, vector.z + z);
	}

	public static void OffsetXZ(this Transform transform, float x, float z)
	{
		transform.position = transform.position.OffsetXZ(x, z);
	}


	public static Vector3 OffsetYZ(this Vector3 vector, float y, float z)
	{
		return new Vector3(vector.x, vector.y + y, vector.z + z);
	}

	public static void OffsetYZ(this Transform transform, float y, float z)
	{
		transform.position = transform.position.OffsetYZ(y, z);
	}

	#endregion


	#region Clamp X/Y

	public static Vector3 ClampX(this Vector3 vector, float min, float max)
	{
		return vector.SetX(Mathf.Clamp(vector.x, min, max));
	}

	public static Vector2 ClampX(this Vector2 vector, float min, float max)
	{
		return vector.SetX(Mathf.Clamp(vector.x, min, max));
	}

	public static void ClampX(this Transform transform, float min, float max)
	{
		transform.SetX(Mathf.Clamp(transform.position.x, min, max));
	}


	public static Vector3 ClampY(this Vector3 vector, float min, float max)
	{
		return vector.SetY(Mathf.Clamp(vector.x, min, max));
	}

	public static Vector2 ClampY(this Vector2 vector, float min, float max)
	{
		return vector.SetY(Mathf.Clamp(vector.x, min, max));
	}

	public static void ClampY(this Transform transform, float min, float max)
	{
		transform.SetY(Mathf.Clamp(transform.position.x, min, max));
	}

	#endregion


	#region Invert

	public static Vector2 InvertX(this Vector2 vector)
	{
		return new Vector2(-vector.x, vector.y);
	}

	public static Vector2 InvertY(this Vector2 vector)
	{
		return new Vector2(vector.x, -vector.y);
	}

	#endregion


	#region Convert

	public static Vector2 ToVector2(this Vector3 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	public static Vector3 ToVector3(this Vector2 vector)
	{
		return new Vector3(vector.x, vector.y);
	}


	public static Vector2 ToVector2(this Vector2Int vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	public static Vector3 ToVector3(this Vector3Int vector)
	{
		return new Vector3(vector.x, vector.y);
	}


	public static Vector2Int ToVector2Int(this Vector2 vector)
	{
		return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	}

	public static Vector3Int ToVector3Int(this Vector3 vector)
	{
		return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
	}

	#endregion


	#region Snap

	/// <summary>
	/// Snap to grid of snapValue
	/// </summary>
	public static Vector3 SnapValue(this Vector3 val, float snapValue)
	{
		return new Vector3(
			MathX.Snap(val.x, snapValue),
			MathX.Snap(val.y, snapValue),
			MathX.Snap(val.z, snapValue));
	}

	/// <summary>
	/// Snap to grid of snapValue
	/// </summary>
	public static Vector2 SnapValue(this Vector2 val, float snapValue)
	{
		return new Vector2(
			MathX.Snap(val.x, snapValue),
			MathX.Snap(val.y, snapValue));
	}

	/// <summary>
	/// Snap position to grid of snapValue
	/// </summary>
	public static void SnapPosition(this Transform transform, float snapValue)
	{
		transform.position = transform.position.SnapValue(snapValue);
	}

	/// <summary>
	/// Snap to one unit grid
	/// </summary>
	public static Vector2 SnapToOne(this Vector2 vector)
	{
		return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
	}

	/// <summary>
	/// Snap to one unit grid
	/// </summary>
	public static Vector3 SnapToOne(this Vector3 vector)
	{
		return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
	}

	#endregion


	#region Average

	public static Vector3 AverageVector(this Vector3[] vectors)
	{
		if (vectors.IsNullOrEmpty()) return Vector3.zero;

		float x = 0f, y = 0f, z = 0f;
		for (var i = 0; i < vectors.Length; i++)
		{
			x += vectors[i].x;
			y += vectors[i].y;
			z += vectors[i].z;
		}

		return new Vector3(x / vectors.Length, y / vectors.Length, z / vectors.Length);
	}

	public static Vector2 AverageVector(this Vector2[] vectors)
	{
		if (vectors.IsNullOrEmpty()) return Vector2.zero;

		float x = 0f, y = 0f;
		for (var i = 0; i < vectors.Length; i++)
		{
			x += vectors[i].x;
			y += vectors[i].y;
		}

		return new Vector2(x / vectors.Length, y / vectors.Length);
	}

	#endregion


	#region Approximately

	public static bool Approximately(this Vector3 vector, Vector3 compared, float threshold = 0.1f)
	{
		var xDiff = Mathf.Abs(vector.x - compared.x);
		var yDiff = Mathf.Abs(vector.y - compared.y);
		var zDiff = Mathf.Abs(vector.z - compared.z);

		return xDiff <= threshold && yDiff <= threshold && zDiff <= threshold;
	}

	public static bool Approximately(this Vector2 vector, Vector2 compared, float threshold = 0.1f)
	{
		var xDiff = Mathf.Abs(vector.x - compared.x);
		var yDiff = Mathf.Abs(vector.y - compared.y);

		return xDiff <= threshold && yDiff <= threshold;
	}

	#endregion


	#region Get Closest

	/// <summary>
	/// Finds the position closest to the given one.
	/// </summary>
	/// <param name="position">World position.</param>
	/// <param name="otherPositions">Other world positions.</param>
	/// <returns>Closest position.</returns>
	public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
	{
		var closest = Vector3.zero;
		var shortestDistance = Mathf.Infinity;

		foreach (var otherPosition in otherPositions)
		{
			var distance = (position - otherPosition).sqrMagnitude;

			if (distance < shortestDistance)
			{
				closest = otherPosition;
				shortestDistance = distance;
			}
		}

		return closest;
	}

	public static Vector3 GetClosest(this IEnumerable<Vector3> positions, Vector3 position)
	{
		return position.GetClosest(positions);
	}

	#endregion


	#region To

	/// <summary>
	/// Get vector from source to destination
	/// </summary>
	public static Vector4 To(this Vector4 source, Vector4 destination) =>
		destination - source;

	/// <summary>
	/// Get vector from source to destination
	/// </summary>
	public static Vector3 To(this Vector3 source, Vector3 destination) =>
		destination - source;

	/// <summary>
	/// Get vector from source to destination
	/// </summary>
	public static Vector2 To(this Vector2 source, Vector2 destination) =>
		destination - source;

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this Component source, Component target) =>
		source.transform.position.To(target.transform.position);

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this Component source, GameObject target) =>
		source.transform.position.To(target.transform.position);

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this GameObject source, Component target) =>
		source.transform.position.To(target.transform.position);

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this GameObject source, GameObject target) =>
		source.transform.position.To(target.transform.position);

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this Vector3 source, GameObject target) =>
		source.To(target.transform.position);

	/// <summary>
	/// Get vector from source to target
	/// </summary>
	public static Vector3 To(this Vector3 source, Component target) =>
		source.To(target.transform.position);

	/// <summary>
	/// Get vector from source to destination
	/// </summary>
	public static Vector3 To(this GameObject source, Vector3 destination) =>
		source.transform.position.To(destination);

	/// <summary>
	/// Get vector from source to destination
	/// </summary>
	public static Vector3 To(this Component source, Vector3 destination) =>
		source.transform.position.To(destination);

	#endregion


	#region Pow

	/// <summary>
	/// Raise each component of the source Vector2 to the specified power.
	/// </summary>
	public static Vector2 Pow(this Vector2 source, float exponent) =>
		new Vector2(Mathf.Pow(source.x, exponent),
			Mathf.Pow(source.y, exponent));

	/// <summary>
	/// Raise each component of the source Vector3 to the specified power.
	/// </summary>
	public static Vector3 Pow(this Vector3 source, float exponent) =>
		new Vector3(Mathf.Pow(source.x, exponent),
			Mathf.Pow(source.y, exponent),
			Mathf.Pow(source.z, exponent));

	/// <summary>
	/// Raise each component of the source Vector3 to the specified power.
	/// </summary>
	public static Vector4 Pow(this Vector4 source, float exponent) =>
		new Vector4(Mathf.Pow(source.x, exponent),
			Mathf.Pow(source.y, exponent),
			Mathf.Pow(source.z, exponent),
			Mathf.Pow(source.w, exponent));

	#endregion


	#region ScaleBy

	/// <summary>
	/// Immutably returns the result of the source vector multiplied with
	/// another vector component-wise.
	/// </summary>
	public static Vector2 ScaleBy(this Vector2 source, Vector2 right) =>
		Vector2.Scale(source, right);

	/// <summary>
	/// Immutably returns the result of the source vector multiplied with
	/// another vector component-wise.
	/// </summary>
	public static Vector3 ScaleBy(this Vector3 source, Vector3 right) =>
		Vector3.Scale(source, right);

	/// <summary>
	/// Immutably returns the result of the source vector multiplied with
	/// another vector component-wise.
	/// </summary>
	public static Vector4 ScaleBy(this Vector4 source, Vector4 right) =>
		Vector4.Scale(source, right);

	#endregion
	
	public static Vector2 Rotate(this Vector2 vector, float angle, Vector2 pivot = default(Vector2))
	{
		Vector2 rotated = Quaternion.Euler(new Vector3(0f, 0f, angle)) * (vector - pivot);
		return rotated + pivot;
	}

	public static void Deconstruct(this Vector2 v2, out float x, out float y)
	{
		x = v2.x;
		y = v2.y;
	}
	
	public static Vector3 down(this Vector3 obj)
	{
		return -Vector3.up;
	}

	public static Vector3 left(this Vector3 obj)
	{
		return -Vector3.right;
	}

	public static Vector3 backward(this Vector3 obj)
	{
		return -Vector3.forward;
	}
	
	/// <summary>
/// gets the square distance between two vector3 positions. this is much faster that Vector3.distance.
/// </summary>
/// <param name="first">first point</param>
/// <param name="second">second point</param>
/// <returns>squared distance</returns>
public static float SqrDistance(this Vector3 first, Vector3 second)
{
	return (first.x - second.x) * (first.x - second.x) +
		  (first.y - second.y) * (first.y - second.y) +
		  (first.z - second.z) * (first.z - second.z);
}

/// <summary>
///
/// </summary>
/// <param name="first"></param>
/// <param name="second"></param>
/// <returns></returns>
public static Vector3 MidPoint(this Vector3 first, Vector3 second)
{
	return new Vector3((first.x + second.x) * 0.5f, (first.y + second.y) * 0.5f, (first.z + second.z) * 0.5f);
}

/// <summary>
/// get the square distance from a point to a line segment.
/// </summary>
/// <param name="point">point to get distance to</param>
/// <param name="lineP1">line segment start point</param>
/// <param name="lineP2">line segment end point</param>
/// <param name="closestPoint">set to either 1, 2, or 4, determining which end the point is closest to (p1, p2, or the middle)</param>
/// <returns></returns>
public static float SqrLineDistance(this Vector3 point, Vector3 lineP1, Vector3 lineP2, out int closestPoint)
{
	Vector3 v = lineP2 - lineP1;
	Vector3 w = point - lineP1;

	float c1 = Vector3.Dot(w, v);

	if (c1 <= 0) //closest point is p1
	{
		closestPoint = 1;
		return SqrDistance(point, lineP1);
	}

	float c2 = Vector3.Dot(v, v);
	if (c2 <= c1) //closest point is p2
	{
		closestPoint = 2;
		return SqrDistance(point, lineP2);
	}

	float b = c1 / c2;

	Vector3 pb = lineP1 + b * v;
	{
		closestPoint = 4;
		return SqrDistance(point, pb);
	}
}

/// <summary>
/// Absolute value of components
/// </summary>
/// <param name="v"></param>
/// <returns></returns>
public static Vector3 Abs(this Vector3 v)
{
	return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
}

/// <summary>
/// Vector3.Project, onto a plane
/// </summary>
/// <param name="v"></param>
/// <param name="planeNormal"></param>
/// <returns></returns>
public static Vector3 ProjectOntoPlane(this Vector3 v, Vector3 planeNormal)
{
	return v - Vector3.Project(v, planeNormal);
}

/// <summary>
/// Gets the normal of the triangle formed by the 3 vectors
/// </summary>
/// <param name="vec1"></param>
/// <param name="vec2"></param>
/// <param name="vec3"></param>
/// <returns></returns>
public static Vector3 Vector3Normal(this Vector3 vec1, Vector3 vec2, Vector3 vec3)
{
	return Vector3.Cross((vec3 - vec1), (vec2 - vec1));
}

/// <summary>
/// Gets the center of two points
/// </summary>
/// <param name="vec1"></param>
/// <param name="vec2"></param>
/// <returns></returns>
public static Vector3 Center(this Vector3 vec1, Vector3 vec2)
{
	return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, (vec1.z + vec2.z) / 2);
}

/// <summary>
///
/// </summary>
/// <param name="vec"></param>
/// <returns></returns>
public static bool IsNaN(this Vector3 vec)
{
	return float.IsNaN(vec.x * vec.y * vec.z);
}

/// <summary>
///
/// </summary>
/// <param name="points"></param>
/// <returns></returns>
public static Vector3 Center(this Vector3[] points)
{
	Vector3 ret = Vector3.zero;
	foreach (var p in points)
	{
		ret += p;
	}
	ret /= points.Length;
	return ret;
}

/// <summary>
///
/// </summary>
/// <param name="dir1"></param>
/// <param name="dir2"></param>
/// <param name="axis"></param>
/// <returns></returns>
public static float AngleAroundAxis(this Vector3 dir1, Vector3 dir2, Vector3 axis)
{
	dir1 = dir1 - Vector3.Project(dir1, axis);
	dir2 = dir2 - Vector3.Project(dir2, axis);

	float angle = Vector3.Angle(dir1, dir2);
	return angle * (Vector3.Dot(axis, Vector3.Cross(dir1, dir2)) < 0 ? -1 : 1);
}

/// <summary>
/// test if a Vector3 is close to another Vector3 (due to floating point inprecision)
/// compares the square of the distance to the square of the range as this
/// avoids calculating a square root which is much slower than squaring the range
/// </summary>
/// <param name="val"></param>
/// <param name="about"></param>
/// <param name="range"></param>
/// <returns></returns>
public static bool Approx(this Vector3 val, Vector3 about, float range)
{
	return ((val - about).sqrMagnitude < range * range);
}

/// <summary>
/// Find a point on the infinite line nearest to point
/// </summary>
/// <param name="lineStart"></param>
/// <param name="lineEnd"></param>
/// <param name="point"></param>
/// <returns></returns>
public static Vector3 NearestPoint(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
{
	Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
	float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
	return lineStart + (closestPoint * lineDirection);
}

public static void Deconstruct(this Vector3 v3, out float x, out float y, out float z)
{
	x = v3.x;
	y = v3.y;
	z = v3.z;
}

	#region Add


	/// <summary>
	/// Adds two Vector3s
	/// </summary>
	/// <param name="v3">source vector3</param>
	/// <param name="value">second vector3</param>
	/// <remarks>
	/// Suggested by: aaro4130
	/// Link: http://forum.unity3d.com/members/aaro4130.22011/
	/// </remarks>
	public static Vector3 Add(this Vector3 v3, Vector3 value)
	{
		return v3 + value;
	}

	/// <summary>
	/// Adds the values to a vector3
	/// </summary>
	/// <param name="v3">source vector3</param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <remarks>
	/// Suggested by: aaro4130
	/// Link: http://forum.unity3d.com/members/aaro4130.22011/
	/// </remarks>
	public static Vector3 Add(this Vector3 v3, float x, float y, float z)
	{
		return v3 + new Vector3(x, y, z);
	}

	// Add
	#endregion

	#region Subtract

	/// <summary>
	/// Subtracts two Vector3s
	/// </summary>
	/// <param name="v3">source vector3</param>
	/// <param name="value">second vector3</param>
	/// <returns></returns>
	/// <remarks>
	/// Suggested by: aaro4130
	/// Link: http://forum.unity3d.com/members/aaro4130.22011/
	/// </remarks>
	public static Vector3 Subtract(this Vector3 v3, Vector3 value)
	{
		return v3 - value;
	}

	/// <summary>
	/// Subtracts the values from a vector 3
	/// </summary>
	/// <param name="v3">source vector3</param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	/// <remarks>
	/// Suggested by: aaro4130
	/// Link: http://forum.unity3d.com/members/aaro4130.22011/
	/// </remarks>
	public static Vector3 Subtract(this Vector3 v3, float x, float y, float z)
	{
		return v3 - new Vector3(x, y, z);
	}

	// Subtract
	#endregion

	public static Vector2 GetAnglesTo(this Vector3 referenceVector, Vector3 compareVector)
	=> new Vector2(-Mathf.Asin(Vector3.Cross(compareVector, referenceVector).y) * Mathf.Rad2Deg,
		-Mathf.Asin(Vector3.Cross(compareVector, referenceVector).x) * Mathf.Rad2Deg);

public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Quaternion rotation) => rotation * (point - pivot) + pivot;

	/// <summary>
	/// Translates, rotates and scales the <paramref name="vector"/> by the position, rotation and scale of the transform.
	/// </summary>
	/// <param name="vector">Vector to transform.</param>
	/// <param name="transform">Transform to be applied.</param>
	/// <returns>Transformed vector.</returns>
	public static Vector3 ApplyTransform(this Vector3 vector, Transform transform) => vector.Transform(transform.position, transform.rotation, transform.lossyScale);

	public static Vector3 Transform(this Vector3 vector, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		vector = Vector3.Scale(vector, new Vector3(scale.x, scale.y, scale.z));
		vector = vector.RotateAround(Vector3.zero, rotation);
		vector += position;
		return vector;
	}

	public static Vector3 InverseApplyTransform(this Vector3 vector, Transform transform) => vector.InverseTransform(transform.position, transform.rotation, transform.lossyScale);

	public static Vector3 InverseTransform(this Vector3 vector, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		vector -= position;
		vector = vector.RotateAround(Vector3.zero, Quaternion.Inverse(rotation));
		vector = Vector3.Scale(vector, new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z));
		return vector;
	}

	public static bool NearlyEquals(this Vector3 lhs, Vector3 rhs, double inaccuracy = 9.99999943962493E-11) => Vector3.SqrMagnitude(lhs - rhs) < inaccuracy;

	public static Vector3 MidPointTo(this Vector3 origin, Vector3 destination) => new Vector3(
																							  (origin.x + destination.x) / 2,
																							  (origin.y + destination.y) / 2,
																							  (origin.z + destination.z) / 2
																							 );

	public static bool IsInside(this Vector3 vector, Collider collider) => vector == collider.ClosestPoint(vector);

	/// <summary>
	/// Transforms a <paramref name="worldPoint"/> seen by <paramref name="worldCamera"/> to a screen point within the specified <paramref name="canvas"/>.
	/// Supports only <see cref="RenderMode.ScreenSpaceCamera"/> and <see cref="RenderMode.ScreenSpaceOverlay"/> render modes for the <paramref name="canvas"/>.
	/// Returns <see cref="Vector3.negativeInfinity"/> if the <paramref name="worldPoint"/> is not in front of the <paramref name="worldCamera"/>.
	/// </summary>
	/// <param name="worldPoint">Vector3 to be transformed to a screen point.</param>
	/// <param name="worldCamera">Camera looking at the <paramref name="worldPoint"/>.</param>
	/// <param name="canvas">Target canvas of the screen point.</param>
	/// <returns>A Vector3 within the specified <paramref name="canvas"/> that is in the same screen position as the <paramref name="worldPoint"/>.</returns>
	/// <exception cref="NotImplementedException"><paramref name="canvas"/> has an unsupported RenderMode.</exception>
	/// <example>
	/// This sample shows how to call the <see cref="WorldToScreenPointInCanvas"/> method to set the position of a UI image.
	/// <code>
	/// var screenPoint = targetObject.transform.position.WorldToScreenPointInCanvas(Camera.main, canvas);
	/// if(screenPoint != Vector3.negativeInfinity) uiImage.RectTransform.position = screenPoint;
	/// </code>
	/// </example>
	public static Vector3 WorldToScreenPointInCanvas(this Vector3 worldPoint, Camera worldCamera, Canvas canvas)
	{
		var direction = worldPoint - worldCamera.transform.position;
		if (!(Vector3.Dot(worldCamera.transform.forward, direction) > 0.0f))
		{
			return Vector3.negativeInfinity;
		}

		var screenPoint = worldCamera.WorldToScreenPoint(worldPoint);
		switch (canvas.renderMode)
		{
			case RenderMode.ScreenSpaceCamera:
				return MathUtils.ScreenPointToLocalPointInRectangle(canvas, position: screenPoint);
			case RenderMode.ScreenSpaceOverlay:
				return screenPoint;
			default:
				throw new NotImplementedException("RenderMode not Supported.");
		}
	}

	public static string ToStringVerbose(this Vector3 v) => $"({v.x}, {v.y}, {v.z})";
    
    [Flags]
    public enum VectorAxesMask
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4,
        XY = X | Y,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z
    }

    public static Vector2 SetValues(this Vector2 vector, Vector2 values, VectorAxesMask vectorAxesMask)
    {
        if ((vectorAxesMask & VectorAxesMask.X) != VectorAxesMask.None)
        {
            vector.x = values.x;
        }
        if ((vectorAxesMask & VectorAxesMask.Y) != VectorAxesMask.None)
        {
            vector.y = values.y;
        }
        return vector;
    }

    public static Vector2 SetValues(this Vector2 vector, float value, VectorAxesMask vectorAxesMask)
    {
        return vector.SetValues(new Vector2(value, value), vectorAxesMask);
    }

    public static Vector3 SetValues(this Vector3 vector, Vector3 values, VectorAxesMask vectorAxesMask)
    {
        if ((vectorAxesMask & VectorAxesMask.X) != VectorAxesMask.None)
        {
            vector.x = values.x;
        }
        if ((vectorAxesMask & VectorAxesMask.Y) != VectorAxesMask.None)
        {
            vector.y = values.y;
        }
        if ((vectorAxesMask & VectorAxesMask.Z) != VectorAxesMask.None)
        {
            vector.z = values.z;
        }
        return vector;
    }

    public static Vector3 SetValues(this Vector3 vector, float value, VectorAxesMask vectorAxesMask)
    {
        return vector.SetValues(new Vector3(value, value, value), vectorAxesMask);
    }

    public static Vector3 ToVector3(this Vector2 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 ClampMagnitude(this Vector2 vector, float min, float max)
    {
        var result = vector;
        var sqrMagnitude = vector.sqrMagnitude;
        var num = min * min;
        var num2 = max * max;
        if (sqrMagnitude < num)
        {
            result = vector.normalized * min;
        }
        else if (sqrMagnitude > num2)
        {
            result = vector.normalized * max;
        }
        return result;
    }

    public static Vector3 ClampMagnitude(this Vector3 vector, float min, float max)
    {
        var result = vector;
        var sqrMagnitude = vector.sqrMagnitude;
        var num = min * min;
        var num2 = max * max;
        if (sqrMagnitude < num)
        {
            result = vector.normalized * min;
        }
        else if (sqrMagnitude > num2)
        {
            result = vector.normalized * max;
        }
        return result;
    }

    public static float Distance2D(this Vector3 v, Vector3 other)
    {
        return Vector2.Distance(new Vector2(v.x, v.y), new Vector2(other.x, other.y));
    }

    public static float DistanceSqr2D(this Vector3 v, Vector3 other)
    {
        return (new Vector2(v.x, v.y) - new Vector2(other.x, other.y)).sqrMagnitude;
    }

    public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
    }

    public static Vector2 Clamp(this Vector2 v, Rect rect)
    {
        return new Vector2(Mathf.Clamp(v.x, rect.xMin, rect.xMax), Mathf.Clamp(v.y, rect.yMin, rect.yMax));
    }

    public static Vector2 Clamp(this Vector2 v, float xMin, float yMin, float xMax, float yMax)
    {
        return new Vector2(Mathf.Clamp(v.x, xMin, xMax), Mathf.Clamp(v.y, yMin, yMax));
    }

    public static Vector2 Rotate(this Vector2 v, float radian)
    {
        var num = Mathf.Sin(radian);
        var num2 = Mathf.Cos(radian);
        var x = v.x;
        var y = v.y;
        v.x = num2 * x - num * y;
        v.y = num * x + num2 * y;
        return v;
    }

    public static Vector2 MoveToward(this Vector2 v, Vector2 target, ref float speed, float maxSpeed, float accel, float deccel, float deltaTime, out bool finished)
    {
        var vector = target - v;
        var num = 1;
        var b = maxSpeed;
        var num2 = speed / deccel;
        var num3 = speed * num2 / 2f;
        if (num3 * num3 > vector.sqrMagnitude)
        {
            num = -1;
        }
        else
        {
            var magnitude = vector.magnitude;
            var num4 = Mathf.Sqrt(magnitude * 2f / deccel);
            b = num4 * deccel;
        }
        speed = Mathf.Clamp(speed + ((num != 1) ? (-deccel * deltaTime) : (accel * deltaTime)), 0f, Mathf.Min(maxSpeed, b));
        var b2 = vector.normalized * (speed * deltaTime);
        if (b2.sqrMagnitude >= vector.sqrMagnitude)
        {
            speed = 0f;
            v = target;
            finished = true;
            return v;
        }
        v += b2;
        finished = false;
        return v;
    }

    public static Vector3 MoveToward(this Vector3 v, Vector3 target, ref float speed, float maxSpeed, float accel, float deccel, float deltaTime, out bool finished)
    {
        var vector = target - v;
        var num = 1;
        var b = maxSpeed;
        var num2 = speed / deccel;
        var num3 = speed * num2 / 2f;
        if (num3 * num3 > vector.sqrMagnitude)
        {
            num = -1;
        }
        else
        {
            var magnitude = vector.magnitude;
            var num4 = Mathf.Sqrt(magnitude * 2f / deccel);
            b = num4 * deccel;
        }
        speed = Mathf.Clamp(speed + ((num != 1) ? (-deccel * deltaTime) : (accel * deltaTime)), 0f, Mathf.Min(maxSpeed, b));
        var b2 = vector.normalized * (speed * deltaTime);
        if (b2.sqrMagnitude >= vector.sqrMagnitude)
        {
            speed = 0f;
            v = target;
            finished = true;
            return v;
        }
        v += b2;
        finished = false;
        return v;
    }

    public static Vector2 MoveToward(this Vector2 v, Vector2 target, ref Vector2 speed, float maxSpeed, float accel, float deltatime, out bool finished)
    {
        var a = target - v;
        a.Normalize();
        speed += a * accel * deltatime;
        if (speed.sqrMagnitude > maxSpeed * maxSpeed)
        {
            speed = speed.normalized * maxSpeed;
        }
        var vector = v + speed * deltatime;
        Vector3 v2 = speed;
        var f = Vector2.Dot(v2, (target - v).normalized);
        var f2 = Vector2.Dot(v2, (target - vector).normalized);
        finished = (Mathf.Sign(f) != Mathf.Sign(f2));
        v = vector;
        return v;
    }

	#region ToV3String

	/// <summary>
	/// Converts a Vector3 to a string in X, Y, Z format
	/// </summary>
	/// <param name="v3"></param>
	/// <returns></returns>
	public static string ToV3String(this Vector3 v3)
	{
		return string.Format("{0}, {1}, {2}", v3.x, v3.y, v3.z);
	}

	// ToV3String
	#endregion

	#region RotateAroundY

	/// <summary>
	/// Rotates goV3 around the vector v3, keeping y in the original position
	/// </summary>
	/// <param name="v3"></param>
	/// <param name="goV3">the game object's transform, which will be rotating</param>
	/// <returns></returns>
	public static Vector3 RotateAroundY(this Vector3 v3, Vector3 goV3)
	{
		return new Vector3(v3.x, goV3.y, v3.z);
	}

	// RotateAroundY
	#endregion

	#region StringToBytes

	/// <summary>
	/// Converts a string to bytes, in a Unity friendly way
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static byte[] StringToBytes(this string source)
	{
		// exit if null
		if (string.IsNullOrEmpty(source))
			return null;

		// convert to bytes
		using (MemoryStream compMemStream = new MemoryStream())
		{
			using (StreamWriter writer = new StreamWriter(compMemStream, Encoding.UTF8))
			{
				writer.Write(source);
				writer.Close();

				return compMemStream.ToArray();
			}
		}
	}

	// StringToBytes
	#endregion

	#region BytesToString

	/// <summary>
	/// Converts a byte array to a Unicode string, in a Unity friendly way
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static string BytesToString(this byte[] source)
	{
		// exit if null
		if (source.IsNullOrEmpty())
			return string.Empty;

		// read from bytes
		using (MemoryStream compMemStream = new MemoryStream(source))
		{
			using (StreamReader reader = new StreamReader(compMemStream, Encoding.UTF8))
			{
				return reader.ReadToEnd();
			}
		}
	}

	// BytesToString
	#endregion

	/// <summary>
	/// Returns a vector from the element calling the method to the parameter target.
	/// </summary>
	/// <returns></returns>
	public static Vector3 RelativePosTo(this Component origin, Component target) {
		return (target.transform.position - origin.transform.position);
	}

	/// <summary>
	/// Returns a vector from the element calling the method to the parameter target.
	/// </summary>
	public static Vector3 RelativePosTo(this Component origin, Vector3 target) {
		return (target - origin.transform.position);
	}

	/// <summary>
	/// Converts a vector3 to a Vector2 using x and z values instead of x and y.
	/// </summary>
	public static Vector2 ToVector2Z(this Vector3 vector) => new Vector2(vector.x, vector.z);

	/// <summary>
	/// Returns the vector with its x value set to 0.
	/// </summary>
	public static Vector3 ZeroX(this Vector3 vector) {
		vector.x = 0;
		return vector;
	}

	/// <summary>
	/// Returns the vector with its y value set to 0.
	/// </summary>
	public static Vector3 ZeroY(this Vector3 vector) {
		vector.y = 0;
		return vector;
	}

	/// <summary>
	/// Returns the vector with its z value set to 0.
	/// </summary>
	public static Vector3 ZeroZ(this Vector3 vector) {
		vector.z = 0;
		return vector;
	}

	/// <summary>
	/// Returns the vector with its x value set to 1.
	/// </summary>
	public static Vector3 OneX(this Vector3 vector) {
		vector.x = 1;
		return vector;
	}

	/// <summary>
	/// Returns the vector with its y value set to 1.
	/// </summary>
	public static Vector3 OneY(this Vector3 vector) {
		vector.y = 1;
		return vector;
	}

	/// <summary>
	/// Returns the vector with its z value set to 1.
	/// </summary>
	public static Vector3 OneZ(this Vector3 vector) {
		vector.z = 1;
		return vector;
	}

	public static Vector2 WithX(this Vector2 vec, float x) {
		return new Vector2(x, vec.y);
	}

	public static Vector2 WithY(this Vector2 vec, float y) {
		return new Vector2(vec.x, y);
	}

	public static Vector2 AddX(this Vector2 vec, float x) {
		return new Vector2(vec.x + x, vec.y);
	}

	public static Vector2 AddY(this Vector2 vec, float y) {
		return new Vector2(vec.x, vec.y + y);
	}

	public static Vector2 Invert(this Vector2 vec) {
		return new Vector2(-vec.x, -vec.y);
	}

	public static Vector2 Abs(this Vector2 vec) {
		return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
	}

	public static Vector3 WithX(this Vector3 vec, float x) {
		return new Vector3(x, vec.y, vec.z);
	}

	public static Vector3 WithY(this Vector3 vec, float y) {
		return new Vector3(vec.x, y, vec.z);
	}

	public static Vector3 WithZ(this Vector3 vec, float z) {
		return new Vector3(vec.x, vec.y, z);
	}

	public static Vector3 AddX(this Vector3 vec, float x) {
		return new Vector3(vec.x + x, vec.y, vec.z);
	}

	public static Vector3 AddY(this Vector3 vec, float y) {
		return new Vector3(vec.x, vec.y + y, vec.z);
	}

	public static Vector3 AddZ(this Vector3 vec, float z) {
		return new Vector3(vec.x, vec.y, vec.z + z);
	}

	public static Vector3 InvertX(this Vector3 vec) {
		return new Vector3(-vec.x, vec.y, vec.z);
	}

	public static Vector3 InvertY(this Vector3 vec) {
		return new Vector3(vec.x, -vec.y, vec.z);
	}

	public static Vector3 InvertZ(this Vector3 vec) {
		return new Vector3(vec.x, vec.y, -vec.z);
	}

	public static Vector3 Invert(this Vector3 vec) {
		return new Vector3(-vec.x, -vec.y, -vec.z);
	}


	public static Vector2Int WithX(this Vector2Int vec, int x) {
		return new Vector2Int(x, vec.y);
	}

	public static Vector2Int WithY(this Vector2Int vec, int y) {
		return new Vector2Int(vec.x, y);
	}

	public static Vector2Int AddX(this Vector2Int vec, int x) {
		return new Vector2Int(vec.x + x, vec.y);
	}

	public static Vector2Int AddY(this Vector2Int vec, int y) {
		return new Vector2Int(vec.x, vec.y + y);
	}

	public static Vector2Int InvertX(this Vector2Int vec) {
		return new Vector2Int(-vec.x, vec.y);
	}

	public static Vector2Int InvertY(this Vector2Int vec) {
		return new Vector2Int(vec.x, -vec.y);
	}

	public static Vector2Int Invert(this Vector2Int vec) {
		return new Vector2Int(-vec.x, -vec.y);
	}

	public static Vector2Int Abs(this Vector2Int vec) {
		return new Vector2Int(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
	}

	public static Vector3Int WithX(this Vector3Int vec, int x) {
		return new Vector3Int(x, vec.y, vec.z);
	}

	public static Vector3Int WithY(this Vector3Int vec, int y) {
		return new Vector3Int(vec.x, y, vec.z);
	}

	public static Vector3Int WithZ(this Vector3Int vec, int z) {
		return new Vector3Int(vec.x, vec.y, z);
	}

	public static Vector3Int AddX(this Vector3Int vec, int x) {
		return new Vector3Int(vec.x + x, vec.y, vec.z);
	}

	public static Vector3Int AddY(this Vector3Int vec, int y) {
		return new Vector3Int(vec.x, vec.y + y, vec.z);
	}

	public static Vector3Int AddZ(this Vector3Int vec, int z) {
		return new Vector3Int(vec.x, vec.y, vec.z + z);
	}

	public static Vector3Int InvertX(this Vector3Int vec) {
		return new Vector3Int(-vec.x, vec.y, vec.z);
	}

	public static Vector3Int InvertY(this Vector3Int vec) {
		return new Vector3Int(vec.x, -vec.y, vec.z);
	}

	public static Vector3Int InvertZ(this Vector3Int vec) {
		return new Vector3Int(vec.x, vec.y, -vec.z);
	}

	public static Vector3Int Invert(this Vector3Int vec) {
		return new Vector3Int(-vec.x, -vec.y, -vec.z);
	}

	public static Vector3Int Abs(this Vector3Int vec) {
		return new Vector3Int(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
	}

	public static Vector3 ToVector3(this Vector2Int v) {
		return new Vector3(v.x, v.y);
	}

	public static Vector2 ToVector2(this Vector3Int v) {
		return new Vector2(v.x, v.y);
	}

	public static Vector3Int ToVector3Int(this Vector2 v) {
		return new Vector3Int((int) v.x, (int) v.y, 0);
	}

	public static Vector2Int ToVector2Int(this Vector3 v) {
		return new Vector2Int((int) v.x, (int) v.y);
	}
}
