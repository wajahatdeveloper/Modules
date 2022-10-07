using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIDesignerCanvasWindow : OdinEditorWindow
{
	[LabelText("Canvases in scene")]
	[CustomValueDrawer(nameof(CustomOnGUI))]
	[ListDrawerSettings(
		OnBeginListElementGUI = nameof(BeginDrawListElement),
		OnEndListElementGUI = nameof(EndDrawListElement),
		AddCopiesLastElement = false, AlwaysAddDefaultValue = false,
		DraggableItems = false, Expanded = true, HideAddButton = true,
		HideRemoveButton = true, IsReadOnly = true
		)]
	public List<Canvas> canvases = new List<Canvas>();

	private static Canvas CustomOnGUI(Canvas canvas)
	{
		return canvas;
	}

	private static void BeginDrawListElement()
	{
		SirenixEditorGUI.BeginToolbarBox();
	}

	private static void EndDrawListElement()
	{
		SirenixEditorGUI.EndToolbarBox();
	}

	private void OnFocus()
	{
		UpdateCanvasList();
	}

	public void UpdateCanvasList()
	{
		canvases.Clear();
		canvases.AddRange(GameObject.FindObjectsOfType<Canvas>().ToList());
	}
}