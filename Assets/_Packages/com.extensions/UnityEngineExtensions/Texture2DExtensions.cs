using UnityEngine;

public static class Texture2DExtensions
{
	/// <summary>
	/// Create new sprite out of Texture
	/// </summary>
	public static Sprite AsSprite(this Texture2D texture)
	{
		var rect = new Rect(0, 0, texture.width, texture.height);
		var pivot = new Vector2(0.5f, 0.5f);
		return Sprite.Create(texture, rect, pivot);
	}

	/// <summary>
	/// Change texture size (and scale accordingly)
	/// </summary>
	public static Texture2D Resample(this Texture2D source, int targetWidth, int targetHeight)
	{
		int sourceWidth = source.width;
		int sourceHeight = source.height;
		float sourceAspect = (float)sourceWidth / sourceHeight;
		float targetAspect = (float)targetWidth / targetHeight;

		int xOffset = 0;
		int yOffset = 0;
		float factor;

		if (sourceAspect > targetAspect)
		{
			// crop width
			factor = (float)targetHeight / sourceHeight;
			xOffset = (int)((sourceWidth - sourceHeight * targetAspect) * 0.5f);
		}
		else
		{
			// crop height
			factor = (float)targetWidth / sourceWidth;
			yOffset = (int)((sourceHeight - sourceWidth / targetAspect) * 0.5f);
		}

		var data = source.GetPixels32();
		var data2 = new Color32[targetWidth * targetHeight];
		for (int y = 0; y < targetHeight; y++)
		{
			for (int x = 0; x < targetWidth; x++)
			{
				var p = new Vector2(Mathf.Clamp(xOffset + x / factor, 0, sourceWidth - 1), Mathf.Clamp(yOffset + y / factor, 0, sourceHeight - 1));
				// bilinear filtering
				var c11 = data[Mathf.FloorToInt(p.x) + sourceWidth * (Mathf.FloorToInt(p.y))];
				var c12 = data[Mathf.FloorToInt(p.x) + sourceWidth * (Mathf.CeilToInt(p.y))];
				var c21 = data[Mathf.CeilToInt(p.x) + sourceWidth * (Mathf.FloorToInt(p.y))];
				var c22 = data[Mathf.CeilToInt(p.x) + sourceWidth * (Mathf.CeilToInt(p.y))];

				data2[x + y * targetWidth] = Color.Lerp(Color.Lerp(c11, c12, p.y), Color.Lerp(c21, c22, p.y), p.x);
			}
		}

		var tex = new Texture2D(targetWidth, targetHeight);
		tex.SetPixels32(data2);
		tex.Apply(true);
		return tex;
	}

	/// <summary>
	/// Will texture with solid color
	/// </summary>
	public static Texture2D WithSolidColor(this Texture2D original, Color color)
	{
		var target = new Texture2D(original.width, original.height);
		for (int i = 0; i < target.width; i++)
		{
			for (int j = 0; j < target.height; j++)
			{
				target.SetPixel(i, j, color);
			}
		}

		target.Apply();

		return target;
	}

	/// <summary>
	/// sets a 1 pixel border of the texture on all mipmap levels to the clear color
	/// </summary>
	/// <param name="texture"></param>
	/// <param name="clearColor"> </param>
	/// <param name="makeNoLongerReadable"> </param>
	public static void ClearMipMapBorders(this Texture2D texture, Color clearColor, bool makeNoLongerReadable = false)
	{
		var mipCount = texture.mipmapCount;

		// In general case, mip level size is mipWidth=max(1,width>>miplevel) and similarly for height.

		var width = texture.width;
		var height = texture.height;
		// tint each mip level
		for (var mip = 1; mip < mipCount; ++mip)
		{
			var mipWidth = Mathf.Max(1, width >> mip);
			var mipHeight = Mathf.Max(1, height >> mip);
			if (mipWidth <= 2) continue; //don't change mip levels below 2x2
			var xCols = new Color[mipWidth];
			var yCols = new Color[mipHeight];
			if (clearColor != default(Color)) //speedup.
			{
				for (var x = 0; x < xCols.Length; ++x)
				{
					xCols[x] = clearColor;
				}
				for (var y = 0; y < yCols.Length; ++y)
				{
					yCols[y] = clearColor;
				}
			}
			texture.SetPixels(0, 0, mipWidth, 1, xCols, mip); //set the top edge colors
			texture.SetPixels(0, 0, 1, mipHeight, yCols, mip); //set the left edge colors
			texture.SetPixels(mipWidth - 1, 0, 1, mipWidth, xCols, mip); //set the bottom edge colors
			texture.SetPixels(0, mipWidth - 1, mipHeight, 1, yCols, mip); //set the right edge colors
		}

		// actually apply all SetPixels, don't recalculate mip levels
		texture.Apply(false, makeNoLongerReadable);
	}

	/// <summary>
	/// sets a 1 pixel border of the texture on all mipmap levels to clear white
	/// </summary>
	/// <param name="texture"></param>
	/// <param name="makeNoLongerReadable"></param>
	public static void ClearMipMapBorders(this Texture2D texture, bool makeNoLongerReadable = false)
	{
		var clear = new Color(1, 1, 1, 0);
		ClearMipMapBorders(texture, clear, makeNoLongerReadable);
	}
}