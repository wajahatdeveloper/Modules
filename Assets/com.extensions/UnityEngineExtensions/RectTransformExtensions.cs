using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Provides extension methods for rect transform components.
/// </summary>
public static class RectTransformExtensions
{
	public static void SetDefaultScale(this RectTransform trans)
	{
		trans.localScale = new Vector3(1, 1, 1);
	}

	public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
	{
		trans.pivot = aVec;
		trans.anchorMin = aVec;
		trans.anchorMax = aVec;
	}

	public static Vector2 GetSize(this RectTransform trans)
	{
		return trans.rect.size;
	}

	public static float GetWidth(this RectTransform trans)
	{
		return trans.rect.width;
	}

	public static float GetHeight(this RectTransform trans)
	{
		return trans.rect.height;
	}

	public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
	{
		trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
	}

	public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
	{
		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	}

	public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
	{
		trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	}

	public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
	{
		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
	}

	public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
	{
		trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
	}

	public static void SetSize(this RectTransform trans, Vector2 newSize)
	{
		var oldSize = trans.rect.size;
		var deltaSize = newSize - oldSize;
		trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
		trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
	}

	public static void SetWidth(this RectTransform trans, float newSize)
	{
		SetSize(trans, new Vector2(newSize, trans.rect.size.y));
	}

	public static void SetHeight(this RectTransform trans, float newSize)
	{
		SetSize(trans, new Vector2(trans.rect.size.x, newSize));
	}

	/// <summary>
	/// Represents values for a rect anchor setting.
	/// </summary>
	private struct RectSetting
	{
		/// <summary>
		/// The anchor's max values.
		/// </summary>
		public Vector2 anchorMax;

		/// <summary>
		/// The anchor's min values.
		/// </summary>
		public Vector2 anchorMin;

		/// <summary>
		/// The pivot values.
		/// </summary>
		public Vector2 pivot;

		/// <summary>
		/// Initializes the rectangle setting.
		/// </summary>
		/// <param name="xMin">The minimum x value.</param>
		/// <param name="xMax">The maximum x value.</param>
		/// <param name="yMin">The minimum y value.</param>
		/// <param name="yMax">The maximum y value.</param>
		/// <param name="xPivot">The pivot value on the x axis.</param>
		/// <param name="yPivot">The pivot value on the y axis.</param>
		public RectSetting(float xMin, float xMax, float yMin, float yMax, float xPivot, float yPivot)
		{
			anchorMax = new Vector2(xMax, yMax);
			anchorMin = new Vector2(xMin, yMin);
			pivot = new Vector2(xPivot, yPivot);
		}
	}

	/// <summary>
	/// Holds the preset values used for each anchor setting.
	/// </summary>
	private static readonly Dictionary<RectAnchor, RectSetting> _anchorPresets = new Dictionary<RectAnchor, RectSetting>
		{
			{ RectAnchor.TOP_LEFT, new RectSetting( 0f, 0f, 1f,1f, 0f,1f )},
			{ RectAnchor.TOP_CENTER, new RectSetting( 0.5f, 0.5f, 1f,1f,0.5f,1f )},
			{ RectAnchor.TOP_RIGHT, new RectSetting( 1f,1f,1f,1f, 1f,1f )},
			{ RectAnchor.MIDDLE_LEFT, new RectSetting( 0f,0f, 0.5f, 0.5f,0f,0.5f )},
			{ RectAnchor.MIDDLE_CENTER, new RectSetting( 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f )},
			{ RectAnchor.MIDDLE_RIGHT, new RectSetting( 1f,1f,0.5f,0.5f, 1f, 0.5f )},
			{ RectAnchor.BOTTOM_LEFT, new RectSetting( 0f,0f,0f,0f, 0f, 0f )},
			{ RectAnchor.BOTTOM_CENTER, new RectSetting( 0.5f, 0.5f, 0f, 0f, 0.5f, 0f )},
			{ RectAnchor.BOTTOM_RIGHT, new RectSetting( 1f,1f,0f,0f, 1f, 0f )},
			{ RectAnchor.STRETCH_TOP, new RectSetting( 0f, 1f, 1f,1f,0.5f,1f )},
			{ RectAnchor.STRETCH_MIDDLE, new RectSetting( 0f,1f,0.5f,0.5f, 0.5f, 0.5f )},
			{ RectAnchor.STRETCH_BOTTOM, new RectSetting( 0f,1f,0f,0f, 0.5f, 0f )},
			{ RectAnchor.STRETCH_LEFT, new RectSetting( 0f,0f,0f,1f, 0f,0.5f )},
			{ RectAnchor.STRETCH_CENTER, new RectSetting( 0.5f, 0.5f, 0f, 1f, 0.5f, 0.5f )},
			{ RectAnchor.STRETCH_RIGHT, new RectSetting( 1f,1f, 0f, 1f, 1f, 0.5f )},
			{ RectAnchor.STRETCH_FULL, new RectSetting( 0f, 1f, 0f, 1f, 0.5f, 0.5f )},
		};

	/// <summary>
	/// Sets the anchor values.
	/// </summary>
	/// <param name="transform">The rectangle transform.</param>
	/// <param name="xMin">The minimum x value.</param>
	/// <param name="xMax">The maximum x value.</param>
	/// <param name="yMin">The minimum y value.</param>
	/// <param name="yMax">The maximum y value.</param>
	public static void SetAnchor(this RectTransform transform, float xMin, float xMax, float yMin, float yMax)
	{
		if (transform == null)
			throw new ArgumentNullException(nameof(transform));

		transform.anchorMin = new Vector2(xMin, yMin);
		transform.anchorMax = new Vector2(xMax, yMax);
	}

	/// <summary>
	/// Sets the anchor to a given setting.
	/// </summary>
	/// <param name="transform">The rectangle transform.</param>
	/// <param name="anchor">The anchor setting to use.</param>
	/// <param name="setPivot">Whether the pivot should also be set based on the new setting.</param>
	/// <param name="setPosition">Whether to set the position after the setting has been applied.</param>
	public static void SetAnchor(
		this RectTransform transform,
		RectAnchor anchor,
		bool setPivot = false,
		bool setPosition = false)
	{
		if (transform == null)
			throw new ArgumentNullException(nameof(transform));

		RectSetting setting = _anchorPresets[anchor];
		SetAnchor(transform, setting.anchorMin.x, setting.anchorMax.x, setting.anchorMin.y, setting.anchorMax.y);

		if (setPivot)
			transform.pivot = setting.pivot;

		if (setPosition)
			transform.anchoredPosition = Vector2.zero;
	}

	/// <summary>
	/// Returns the world rectangle of a rectangle transform.
	/// </summary>
	/// <param name="transform">The rectangle transform.</param>
	/// <returns>The world rectangle.</returns>
	public static Rect GetWorldRect(this RectTransform transform)
	{
		if (transform == null)
			throw new ArgumentNullException(nameof(transform));

		Vector3[] corners = new Vector3[4];
		transform.GetWorldCorners(corners);

		Vector3 bottomLeft = corners[0];

		Vector2 size = new Vector2(
			transform.lossyScale.x * transform.rect.size.x,
			transform.lossyScale.y * transform.rect.size.y);

		return new Rect(bottomLeft, size);
	}

	/// <summary>
	/// Returns the rectangle transform. Will return null if a normal transform is used.
	/// </summary>
	/// <param name="component">The component of which to get the rectangle transform.</param>
	/// <returns>The rectangle transform instance.</returns>
	public static RectTransform GetRectTransform(this Component component)
	{
		if (component == null)
			throw new ArgumentNullException(nameof(component));

		return component.transform as RectTransform;
	}

	/// <summary>
	/// Returns the rectangle transform. Will return null if a normal transform is used.
	/// </summary>
	/// <param name="gameObject">The game object of which to get the rectangle transform.</param>
	/// <returns>The rectangle transform instance.</returns>
	public static RectTransform GetRectTransform(this GameObject gameObject)
	{
		if (gameObject == null)
			throw new ArgumentNullException(nameof(gameObject));

		return gameObject.transform as RectTransform;
	}

	/// <summary>
	///   <para>Returns true if the <paramref name="other"/> RectTransform overlaps this one.</para>
	/// </summary>
	/// <param name="other">Other rectangle to test overlapping with.</param>
	public static bool Overlaps(this RectTransform rect, RectTransform other) => rect.WorldRect().Overlaps(other.WorldRect());

	/// <summary>
	///   <para>Returns true if the <paramref name="other"/> RectTransform overlaps this one. If <paramref name="allowInverse"/> is true, the widths and heights of the Rects are allowed to take negative values (ie, the min value is greater than the max), and the test will still work.</para>
	/// </summary>
	/// <param name="other">Other rectangle to test overlapping with.</param>
	/// <param name="allowInverse">Does the test allow the widths and heights of the Rects to be negative?</param>
	public static bool Overlaps(this RectTransform rect, RectTransform other, bool allowInverse) => rect.WorldRect().Overlaps(other.WorldRect(), allowInverse);

	public static Rect WorldRect(this RectTransform rectTransform)
	{
		var sizeDelta = rectTransform.sizeDelta;
		var rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
		var rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

		var position = rectTransform.position;
		return new Rect(position.x - rectTransformWidth / 2f,
						position.y - rectTransformHeight / 2f,
						rectTransformWidth,
						rectTransformHeight);
	}

	public static Rect ToScreenSpace(this RectTransform transform) // TODO This might be a duplicate of RectTransformExtensions.WorldRect
	{
		Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
		Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
		rect.x -= (transform.pivot.x * size.x);
		rect.y -= ((1.0f - transform.pivot.y) * size.y);
		return rect;
	}
}

public enum RectAnchor
{
	TOP_LEFT = 0,
	TOP_CENTER = 1,
	TOP_RIGHT = 2,
	MIDDLE_LEFT = 3,
	MIDDLE_CENTER = 4,
	MIDDLE_RIGHT = 5,
	BOTTOM_LEFT = 6,
	BOTTOM_CENTER = 7,
	BOTTOM_RIGHT = 8,
	STRETCH_TOP = 9,
	STRETCH_MIDDLE = 10,
	STRETCH_BOTTOM = 11,
	STRETCH_LEFT = 12,
	STRETCH_CENTER = 13,
	STRETCH_RIGHT = 14,
	STRETCH_FULL = 15
}