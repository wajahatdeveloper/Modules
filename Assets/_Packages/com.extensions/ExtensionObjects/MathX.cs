using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Most of these are speedups of builtin Lerps, as well as a bunch of extra lerp options
/// </summary>
public static class MathX
{
	#region Detectors
		#region 2D Detector
		/// <summary>
		/// Compact implementation of a "Line of Sight"/"Detection" algorithm in 2D.
		/// </summary>
		/// <param name="transform">Transform of the detector entity</param>
		/// <param name="detectionLayerMask">Layer mask of detectable targets</param>
		/// <param name="detectionRange">Detection radius from detector's pivot/centre</param>
		/// <param name="detectionFOV">Detection Fielf of View in degrees</param>
		/// <param name="detectionSightlineLayerMask">"Opaque" objects layer mask. Detector will ignore those excluded layers when determining visibility to target.
		/// Target must be included</param>
		/// <param name="target">First acquired target. Null if none found.</param>
		/// <param name="usesRight">When determining field of view, assign to 'true' if 'transform.right' is considered the front. Otherwise it will use 'transform.forward'.</param>
		/// <returns></returns>
		public static bool Detection2D(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out Transform target, bool usesRight = false) {

			Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectionRange, detectionLayerMask);
			if (cols != null && cols.Length > 0)
			{
				foreach (var item in cols)
				{
					Vector2 relativePos = item.transform.position - transform.position;
					if (Vector2.Angle((usesRight ? transform.right : transform.forward), relativePos) < detectionFOV * 0.5f)
					{
						RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePos, detectionRange,
							detectionSightlineLayerMask);
						if (detectionLayerMask.ContainsLayer(hit.transform.gameObject.layer))
						{
							target = hit.transform;
							return true;
						}
					}
				}
			}
			target = null;
			return false;
		}
		#endregion
		#region 3D Detector
		/// <summary>
		/// Compact implementation of a "Line of Sight"/"Detection" algorithm in 3D.
		/// </summary>
		/// <param name="transform">Transform of the detector entity</param>
		/// <param name="detectionLayerMask">Layer mask of detectable targets</param>
		/// <param name="detectionRange">Detection radius from detector's pivot/centre</param>
		/// <param name="detectionFOV">Detection Fielf of View in degrees</param>
		/// <param name="detectionSightlineLayerMask">"Opaque" objects layer mask. Detector will ignore those excluded layers when determining visibility to target.
		/// Target must be included</param>
		/// <param name="target">First acquired target. Null if none found.</param>
		/// <param name="usesRight">When determining field of view, assign to 'true' if 'transform.right' is considered the front. Otherwise it will use 'transform.forward'.</param>
		/// <returns></returns>
		public static bool Detection3D(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out Transform target, bool usesRight = false) =>
			Detection3D(transform, detectionLayerMask, detectionRange, detectionFOV,
				detectionSightlineLayerMask, out target, Vector3.zero, usesRight);

		/// <summary>
		/// Compact implementation of a "Line of Sight"/"Detection" algorithm in 3D.
		/// </summary>
		/// <param name="transform">Transform of the detector entity</param>
		/// <param name="detectionLayerMask">Layer mask of detectable targets</param>
		/// <param name="detectionRange">Detection radius from detector's pivot/centre</param>
		/// <param name="detectionFOV">Detection Fielf of View in degrees</param>
		/// <param name="detectionSightlineLayerMask">"Opaque" objects layer mask. Detector will ignore those excluded layers when determining visibility to target.
		/// Target must be included</param>
		/// <param name="target">First acquired target. Null if none found.</param>
		/// <param name="deltaPos">View offset for when the "eyes" are not located at the object pivot</param>
		/// <param name="usesRight">When determining field of view, assign to 'true' if 'transform.right' is considered the front. Otherwise it will use 'transform.forward'.</param>
		/// <returns></returns>
		public static bool Detection3D(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out Transform target, Vector3 deltaPos, bool usesRight = false) =>
			 Detection3D(transform, detectionLayerMask, detectionRange, detectionFOV,
				 detectionSightlineLayerMask, out target, deltaPos, usesRight ? Vector3.right : Vector3.forward);


		/// <summary>
		/// Compact implementation of a "Line of Sight"/"Detection" algorithm in 3D.
		/// </summary>
		/// <param name="transform">Transform of the detector entity</param>
		/// <param name="detectionLayerMask">Layer mask of detectable targets</param>
		/// <param name="detectionRange">Detection radius from detector's pivot/centre</param>
		/// <param name="detectionFOV">Detection Fielf of View in degrees</param>
		/// <param name="detectionSightlineLayerMask">"Opaque" objects layer mask. Detector will ignore those excluded layers when determining visibility to target.
		/// Target must be included</param>
		/// <param name="target">First acquired target. Null if none found.</param>
		/// <param name="deltaPos">View offset for when the "eyes" are not located at the object pivot</param>
		/// <param name="frontDirection">Determines the direction considered as "front", relative to the transform.
		/// It will be rotated accordingly to the transform's rotation.</param>
		/// <returns></returns>
		public static bool Detection3D(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out Transform target, Vector3 deltaPos, Vector3 frontDirection) {
			Vector3 refPos = transform.position + (transform.rotation * deltaPos);
			Collider[] cols = Physics.OverlapSphere(refPos, detectionRange, detectionLayerMask);
			if(cols != null && cols.Length > 0)
			{
				foreach(var item in cols)
				{
					Vector3 relativePos = item.transform.position - refPos;
					if(Vector3.Angle((transform.rotation * frontDirection), relativePos) < detectionFOV * 0.5f)
					{
						RaycastHit hit;
						bool didHit = Physics.Raycast(refPos, relativePos, out hit, detectionRange,
							detectionSightlineLayerMask);
						if(didHit && detectionLayerMask.ContainsLayer(hit.transform.gameObject.layer))
						{
							target = hit.transform;
							return true;
						}
					}
				}
			}
			target = null;
			return false;
		}
		#endregion
		#region 3D Detector All
		public static bool Detection3DAll(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out List<Transform> targets, Vector3 deltaPos, bool usesRight = false) {

			Vector3 refPos = transform.position + (transform.rotation * deltaPos);
			Collider[] cols = Physics.OverlapSphere(refPos, detectionRange, detectionLayerMask);
			bool found = false;
			targets = new List<Transform>();

			if (cols != null && cols.Length > 0)
			{
				foreach (var item in cols)
				{
					Vector3 relativePos = item.transform.position - transform.position;
					if (Vector3.Angle((usesRight ? transform.right : transform.forward), relativePos) < detectionFOV * 0.5f)
					{
						RaycastHit hit;

						bool didHit = Physics.Raycast(refPos, relativePos, out hit, detectionRange,
							detectionSightlineLayerMask);
						if (didHit && detectionLayerMask.ContainsLayer(hit.transform.gameObject.layer))
						{
							targets.Add(hit.transform);
							found = true;
						}
					}
				}
			}
			return found;
		}

		public static bool Detection3DAll(Transform transform, LayerMask detectionLayerMask, float detectionRange,
			float detectionFOV, LayerMask detectionSightlineLayerMask, out List<Transform> targets, bool usesRight = false) {

			return Detection3DAll(transform, detectionLayerMask, detectionRange, detectionFOV, detectionSightlineLayerMask, out targets, Vector3.zero, usesRight);
		}
		#endregion
		#endregion


		public static Quaternion Slerp2D(Vector2 a, Vector2 b, float t, bool usesRight = false) {

			float fa;
			float fb;

			if (!usesRight)
			{
				fa = Vector2.SignedAngle(Vector2.up, a);
				fb = Vector2.SignedAngle(Vector2.up, b);
			} else
			{
				fa = Vector2.SignedAngle(Vector2.right, a);
				fb = Vector2.SignedAngle(Vector2.right, b);
			}

			Quaternion qa = Quaternion.AngleAxis(fa, Vector3.forward);
			Quaternion qb = Quaternion.AngleAxis(fb, Vector3.forward);

			return Quaternion.Slerp(qa, qb, t);
		}

		public static Quaternion SlerpWithAxises(Vector3 a, Vector3 b, float t, Vector3 reference, Vector3 axis) {

			float fa = Vector3.SignedAngle(reference, a, axis);
			float fb = Vector3.SignedAngle(reference, b, axis);

			Quaternion qa = Quaternion.AngleAxis(fa, axis);
			Quaternion qb = Quaternion.AngleAxis(fb, axis);

			return Quaternion.Slerp(qa, qb, t);
		}

		public static void Swap(ref float v1, ref float v2) {
			float temp = v1;
			v1 = v2;
			v2 = temp;
		}

		/// <summary>
		/// Returns true if the angle between vec1 and vec2 is smaller than the given angle.
		/// </summary>
		/// <param name="vec1">First vector</param>
		/// <param name="vec2">Second vector</param>
		/// <param name="comparisonAngle">Angle to compare against in degrees.</param>
		/// <returns></returns>
		public static bool AngleBetweenIsSmaller(Vector3 vec1, Vector3 vec2, float comparisonAngle)
			=> Vector3.Angle(vec1, vec2) < comparisonAngle;

		/// <summary>
		/// Returns Cosine of angle. Listen, it may be easy, but sometimes you need a reminder
		/// </summary>
		public static float DotValueForNormalizedAngles(float degrees) => Mathf.Cos(degrees);

	/// <summary>
	/// Swap two reference values
	/// </summary>
	public static void Swap<T>(ref T a, ref T b)
	{
		T x = a;
		a = b;
		b = x;
	}

	/// <summary>
	/// Snap to grid of "round" size
	/// </summary>
	public static float Snap(this float val, float round)
	{
		return round * Mathf.Round(val / round);
	}

	/// <summary>
	/// Returns the sign 1/-1 evaluated at the given value.
	/// </summary>
	public static int Sign(IComparable x) => x.CompareTo(0);

	/// <summary>
	/// Value is in [0, 1) range.
	/// </summary>
	public static bool InRange01(this float value)
	{
		return InRange(value, 0, 1);
	}

	/// <summary>
	/// Value is in [closedLeft, openRight) range.
	/// </summary>
	public static bool InRange<T>(this T value, T closedLeft, T openRight)
		where T : IComparable =>
		value.CompareTo(closedLeft) >= 0 && value.CompareTo(openRight) < 0;

	/// <summary>
	/// Value is in [closedLeft, closedRight] range, max-inclusive.
	/// </summary>
	public static bool InRangeInclusive<T>(this T value, T closedLeft, T closedRight)
		where T : IComparable =>
		value.CompareTo(closedLeft) >= 0 && value.CompareTo(closedRight) <= 0;

	/// <summary>
	/// Clamp value to less than min or more than max
	/// </summary>
	public static float NotInRange(this float num, float min, float max)
	{
		if (min > max)
		{
			var x = min;
			min = max;
			max = x;
		}

		if (num < min || num > max) return num;

		float mid = (max - min) / 2;

		if (num > min) return num + mid < max ? min : max;
		return num - mid > min ? max : min;
	}

	/// <summary>
	/// Clamp value to less than min or more than max
	/// </summary>
	public static int NotInRange(this int num, int min, int max)
	{
		return (int)((float)num).NotInRange(min, max);
	}

	/// <summary>
	/// Return point A or B, closest to num
	/// </summary>
	public static float ClosestPoint(this float num, float pointA, float pointB)
	{
		if (pointA > pointB)
		{
			var x = pointA;
			pointA = pointB;
			pointB = x;
		}

		float middle = (pointB - pointA) / 2;
		float withOffset = num.NotInRange(pointA, pointB) + middle;
		return (withOffset >= pointB) ? pointB : pointA;
	}

	/// <summary>
	/// Check if pointA closer to num than pointB
	/// </summary>
	public static bool ClosestPointIsA(this float num, float pointA, float pointB)
	{
		var closestPoint = num.ClosestPoint(pointA, pointB);
		return Mathf.Approximately(closestPoint, pointA);
	}

	public static float Lerp(float from, float to, float value)
	{
		if (value < 0.0f)
			return from;
		if (value > 1.0f)
			return to;
		return (to - from) * value + from;
	}

	public static float LerpUnclamped(float from, float to, float value)
	{
		return (1.0f - value) * from + value * to;
	}

	public static float InverseLerp(float from, float to, float value)
	{
		if (from < to)
		{
			if (value < from)
				return 0.0f;
			if (value > to)
				return 1.0f;
		}
		else
		{
			if (value < to)
				return 1.0f;
			if (value > @from)
				return 0.0f;
		}
		return (value - from) / (to - from);
	}

	public static float InverseLerpUnclamped(float from, float to, float value)
	{
		return (value - from) / (to - from);
	}

	public static float SmoothStep(float from, float to, float value)
	{
		if (value < 0.0f)
			return from;
		if (value > 1.0f)
			return to;
		value = value * value * (3.0f - 2.0f * value);
		return (1.0f - value) * from + value * to;
	}

	public static float SmoothStepUnclamped(float from, float to, float value)
	{
		value = value * value * (3.0f - 2.0f * value);
		return (1.0f - value) * from + value * to;
	}

	public static float SuperLerp(float from, float to, float from2, float to2, float value)
	{
		if (from2 < to2)
		{
			if (value < from2)
				value = from2;
			else if (value > to2)
				value = to2;
		}
		else
		{
			if (value < to2)
				value = to2;
			else if (value > from2)
				value = from2;
		}
		return (to - from) * ((value - from2) / (to2 - from2)) + from;
	}

	public static float SuperLerpUnclamped(float from, float to, float from2, float to2, float value)
	{
		return (to - from) * ((value - from2) / (to2 - from2)) + from;
	}

	public static float Hermite(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
	}

	public static float Sinerp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
	}

	public static float Coserp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
	}

	public static float Berp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
		return start + (end - start) * value;
	}

	public static float Bounce(float x)
	{
		return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
	}

	// test for value that is near specified float (due to floating point inprecision)
	// all thanks to Opless for this!
	public static bool Approx(float val, float about, float range)
	{
		return ((Mathf.Abs(val - about) < range));
	}

	/*
	  * CLerp - Circular Lerp - is like lerp but handles the wraparound from 0 to 360.
	  * This is useful when interpolating eulerAngles and the object
	  * crosses the 0/360 boundary.  The standard Lerp function causes the object
	  * to rotate in the wrong direction and looks stupid. Clerp fixes that.
	  */

	public static float Clerp(float start, float end, float value)
	{
		const float min = 0.0f;
		const float max = 360.0f;
		float half = Mathf.Abs((max - min) / 2.0f);//half the distance between min and max
		float retval;
		float diff;

		if ((end - start) < -half)
		{
			diff = ((max - start) + end) * value;
			retval = start + diff;
		}
		else if ((end - start) > half)
		{
			diff = -((max - end) + start) * value;
			retval = start + diff;
		}
		else retval = start + (end - start) * value;

		// Debug.Log("Start: "  + start + "   End: " + end + "  Value: " + value + "  Half: " + half + "  Diff: " + diff + "  Retval: " + retval);
		return retval;
	}

	public static float GaussFalloff(float distance, float inRadius)
	{
		return Mathf.Clamp01(Mathf.Pow(360f, -Mathf.Pow(distance / inRadius, 2.5f) - 0.01f));
	}

	//public unsafe static float FastInvSqrt(float x)
	//{
	//    float xhalf = 0.5f * x;
	//    int i = *(int*)&x;
	//    i = 0x5f375a86 - (i >> 1); //this constant is slightly more accurate than the common one
	//    x = *(float*)&i;
	//    x = x * (1.5f - xhalf * x * x);
	//    return x;
	//}

	//public static unsafe float FastSqrt(float x)
	//{
	//    float xhalf = 0.5f * x;
	//    int i = *(int*) &x;
	//    i = 0x1fbd1df5 + (i >> 1);  // da magicks
	//    x = *(float*) &i;
	//    x = x * (1.5f - (xhalf * x * x)); //newtons method to improve approximation
	//    return x;
	//}

	public static string ConvertSeconds(int time)
	{
		var _minutes = (int)(time / 60);
		string minutes = "";
		int _seconds = (time % 60);
		string seconds = "";
		if (_minutes > 0) minutes = _minutes + " minute" + (_minutes > 1 ? "s " : " ");
		if (_seconds > 0) seconds = _seconds + " second" + (_seconds > 1 ? "s " : " ");
		return (minutes + seconds).Substring(0, (minutes + seconds).Length - 1);
	}

	/// <summary>
	/// is this float approximately other
	/// </summary>
	/// <param name="a"></param>
	/// <param name="other"></param>
	/// <returns></returns>
	public static bool Approximately(this float a, float other)
	{
		return Mathf.Approximately(a, other);
	}

	/// <summary>
	/// is this float within range of other
	/// </summary>
	/// <param name="x"></param>
	/// <param name="other"></param>
	/// <param name="delta"></param>
	/// <returns></returns>
	public static bool Approximately(this float x, float other, float delta)
	{
		return Math.Abs(x - other) < delta;
	}
}
