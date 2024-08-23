using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class UIWidgets : EditorWindow
{
	private static Dictionary<string, GameObject> _SpawnableItems = new Dictionary<string, GameObject>();
	private static Vector2 _WindowMinSize = new Vector2(250f, 200f);

	private bool isInstantiatingPrefab = true;
	private bool autoSelectNewItems = true;
	private bool preferExistingCanvas = false;
	private Vector2 scrollPosition;

    [MenuItem("Hub/UI Widgets", priority = 101)]
    public static void Init()
    {
        RebuildSpawnList();

        var window = GetWindow<UIWidgets>();
        window.minSize = _WindowMinSize;
        window.Show();
    }

    protected virtual void OnGUI()
    {
	    var toggleStyle = new GUIStyle(GUI.skin.toggle)
	    {
		    fontSize = 10
	    };

	    GUILayout.BeginHorizontal();
	    {
		    isInstantiatingPrefab = GUILayout.Toggle(isInstantiatingPrefab, "Use Prefabs", toggleStyle);
		    autoSelectNewItems = GUILayout.Toggle(autoSelectNewItems, "Auto Select New Items", toggleStyle);
		    preferExistingCanvas = GUILayout.Toggle(preferExistingCanvas, "Prefer Existing Canvas", toggleStyle);
		    GUILayout.FlexibleSpace();
	    }
	    GUILayout.EndHorizontal();

	    DrawSelectionTools();

	    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
	    {
		    GUILayout.BeginVertical();
		    {
			    DrawItemList();
		    }
		    GUILayout.EndVertical();
	    }
	    GUILayout.EndScrollView();
    }

    private void OnFocus()
    {
	    RebuildSpawnList();
	    Resources.UnloadUnusedAssets();
    }
}