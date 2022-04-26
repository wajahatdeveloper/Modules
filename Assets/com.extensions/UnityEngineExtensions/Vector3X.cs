using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Vector3 extensions and helper functions
/// </summary>
public static class Vector3X
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

	public static Vector3 ScaleByValue(this Vector3 obj, float n)
	{
		return obj *= n;
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
	public static Vector3 Vector3Normal(Vector3 vec1, Vector3 vec2, Vector3 vec3)
	{
		return Vector3.Cross((vec3 - vec1), (vec2 - vec1));
	}

	///// <summary>
	///// Using the magic of 0x5f3759df
	///// </summary>
	///// <param name="vec1"></param>
	///// <returns></returns>
	//public static Vector3 FastNormalized(this Vector3 vec1)
	//{
	//    var componentMult = MathX.FastInvSqrt(vec1.sqrMagnitude);
	//    return new Vector3(vec1.x*componentMult, vec1.y*componentMult, vec1.z*componentMult);
	//}

	/// <summary>
	/// Gets the center of two points
	/// </summary>
	/// <param name="vec1"></param>
	/// <param name="vec2"></param>
	/// <returns></returns>
	public static Vector3 Center(Vector3 vec1, Vector3 vec2)
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
	public static float AngleAroundAxis(Vector3 dir1, Vector3 dir2, Vector3 axis)
	{
		dir1 = dir1 - Vector3.Project(dir1, axis);
		dir2 = dir2 - Vector3.Project(dir2, axis);

		float angle = Vector3.Angle(dir1, dir2);
		return angle * (Vector3.Dot(axis, Vector3.Cross(dir1, dir2)) < 0 ? -1 : 1);
	}

	/// <summary>
	/// Returns a random direction in a cone. a spread of 0 is straight, 0.5 is 180*
	/// </summary>
	/// <param name="spread"></param>
	/// <param name="forward">must be unit</param>
	/// <returns></returns>
	public static Vector3 RandomDirection(float spread, Vector3 forward)
	{
		return Vector3.Slerp(forward, Random.onUnitSphere, spread);
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
	public static bool Approx(Vector3 val, Vector3 about, float range)
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
	public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
		float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
		return lineStart + (closestPoint * lineDirection);
	}

	/// <summary>
	/// find a point on the line segment nearest to point
	/// </summary>
	/// <param name="lineStart"></param>
	/// <param name="lineEnd"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 fullDirection = lineEnd - lineStart;
		Vector3 lineDirection = Vector3.Normalize(fullDirection);
		float closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);
		return lineStart + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
	}

	/// <summary>
	/// Calculates the intersection line segment between 2 lines (not segments).
	/// Returns false if no solution can be found.
	/// </summary>
	/// <returns></returns>
	public static bool CalculateLineLineIntersection(Vector3 line1Point1, Vector3 line1Point2,
		Vector3 line2Point1, Vector3 line2Point2, out Vector3 resultSegmentPoint1, out Vector3 resultSegmentPoint2)
	{
		// Algorithm is ported from the C algorithm of
		// Paul Bourke at http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline3d/
		resultSegmentPoint1 = new Vector3(0, 0, 0);
		resultSegmentPoint2 = new Vector3(0, 0, 0);

		var p1 = line1Point1;
		var p2 = line1Point2;
		var p3 = line2Point1;
		var p4 = line2Point2;
		var p13 = p1 - p3;
		var p43 = p4 - p3;

		if (p4.sqrMagnitude < float.Epsilon)
		{
			return false;
		}
		var p21 = p2 - p1;
		if (p21.sqrMagnitude < float.Epsilon)
		{
			return false;
		}

		var d1343 = p13.x * p43.x + p13.y * p43.y + p13.z * p43.z;
		var d4321 = p43.x * p21.x + p43.y * p21.y + p43.z * p21.z;
		var d1321 = p13.x * p21.x + p13.y * p21.y + p13.z * p21.z;
		var d4343 = p43.x * p43.x + p43.y * p43.y + p43.z * p43.z;
		var d2121 = p21.x * p21.x + p21.y * p21.y + p21.z * p21.z;

		var denom = d2121 * d4343 - d4321 * d4321;
		if (Mathf.Abs(denom) < float.Epsilon)
		{
			return false;
		}
		var numer = d1343 * d4321 - d1321 * d4343;

		var mua = numer / denom;
		var mub = (d1343 + d4321 * (mua)) / d4343;

		resultSegmentPoint1.x = p1.x + mua * p21.x;
		resultSegmentPoint1.y = p1.y + mua * p21.y;
		resultSegmentPoint1.z = p1.z + mua * p21.z;
		resultSegmentPoint2.x = p3.x + mub * p43.x;
		resultSegmentPoint2.y = p3.y + mub * p43.y;
		resultSegmentPoint2.z = p3.z + mub * p43.z;

		return true;
	}

	/// <summary>
	/// Direct speedup of <seealso cref="Vector3.Lerp"/>
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static Vector3 Lerp(Vector3 v1, Vector3 v2, float value)
	{
		if (value > 1.0f)
			return v2;
		if (value < 0.0f)
			return v1;
		return new Vector3(v1.x + (v2.x - v1.x) * value,
						   v1.y + (v2.y - v1.y) * value,
						   v1.z + (v2.z - v1.z) * value);
	}

	public static Vector3 Sinerp(Vector3 from, Vector3 to, float value)
	{
		value = Mathf.Sin(value * Mathf.PI * 0.5f);
		return Vector3.Lerp(from, to, value);
	}

	public static void Deconstruct(this Vector3 v3, out float x, out float y, out float z)
	{
		x = v3.x;
		y = v3.y;
		z = v3.z;
	}

	public static Vector2 GetAnglesTo(this Vector3 referenceVector, Vector3 compareVector)
			=> new Vector2(-Mathf.Asin(Vector3.Cross(compareVector, referenceVector).y) * Mathf.Rad2Deg,
						   -Mathf.Asin(Vector3.Cross(compareVector, referenceVector).x) * Mathf.Rad2Deg);

	public static Vector3 RandomRange(Vector3 min, Vector3 max) => new Vector3(Random.Range(min.x, max.x),
																			   Random.Range(min.y, max.y),
																			   Random.Range(min.z, max.z));

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
}