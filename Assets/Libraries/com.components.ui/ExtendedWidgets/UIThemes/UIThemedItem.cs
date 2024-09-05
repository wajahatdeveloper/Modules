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
			#if UNITY_EDITOR
			Undo.RecordObject(image, "Theme:ChangeSprite");
			PrefabUtility.RecordPrefabInstancePropertyModifications(image);
			#endif
			{
				imageSprite = uiTheme.GetSprite(imageTag, out Color colorTint, out Image.Type imageType, out float pixelPerUnit);
				image.sprite = imageSprite;
				image.color = colorTint;
				image.type = imageType;
				image.pixelsPerUnitMultiplier = pixelPerUnit;
			}
			#if UNITY_EDITOR
			EditorUtility.SetDirty(image);
			#endif
		}

		private void SetFont()
		{
			#if UNITY_EDITOR
			Undo.RecordObject(text, "Theme:ChangeFont");
			PrefabUtility.RecordPrefabInstancePropertyModifications(text);
			#endif
			{
				font = uiTheme.GetFont(fontTag);
				text.font = font;
			}
			#if UNITY_EDITOR
			EditorUtility.SetDirty(text);
			#endif
		}

		private void SetTMPFont()
		{
			#if UNITY_EDITOR
			Undo.RecordObject(textMesh, "Theme:ChangeTmpFont");
			PrefabUtility.RecordPrefabInstancePropertyModifications(textMesh);
			#endif
			{
				tmpFont = uiTheme.GetTmpFont(tmpFontTag);
				textMesh.font = tmpFont;
			}
			#if UNITY_EDITOR
			EditorUtility.SetDirty(textMesh);
			#endif
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