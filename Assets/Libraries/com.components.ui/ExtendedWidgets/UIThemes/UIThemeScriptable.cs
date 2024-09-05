using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UGUITheme
{
	[CreateAssetMenu(menuName = "UI/ThemeData")]
	public class UIThemeScriptable : ScriptableObject
	{
		[SerializeField]
		public UIThemeTagsScriptable themeTagsScriptable;

		[SerializeField]
		[ListDrawerSettings(CustomAddFunction = nameof(AddSpriteItem))]
		public List<ThemeTaggedSpriteAsset> spriteAssets = new();

		[SerializeField]
		[ListDrawerSettings(CustomAddFunction = nameof(AddFontItem))]
		public List<ThemeTaggedFontAsset> fontAssets = new();

		[SerializeField]
		[ListDrawerSettings(CustomAddFunction = nameof(AddTmpFontItem))]
		public List<ThemeTaggedTmpFontAsset> tmpFontAssets = new();

		private int AddSpriteItem()
		{
			spriteAssets.Add(new ThemeTaggedSpriteAsset()
			{
				themeTagsScriptable = themeTagsScriptable
			});
			return spriteAssets.Count;
		}

		private int AddFontItem()
		{
			fontAssets.Add(new ThemeTaggedFontAsset()
			{
				themeTagsScriptable = themeTagsScriptable
			});
			return fontAssets.Count;
		}

		private int AddTmpFontItem()
		{
			tmpFontAssets.Add(new ThemeTaggedTmpFontAsset()
			{
				themeTagsScriptable = themeTagsScriptable
			});
			return tmpFontAssets.Count;
		}
	}

	[Serializable]
	public class ThemeTaggedSpriteAsset
	{
		[HorizontalGroup("1", 125)]
		[HideLabel]
		[ValueDropdown(nameof(GetTagNames))]
		public string tagName;

		[HorizontalGroup("1")]
		[HideLabel]
		[AssetSelector]
		public Sprite spriteAsset;

		[NonSerialized]
		public UIThemeTagsScriptable themeTagsScriptable;

		private IEnumerable<string> GetTagNames()
		{
			return themeTagsScriptable == null ? new List<string>() : themeTagsScriptable.themeTags;
		}
	}

	[Serializable]
	public class ThemeTaggedFontAsset
	{
		[HorizontalGroup("2", 125)]
		[HideLabel]
		[ValueDropdown(nameof(GetTagNames))]
		public string tagName;

		[HorizontalGroup("2")]
		[HideLabel]
		[AssetSelector]
		public Font fontAsset;

		[NonSerialized]
		public UIThemeTagsScriptable themeTagsScriptable;

		private IEnumerable<string> GetTagNames()
		{
			return themeTagsScriptable == null ? new List<string>() : themeTagsScriptable.themeTags;
		}
	}

	[Serializable]
	public class ThemeTaggedTmpFontAsset
	{
		[HorizontalGroup("3", 125)]
		[HideLabel]
		[ValueDropdown(nameof(GetTagNames))]
		public string tagName;

		[HorizontalGroup("3")]
		[HideLabel]
		[AssetSelector]
		public TMP_FontAsset tmpFontAsset;

		[NonSerialized]
		public UIThemeTagsScriptable themeTagsScriptable;

		private IEnumerable<string> GetTagNames()
		{
			return themeTagsScriptable == null ? new List<string>() : themeTagsScriptable.themeTags;
		}
	}
}