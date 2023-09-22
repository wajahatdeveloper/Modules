using InlineCoroutine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public static class AssortedExtensions
{
	public static float GetPercent(this float totalValue, float percent)
	{
		return (totalValue * (percent / 100f));
	}

	public static float GetPercent(this int totalValue, float percent)
	{
		return ((float)totalValue * (percent / 100f));
	}

	// http://wiki.unity3d.com/index.php?title=IsVisibleFrom
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}

	public static void SetAlpha(this Graphic graphic, float value)
	{
		graphic.color = graphic.color.WithAlpha(value);
	}

	public static void SetAlpha(this SpriteRenderer spriteRenderer, float value)
	{
		spriteRenderer.color = spriteRenderer.color.WithAlpha(value);
	}

	   public static Color WithR(this Color color, float value)
        {
            color.r = value;

            return color;
        }

        public static Color WithG(this Color color, float value)
        {
            color.g = value;

            return color;
        }

        public static Color WithB(this Color color, float value)
        {
            color.b = value;

            return color;
        }

        public static Color WithAlpha(this Color color, float value)
        {
            color.a = value;

            return color;
        }

        public static float Hue(this Color color)
        {
            float min = Mathf.Min(color.r, color.g, color.b);
            float max = Mathf.Max(color.r, color.g, color.b);

            if (max == 0 || max == min)
                return 0;
            else
            {
                float hue;
                float delta = max - min;

                if (color.r == max)
                    hue = 0 + (color.g - color.b) / delta;
                else if (color.g == max)
                    hue = 2 + (color.b - color.r) / delta;
                else
                    hue = 4 + (color.r - color.g) / delta;

                hue *= 60;

                if (hue < 0)
                    hue += 360;

                return hue;
            }
        }

        public static float Brightness(this Color color)
        {
            return Mathf.Max(color.r, color.g, color.b) * 100 / 255;
        }

        public static float Saturation(this Color color)
        {
            if (color == Color.black)
                return 0;

            float max = Mathf.Max(color.r, color.g, color.b);
            float delta = max - Mathf.Min(color.r, color.g, color.b);

            return 255 * delta / max;
        }

        public static Color Closest(this Color color, Color[] colors)
        {
            return colors.MinBy(x => GetDiff(x, color));
        }

        private static float GetDiff(Color color, Color otherColor)
        {
            float a = color.a - otherColor.a,
                r = color.r - otherColor.r,
                g = color.g - otherColor.g,
                b = color.b - otherColor.b;

            return a * a + r * r + g * g + b * b;
        }

	public static bool IsWorldPointInViewport(this Camera camera, Vector3 point)
	{
		var position = camera.WorldToViewportPoint(point);
		return position.x > 0 && position.y > 0;
	}

	/// <summary>
	/// Gets a point with the same screen point as the source point,
	/// but at the specified distance from camera.
	/// </summary>
	public static Vector3 WorldPointOffsetByDepth(this Camera camera,
		Vector3 source,
		float distanceFromCamera,
		Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono)
	{
		var screenPoint = camera.WorldToScreenPoint(source, eye);
		return camera.ScreenToWorldPoint(screenPoint.SetZ(distanceFromCamera),
			eye);
	}


	/// <summary>
	/// Sets the lossy scale of the source Transform.
	/// </summary>
	public static Transform SetLossyScale(this Transform source,
		Vector3 targetLossyScale)
	{
		source.localScale = source.lossyScale.Pow(-1).ScaleBy(targetLossyScale)
			.ScaleBy(source.localScale);
		return source;
	}

	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		var toGet = gameObject.GetComponent<T>();
		if (toGet != null) return toGet;
		return gameObject.AddComponent<T>();
	}

	public static T GetOrAddComponent<T>(this Component component) where T : Component
	{
		var toGet = component.gameObject.GetComponent<T>();
		if (toGet != null) return toGet;
		return component.gameObject.AddComponent<T>();
	}
	
	/// <summary>
	/// Get all components of specified Layer in childs
	/// </summary>
	public static List<Transform> GetObjectsOfLayerInChilds(this GameObject gameObject, int layer)
	{
		List<Transform> list = new List<Transform>();
		CheckChildsOfLayer(gameObject.transform, layer, list);
		return list;
	}

	/// <summary>
	/// Get all components of specified Layer in childs
	/// </summary>
	public static List<Transform> GetObjectsOfLayerInChilds(this GameObject gameObject, string layer)
	{
		return gameObject.GetObjectsOfLayerInChilds(LayerMask.NameToLayer(layer));
	}

	/// <summary>
	/// Get all components of specified Layer in childs
	/// </summary>
	public static List<Transform> GetObjectsOfLayerInChilds(this Component component, string layer)
	{
		return component.GetObjectsOfLayerInChilds(LayerMask.NameToLayer(layer));
	}

	/// <summary>
	/// Get all components of specified Layer in childs
	/// </summary>
	public static List<Transform> GetObjectsOfLayerInChilds(this Component component, int layer)
	{
		return component.gameObject.GetObjectsOfLayerInChilds(layer);
	}

	private static void CheckChildsOfLayer(Transform transform, int layer, List<Transform> childsCache)
	{
		foreach (Transform t in transform)
		{
			CheckChildsOfLayer(t, layer, childsCache);

			if (t.gameObject.layer != layer) continue;
			childsCache.Add(t);
		}
	}


	/// <summary>
	/// Swap Rigidbody IsKinematic and DetectCollisions
	/// </summary>
	/// <param name="body"></param>
	/// <param name="state"></param>
	public static void SetBodyState(this Rigidbody body, bool state)
	{
		body.isKinematic = !state;
		body.detectCollisions = state;
	}


	/// <summary>
	/// Find all Components of specified interface
	/// </summary>
	public static T[] FindObjectsOfInterface<T>() where T : class
	{
		var monoBehaviours = Object.FindObjectsOfType<Transform>();

		return monoBehaviours.Select(behaviour => behaviour.GetComponent(typeof(T))).OfType<T>().ToArray();
	}

	/// <summary>
	/// Find all Components of specified interface along with Component itself
	/// </summary>
	public static ComponentOfInterface<T>[] FindObjectsOfInterfaceAsComponents<T>() where T : class
	{
		return Object.FindObjectsOfType<Component>()
			.Where(c => c is T)
			.Select(c => new ComponentOfInterface<T>(c, c as T)).ToArray();
	}

	public struct ComponentOfInterface<T>
	{
		public readonly Component Component;
		public readonly T Interface;

		public ComponentOfInterface(Component component, T @interface)
		{
			Component = component;
			Interface = @interface;
		}
	}

	/// <summary>
	/// Disposes of the object, if it isn't null.
	/// </summary>
	/// <param name="disposable"></param>
	public static void DisposeIfNotNull(this IDisposable argument)
	{
		if (argument != null)
		{
			argument.Dispose();
		}
	}
	
	public static bool HasMethod(this object target, string methodName)
	{
		return target.GetType().GetMethod(methodName) != null;
	}

	public static bool HasField(this object target, string fieldName)
	{
		return target.GetType().GetField(fieldName) != null;
	}

	public static bool HasProperty(this object target, string propertyName)
	{
		return target.GetType().GetProperty(propertyName) != null;
	}

	#region One Per Instance

	/// <summary>
	/// Get components with unique Instance ID
	/// </summary>
	public static T[] OnePerInstance<T>(this T[] components) where T : Component
	{
		if (components == null || components.Length == 0) return null;
		return components.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
	}

	/// <summary>
	/// Get hits with unique owner Instance ID
	/// </summary>
	public static RaycastHit2D[] OneHitPerInstance(this RaycastHit2D[] hits)
	{
		if (hits == null || hits.Length == 0) return null;
		return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
	}

	/// <summary>
	/// Get colliders with unique owner Instance ID
	/// </summary>
	public static Collider2D[] OneHitPerInstance(this Collider2D[] hits)
	{
		if (hits == null || hits.Length == 0) return null;
		return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
	}

	/// <summary>
	/// Get colliders with unique owner Instance ID
	/// </summary>
	public static List<Collider2D> OneHitPerInstanceList(this Collider2D[] hits)
	{
		if (hits == null || hits.Length == 0) return null;
		return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToList();
	}

	#endregion

	/// <summary>
		/// Get individual bit from a byte.
		/// </summary>
		/// <param name="input">Byte to extract boolean bit from.</param>
		/// <param name="index">Index from 0 to 7 of the desired bit within the byte.</param>
		/// <returns></returns>
		public static bool GetBit(this byte input, int index) {
			if(index < 0 || index > 7)
				throw new IndexOutOfRangeException();
			return (input & (1 << index)) != 0;
		}

		/// <summary>
		/// Set individual bit to a byte. This does not modifies the referenced byte.
		/// </summary>
		/// <param name="thisByte">Byte to insert bit into.</param>
		/// <param name="index">Index from 0 to 7 of the desired bit within the byte.</param>
		/// <param name="value">Boolean value to set bit as (0 or 1).</param>
		/// <returns></returns>
		public static byte SetBit(this byte thisByte, int index, bool value) {
			if(index < 0 || index > 7)
				throw new IndexOutOfRangeException();
			if(value)
				thisByte = (byte)(thisByte | 1 << index);
			else
				thisByte = (byte)(thisByte & ~(1 << index));
			return thisByte;
		}

		/// <summary>
		/// Same as SetBit, but instead actually modifies the referenced byte.
		/// Set individual bit to a byte.
		/// </summary>
		/// <param name="thisByte">Byte to insert bit into.</param>
		/// <param name="index">Index from 0 to 7 of the desired bit within the byte.</param>
		/// <param name="value">Boolean value to set bit as (0 or 1).</param>
		public static void RefSetBit(this ref byte thisByte, int index, bool value) =>
			thisByte = thisByte.SetBit(index, value);

		/// <summary>
		/// Get individual bit from an int.
		/// </summary>
		/// <param name="input">Int to extract boolean bit from.</param>
		/// <param name="index">Index from 0 to 31 of the desired bit within the int.</param>
		/// <returns></returns>
		public static bool GetBit(this int input, int index) {
			if(index < 0 || index > 31)
				throw new IndexOutOfRangeException();
			return (input & (1 << index)) != 0;
		}

		/// <summary>
		/// Set individual bit to an in. This does not modifies the referenced int.
		/// </summary>
		/// <param name="input">Int to insert bit into.</param>
		/// <param name="value">Boolean value to set bit as (0 or 1).</param>
		/// <param name="index">Index from 0 to 31 of the desired bit within the byte.</param>
		/// <returns></returns>
		public static int SetBit(this int input, int index, bool value) {
			if(index < 0 || index > 31)
				throw new IndexOutOfRangeException();
			if(value)
				input = (byte)(input | 1 << index);
			else
				input = (byte)(input & ~(1 << index));
			return input;
		}

		/// <summary>
		/// Get a slice of an array as a new array.
		/// </summary>
		/// <param name="source">Source array from which the slice will be made.</param>
		/// <param name="start">Index from the original array from which to begin the slice.</param>
		/// <param name="count">Count of elements to copy from the array.</param>
		/// <returns></returns>
		public static T[] Slice<T>(this T[] source, int start, int count) {
			var array = new T[count];
			float limit = count + start;
			int c = 0;
			for(int i = start; i < limit; i++)
			{
				array[c] = source[i];
				c++;
			}
			return array;
		}

		/// <summary>
		/// Given an int value, returns a layer mask for that layer alone.
		/// </summary>
		/// <param name="layer">Layer to create a layer mask from (0-31)</param>
		/// <returns></returns>
		public static LayerMask IntToLayerMask(this int layer) {
			return (1 << layer);
		}

		/// <summary>
		/// Returns value squared.
		/// </summary>
		/// <returns></returns>
		public static float Squared(this float num) => num * num;

		public static bool XNOR(this bool a, bool b) => !(a ^ b);
}