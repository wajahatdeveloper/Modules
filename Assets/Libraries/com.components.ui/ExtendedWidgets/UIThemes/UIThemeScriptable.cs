using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(menuName = "UI/ThemeData")]
public class UIThemeScriptable : ScriptableObject
{
	public List<ThemeItem> themeItems = new();
}

[Serializable]
public class ThemeItem
{
	private string itemTypeName;

	[ShowInInspector]
	[PropertyOrder(1)]
	public Type itemType
	{
		set
		{
			itemTypeName = value.AssemblyQualifiedName;
		}

		get
		{
			Type type = null;

			try
			{
				type = Type.GetType(itemTypeName);
			}
			catch (NullReferenceException)
			{
			}

			return type;
		}
	}

	[PropertyOrder(2)]
	public List<Sprite> spriteAssets = new();

	[PropertyOrder(3)]
	public List<Font> fonts = new();

	[PropertyOrder(4)]
	public List<TMP_FontAsset> tmpFontAssets = new();
}