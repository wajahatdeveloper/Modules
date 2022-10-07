using Sirenix.OdinInspector.Demos;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UIDesignerWindow : OdinEditorWindow
{
	public static UIDesignerWindow currentWindow = null;
	public static UIDesignerCanvasWindow canvasWindow = null;

	[MenuItem("Hub/UI Designer",priority = 102)]
	public static void ShowWindow()
	{
		currentWindow = GetWindow<UIDesignerWindow>();

		currentWindow.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
		currentWindow.minSize = new Vector2(1280, 720);

		canvasWindow = GetWindow<UIDesignerCanvasWindow>("Canvases",typeof(UIDesignerWindow));
		canvasWindow.ShowUtility();

		// OdinEditorWindow.
	}

	protected override IEnumerable<object> GetTargets()
	{
		// Draws this instance using Odin
		yield return this;
	}

	protected override void DrawEditor(int index)
	{
		var currentDrawingEditor = this.CurrentDrawingTargets[index];

		// SirenixEditorGUI.

		base.DrawEditor(index);
	}

	private void OnFocus()
	{
		canvasWindow?.UpdateCanvasList();
	}
}