using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIThemedItem : MonoBehaviour
{
	[ShowIf(nameof(HasImage))]
	[ValueDropdown(nameof(GetSprites)), OnValueChanged(nameof(SetImageSprite))]
	public Sprite imageSprite;

	[ShowIf(nameof(HasText))]
	[ValueDropdown(nameof(GetFonts)), OnValueChanged(nameof(SetFont))]
	public Font font;

	[ShowIf(nameof(HasTextMesh))]
	[ValueDropdown(nameof(GetTMPFonts)), OnValueChanged(nameof(SetTMPFont))]
	public TMP_FontAsset TMPFont;

	private UITheme uiTheme;
	private ThemeItem themeItem;
	private Image image;
	private Text text;
	private TextMeshProUGUI textMesh;

	[OnInspectorInit]
	[ContextMenu(nameof(_internal_inspector_init))]
	private void _internal_inspector_init()
	{
		Apply();
	}

	private bool HasImage() => image != null;
	private bool HasText() => text != null;
	private bool HasTextMesh() => textMesh != null;

	private void SetImageSprite()
	{
		if (imageSprite == null) { return; }
		image.sprite = imageSprite;
	}

	private void SetFont()
	{
		if (font == null) { return; }
		text.font = font;
	}

	private void SetTMPFont()
	{
		if (TMPFont == null) { return; }
		textMesh.font = TMPFont;
	}

	private List<Sprite> GetSprites() => themeItem.spriteAssets;
	private List<Font> GetFonts() => themeItem.fonts;
	private List<TMP_FontAsset> GetTMPFonts() => themeItem.tmpFontAssets;

	private T FindComponentInParentHierarchy<T>() where T : Component
	{
		if (gameObject == null)
		{
			return null;
		}

		// Check the current GameObject first
		T component = gameObject.GetComponent<T>();
		if (component != null)
		{
			return component;
		}

		// Traverse up the hierarchy and check each parent
		Transform parent = gameObject.transform.parent;
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

	private void SetDefaultValues()
	{
		if (imageSprite == null)
		{
			imageSprite = themeItem.spriteAssets.FirstOrDefault();
			SetImageSprite();
		}

		if (font == null)
		{
			font = themeItem.fonts.FirstOrDefault();
			SetFont();
		}

		if (TMPFont == null)
		{
			TMPFont = themeItem.tmpFontAssets.FirstOrDefault();
			SetTMPFont();
		}
	}

	public void Apply()
	{
		if (uiTheme == null)
		{
			uiTheme = FindComponentInParentHierarchy<UITheme>();
		}

		foreach (ThemeItem item in uiTheme.themeData.themeItems)
		{
			if (GetComponent(item.itemType) != null)
			{
				themeItem = item;

				if (item.itemType == typeof(Button) || item.itemType == typeof(ButtonX))
				{
					image = GetComponent<Image>();
					text = GetComponentInChildren<Text>();
					textMesh = GetComponentInChildren<TextMeshProUGUI>();

					SetDefaultValues();

					break;
				}

				// Generic
				image = GetComponent<Image>();
				text = GetComponent<Text>();
				textMesh = GetComponent<TextMeshProUGUI>();

				SetDefaultValues();

				break;
			}
		}
	}
}