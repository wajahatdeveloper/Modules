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
		[SerializeField] private UIThemeScriptable uiThemeScriptable;

		public List<(string, Sprite)> GetSprites() => uiThemeScriptable.spriteAssets.Select(x=>(x.tagName, x.spriteAsset)).ToList();
		public List<(string, Font)> GetFonts() => uiThemeScriptable.fontAssets.Select(x=>(x.tagName, x.fontAsset)).ToList();
		public List<(string, TMP_FontAsset)> GetTmpFonts() => uiThemeScriptable.tmpFontAssets.Select(x=>(x.tagName, x.tmpFontAsset)).ToList();

		public Sprite GetSprite(string imageTag)
		{
			return uiThemeScriptable.spriteAssets.FirstOrDefault(x => x.tagName == imageTag)?.spriteAsset;
		}

		public Font GetFont(string fontTag)
		{
			return uiThemeScriptable.fontAssets.FirstOrDefault(x => x.tagName == fontTag)?.fontAsset;
		}

		public TMP_FontAsset GetTmpFont(string tmpFontTag)
		{
			return uiThemeScriptable.tmpFontAssets.FirstOrDefault(x => x.tagName == tmpFontTag)?.tmpFontAsset;
		}
	}
}