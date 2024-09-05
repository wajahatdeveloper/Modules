using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UGUITheme
{
	public class UIThemedItem : MonoBehaviour
	{
		[ShowIf(nameof(HasImage))] [ValueDropdown(nameof(GetSpriteTags))] [OnValueChanged(nameof(SetImageSprite))]
		public string imageTag;

		[HideInInspector] public Sprite imageSprite;

		[ShowIf(nameof(HasText))] [ValueDropdown(nameof(GetFontTags))] [OnValueChanged(nameof(SetFont))]
		public string fontTag;

		[HideInInspector] public Font font;

		[ShowIf(nameof(HasTextMesh))] [ValueDropdown(nameof(GetTMPFontTags))] [OnValueChanged(nameof(SetTMPFont))]
		public string tmpFontTag;

		[HideInInspector] public TMP_FontAsset tmpFont;

		private UITheme uiTheme;

		private Image image;
		private Text text;
		private TextMeshProUGUI textMesh;

		[OnInspectorInit]
		[ContextMenu(nameof(_internal_inspector_init))]
		private void _internal_inspector_init()
		{
			if (uiTheme == null)
			{
				uiTheme = FindComponentInParentHierarchy<UITheme>();
			}

			image = GetComponent<Image>();
			text = GetComponent<Text>();
			textMesh = GetComponent<TextMeshProUGUI>();
		}

		private bool HasImage()
		{
			return image != null;
		}

		private bool HasText()
		{
			return text != null;
		}

		private bool HasTextMesh()
		{
			return textMesh != null;
		}

		private void SetImageSprite()
		{
			Undo.RecordObject(image, "Theme:ChangeSprite");
			PrefabUtility.RecordPrefabInstancePropertyModifications(image);
			{
				imageSprite = uiTheme.GetSprite(imageTag);
				image.sprite = imageSprite;
			}
			EditorUtility.SetDirty(image);
		}

		private void SetFont()
		{
			Undo.RecordObject(image, "Theme:ChangeFont");
			PrefabUtility.RecordPrefabInstancePropertyModifications(image);
			{
				font = uiTheme.GetFont(fontTag);
				text.font = font;
			}
			EditorUtility.SetDirty(image);
		}

		private void SetTMPFont()
		{
			Undo.RecordObject(image, "Theme:ChangeTmpFont");
			PrefabUtility.RecordPrefabInstancePropertyModifications(image);
			{
				tmpFont = uiTheme.GetTmpFont(tmpFontTag);
				textMesh.font = tmpFont;
			}
			EditorUtility.SetDirty(image);
		}

		private List<string> GetSpriteTags()
		{
			return uiTheme.GetSprites().Select(x => x.Item1).ToList();
		}

		private List<string> GetFontTags()
		{
			return uiTheme.GetFonts().Select(x => x.Item1).ToList();
		}

		private List<string> GetTMPFontTags()
		{
			return uiTheme.GetTmpFonts().Select(x => x.Item1).ToList();
		}

		private List<Sprite> GetSprites()
		{
			return uiTheme.GetSprites().Select(x => x.Item2).ToList();
		}

		private List<Font> GetFonts()
		{
			return uiTheme.GetFonts().Select(x => x.Item2).ToList();
		}

		private List<TMP_FontAsset> GetTMPFonts()
		{
			return uiTheme.GetTmpFonts().Select(x => x.Item2).ToList();
		}

		private T FindComponentInParentHierarchy<T>() where T : Component
		{
			if (gameObject == null)
			{
				return null;
			}

			// Check the current GameObject first
			var component = gameObject.GetComponent<T>();
			if (component != null)
			{
				return component;
			}

			// Traverse up the hierarchy and check each parent
			var parent = gameObject.transform.parent;
			while (parent != null)
			{
				component = parent.GetComponent<T>();
				if (component != null)
				{
					return component;
				}

				parent = parent.parent;
			}

			// No component found in the hierarchy
			return null;
		}
	}
}