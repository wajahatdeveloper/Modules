using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/UIHelperAsset")]
public class UIHelperAssetScriptable : ScriptableObject
{
	public List<UIWidget> widgets;
}

[Serializable]
public class UIWidget
{
	public UIWidget(UIWidgetLeaf leaf)
	{
		this.widgetName = leaf.widgetName;
		this.widgetIcon = leaf.widgetIcon;
		this.widgetPrefab = leaf.widgetPrefab;
		this.widgetPrefabLegacy = leaf.widgetPrefabLegacy;
		this.noCanvasRequired = leaf.noCanvasRequired;
	}

	public string widgetName;
	public Texture widgetIcon;
	public GameObject widgetPrefab;
	public GameObject widgetPrefabLegacy;
	public List<UIWidgetLeaf> widgetVariations = new List<UIWidgetLeaf>();
	public bool noCanvasRequired;
}

[Serializable]
public class UIWidgetLeaf
{
	public string widgetName;
	public Texture widgetIcon;
	public GameObject widgetPrefab;
	public GameObject widgetPrefabLegacy;
	public bool noCanvasRequired;
}