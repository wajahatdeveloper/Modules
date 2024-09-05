using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UGUITheme
{
	public class UITheme : MonoBehaviour
	{
		[AssetSelector]
		[SerializeField]
		private UIThemeScriptable uiThemeScriptable;

		[SerializeField]
		[OnInspectorInit(nameof(GetUIThemedItems))]
		[ListDrawerSettings(IsReadOnly = true)]
		[InlineEditor]
		private List<UIThemedItem> uiThemedItems = new();

		public List<(string, Sprite)> GetSprites() => uiThemeScriptable.spriteAssets.Select(x=>(x.tagName, x.spriteAsset)).ToList();
		public List<(string, Font)> GetFonts() => uiThemeScriptable.fontAssets.Select(x=>(x.tagName, x.fontAsset)).ToList();
		public List<(string, TMP_FontAsset)> GetTmpFonts() => uiThemeScriptable.tmpFontAssets.Select(x=>(x.tagName, x.tmpFontAsset)).ToList();

		public Sprite GetSprite(string imageTag, out Color colorTint, out Image.Type imageType, out float pixelPerUnit)
		{
			colorTint = Color.white;
			imageType = Image.Type.Simple;
			pixelPerUnit = 0;

			var item = uiThemeScriptable.spriteAssets.FirstOrDefault(x => x.tagName == imageTag);
			if (item == null)
			{
				return null;
			}

			colorTint = item.colorTint;
			imageType = item.imageType;
			pixelPerUnit = item.pixelPerUnit;

			return item?.spriteAsset;
		}

		public Font GetFont(string fontTag)
		{
			return uiThemeScriptable.fontAssets.FirstOrDefault(x => x.tagName == fontTag)?.fontAsset;
		}

		public TMP_FontAsset GetTmpFont(string tmpFontTag)
		{
			return uiThemeScriptable.tmpFontAssets.FirstOrDefault(x => x.tagName == tmpFontTag)?.tmpFontAsset;
		}

		public void GetUIThemedItems(GameObject root)
		{
			List<UIThemedItem> uiThemedItems = new List<UIThemedItem>();

			// Get all UIThemedItem components in the GameObject and its children
			UIThemedItem[] componentsInChildren = root.GetComponentsInChildren<UIThemedItem>(true);

			foreach (UIThemedItem item in componentsInChildren)
			{
				if (!uiThemedItems.Contains(item))
				{
					uiThemedItems.Add(item);
				}
			}

			this.uiThemedItems = uiThemedItems;
		}
	}
}