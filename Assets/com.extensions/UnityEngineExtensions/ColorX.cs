using System;
using UnityEngine;

public static class ColorX
{
	public static string ColorToHex(this Color color, bool includeAlpha = false)
	{
		Color32 color32 = color;
		var result = color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
		if (includeAlpha) result += color32.a.ToString("X2");
		return result;
	}

	public static Color HexToColor(this string inputHexString)
	{
		if (string.IsNullOrEmpty(inputHexString)) throw new ArgumentNullException(nameof(inputHexString));
		if (inputHexString.Length != 6 && inputHexString.Length != 8)
			throw new ArgumentException("Input string must have exactly 6 or 8 characters (without or with alpha).", nameof(inputHexString));

		var r = byte.Parse(inputHexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		var g = byte.Parse(inputHexString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		var b = byte.Parse(inputHexString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		var a = inputHexString.Length == 8
			? byte.Parse(inputHexString.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)
			: (byte)255;
		return new Color32(r, g, b, a);
	}

	public static Color GetColorFromRGB255(int r, int g, int b) => new Color(r / 255.0f, g / 255.0f, b / 255.0f);

	public static HSV GetHSV(this Color color)
	{
		return HSV.FromColor(color);
	}

	public static HSL ToHsl(this Color color)
	{
		return HSL.FromColor(color);
	}

	public static Color MakeRandomColor(this Color color, float minClamp = 0.5f)
	{
		var randCol = UnityEngine.Random.onUnitSphere * 3;
		randCol.x = Mathf.Clamp(randCol.x, minClamp, 1f);
		randCol.y = Mathf.Clamp(randCol.y, minClamp, 1f);
		randCol.z = Mathf.Clamp(randCol.z, minClamp, 1f);

		return new Color(randCol.x, randCol.y, randCol.z, 1f);
	}

	/// <summary>
	/// Direct speedup of <seealso cref="Color.Lerp"/>
	/// </summary>
	/// <param name="c1"></param>
	/// <param name="c2"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static Color Lerp(Color c1, Color c2, float value)
	{
		if (value > 1.0f)
			return c2;
		if (value < 0.0f)
			return c1;
		return new Color(c1.r + (c2.r - c1.r) * value,
						 c1.g + (c2.g - c1.g) * value,
						 c1.b + (c2.b - c1.b) * value,
						 c1.a + (c2.a - c1.a) * value);
	}
}