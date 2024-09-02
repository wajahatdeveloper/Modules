using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public partial class UIWidgets
{
	public UIHelperAssetScriptable uiHelperAsset;

	private Dictionary<string, bool> foldoutLUT = new Dictionary<string, bool>();

	private GUIStyle _foldoutStyle;

	private GUIStyle foldoutStyle
	{
		get
		{
			if (_foldoutStyle == null)
			{
				_foldoutStyle = new GUIStyle(EditorStyles.foldoutHeader)
				{
					fixedWidth = 20
				};
			}

			return _foldoutStyle;
		}
	}

	private static T[] FindObjectsOfTypeInChildrenRecursive<T>(Transform root, bool includeInactive = true)
		where T : Component
	{
		List<T> results = new List<T>();

		// Iterate through all child transforms
		foreach (Transform child in root)
		{
			var comp = child.GetComponent(typeof(T));

			// Check if the child is of the desired type and is active (if includeInactive is false)
			if (comp != null && (includeInactive || child.gameObject.activeInHierarchy))
			{
				results.Add((T)comp);
			}

			// Recursively search for objects in child's children and **add the results to the current list**
			results.AddRange(FindObjectsOfTypeInChildrenRecursive<T>(child, includeInactive));
		}

		return results.ToArray();
	}

	private static List<T2> GetAllT2FromT1<T1, T2>(bool includeInactive = true, Transform root = null)
			where T1 : Component
			where T2 : Component
	{
		T1[] t1Arr = FindObjectsOfType<T1>(includeInactive);

        List<T2> list = new();

        if (root != null)
		{
            list = FindObjectsOfTypeInChildrenRecursive<T2>(root, includeInactive).ToList();
        }
		else
		{
            // find global
            foreach (T1 t1 in t1Arr)
            {
                if (t1.transform.childCount > 0)
                {
                    Transform firstChild = t1.transform.GetChild(0);
                    if (firstChild.GetComponent<T2>() == null)
                    {
                        continue;
                    }
                    list.Add(firstChild.GetComponent<T2>());
                }
            }
        }

		return list;
	}

    private void DrawSelectionTools()
    {
	    // Set Button Style
	    GUIStyle buttonStyleActive = new GUIStyle(GUI.skin.button)
	    {
		    alignment = TextAnchor.MiddleLeft,
		    fontSize = 10
	    };

	    GUIStyle buttonStyleInactive = new GUIStyle(GUI.skin.button)
	    {
		    normal = new GUIStyleState()
		    {
			    textColor = Color.gray
		    },
		    alignment = TextAnchor.MiddleLeft,
		    fontSize = 10
	    };

	    GUIStyle buttonStyle = (Selection.activeGameObject == null)?buttonStyleInactive:buttonStyleActive;

	    GUILayout.BeginVertical(GUILayout.Width(325));
	    {
		    GUILayout.BeginHorizontal(GUILayout.Width(325));
		    {
			    if (GUILayout.Button("Select All Button Text", buttonStyleActive))
			    {
				    var arr = GetAllT2FromT1<Button, Text>(true)
						    .Select(x => x.gameObject)
						    .ToArray();
				    Selection.objects = arr;
			    }

			    if (GUILayout.Button("In Children", buttonStyle, GUILayout.Width(75)) && buttonStyle == buttonStyleActive)
			    {
				    var arr = GetAllT2FromT1<Button, Text>(true, Selection.activeTransform)
						    .Select(x => x.gameObject)
						    .ToArray();
				    Selection.objects = arr;
			    }
		    }
		    GUILayout.EndHorizontal();

		    GUILayout.BeginHorizontal(GUILayout.Width(325));
		    {
			    if (GUILayout.Button("Select All Button TextMeshProUGUI", buttonStyleActive))
			    {
				    var arr = GetAllT2FromT1<Button, TextMeshProUGUI>(true)
						    .Select(x => x.gameObject)
						    .ToArray();
				    Selection.objects = arr;
			    }

			    if (GUILayout.Button("In Children", buttonStyle, GUILayout.Width(75)) && buttonStyle == buttonStyleActive)
			    {
				    var arr = GetAllT2FromT1<Button, TextMeshProUGUI>(true, Selection.activeTransform)
						    .Select(x => x.gameObject)
						    .ToArray();
				    Selection.objects = arr;
			    }
		    }
		    GUILayout.EndHorizontal();
	    }
	    GUILayout.EndVertical();
    }

    private void DrawItemList()
    {
	    if (uiHelperAsset == null)
	    {
		    return;
	    }

	    int index = 0;
	    int subItemPreSpace = 32;

	    foreach (UIWidget widget in uiHelperAsset.widgets)
	    {
		    index++;

		    EditorGUILayout.BeginHorizontal();
		    {
			    if (widget.widgetPrefabLegacy != null || widget.widgetVariations.Count > 0)
			    {
				    foldoutLUT[widget.widgetName] = EditorGUILayout.BeginFoldoutHeaderGroup(
					    foldoutLUT.GetValueOrDefault(widget.widgetName, false),
					    GUIContent.none, foldoutStyle);
				    {
					    DrawWidgetRow(widget);
				    }
				    EditorGUILayout.EndFoldoutHeaderGroup();
			    }
			    else
			    {
				    // Draw Button Element
				    EditorGUILayout.BeginHorizontal();
				    {
					    EditorGUILayout.Space(22, false);
					    DrawWidgetRow(widget);
				    }
				    EditorGUILayout.EndHorizontal();
			    }
		    }
		    EditorGUILayout.EndHorizontal();

		    EditorGUILayout.Space(5, false);

		    // Draw Variations
		    if (foldoutLUT.GetValueOrDefault(widget.widgetName, false))
		    {
			    EditorGUILayout.BeginVertical();
			    {
				    if (widget.widgetPrefabLegacy != null)
				    {
					    DrawWidgetRow(widget, subItemPreSpace,true);
				    }

				    foreach (UIWidgetLeaf widgetVariation in widget.widgetVariations)
				    {
					    DrawWidgetRow(widgetVariation, subItemPreSpace);
				    }
			    }

			    EditorGUILayout.Space(10);

			    EditorGUILayout.EndVertical();
		    }
	    }
    }

    private void DrawWidgetRow(UIWidget widget, int width = -1,bool useLegacyPrefab = false)
    {
	    EditorGUILayout.BeginHorizontal();
	    {
		    if (width > 0)
		    {
			    GUILayout.Space(width);
		    }

		    var pair = new KeyValuePair<string, GameObject>(
			    (useLegacyPrefab)?widget.widgetName + " (Legacy)":widget.widgetName,
			    (useLegacyPrefab)?widget.widgetPrefabLegacy:widget.widgetPrefab);
		    DrawSiblingItemButton(pair, widget.widgetIcon, widget.noCanvasRequired);
		    DrawChildItemButton(pair);
		    DrawParentItemButton(pair);
	    }
	    EditorGUILayout.EndHorizontal();
    }

    private void DrawWidgetRow(UIWidgetLeaf widgetLeaf, int width = -1) => DrawWidgetRow(new UIWidget(widgetLeaf), width);

    private void DrawSiblingItemButton(KeyValuePair<string, GameObject> spawnableItem, Texture icon, bool noCanvasRequired)
    {
	    // Set Button Style
	    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
	    {
		    alignment = TextAnchor.MiddleLeft,
		    fontSize = 12
	    };

	    var buttonContent = new GUIContent(spawnableItem.Key, icon);
	    if (GUILayout.Button(buttonContent, buttonStyle))
	    {
		    var itemPrefab = spawnableItem.Value;
		    GameObject itemObject;

		    if (Selection.activeTransform == null && !noCanvasRequired)
		    {
			    Debug.LogError("No Item Selected to add as sibling!");
			    return;
		    }

		    if (isInstantiatingPrefab)
		    {
			    if (noCanvasRequired)
			    {
				    itemObject = PrefabUtility.InstantiatePrefab(itemPrefab) as GameObject;
			    }
			    else
			    {
				    itemObject =
						    PrefabUtility.InstantiatePrefab(itemPrefab, Selection.activeTransform.parent) as GameObject;
			    }
		    }
		    else
		    {
			    if (noCanvasRequired)
			    {
				    itemObject = Instantiate(itemPrefab);
			    }
			    else
			    {
				    itemObject = Instantiate(itemPrefab, Selection.activeTransform.parent);
			    }
		    }

		    Undo.RegisterCreatedObjectUndo(itemObject, $"Create {itemPrefab.name}");

		    itemObject.name = itemObject.name.Replace("(Clone)", "");

		    if (autoSelectNewItems)
		    {
			    Selection.SetActiveObjectWithContext(itemObject, itemObject);
		    }
	    }
    }

    private void DrawChildItemButton(KeyValuePair<string, GameObject> spawnableItem)
    {
	    // Set Button Style
	    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
	    {
		    alignment = TextAnchor.MiddleCenter,
		    fontSize = 12
	    };

	    var buttonContent = new GUIContent("Child");
	    if (GUILayout.Button(buttonContent, buttonStyle, GUILayout.Width(65)))
	    {
		    // If nothing is selected then select the canvas
		    if (Selection.activeTransform == null ||
		        Selection.activeTransform.GetComponent<RectTransform>() == null)
		    {
			    if (preferExistingCanvas)
			    {
				    // Find and Try Assign Existing Canvas
				    Selection.activeTransform = FindObjectOfType<Canvas>()?.transform;
			    }

			    // Create and Assign New Canvas
			    var newCanvas = CreateCanvasExplicit();
			    Selection.activeGameObject = newCanvas;
		    }

		    var itemPrefab = spawnableItem.Value;
		    GameObject itemObject;

		    if (isInstantiatingPrefab)
		    {
			    itemObject =
					    PrefabUtility.InstantiatePrefab(itemPrefab, Selection.activeTransform) as GameObject;
		    }
		    else
		    {
			    itemObject = Instantiate(itemPrefab, Selection.activeTransform);
		    }

		    Undo.RegisterCreatedObjectUndo(itemObject, $"Create {itemPrefab.name}");

		    itemObject.name = itemObject.name.Replace("(Clone)", "");

		    if (autoSelectNewItems)
		    {
			    Selection.SetActiveObjectWithContext(itemObject, itemObject);
		    }
	    }
    }

    private void DrawParentItemButton(KeyValuePair<string, GameObject> spawnableItem)
    {
	    // Set Button Style
	    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
	    {
		    alignment = TextAnchor.MiddleCenter,
		    fontSize = 12
	    };

	    var buttonContent = new GUIContent("Parent");
	    if (GUILayout.Button(buttonContent, buttonStyle, GUILayout.Width(65)))
	    {
		    // If nothing is selected then select the canvas
		    if (Selection.activeTransform == null ||
		        Selection.activeTransform.GetComponent<RectTransform>() == null)
		    {
			    if (preferExistingCanvas)
			    {
				    // Find and Try Assign Existing Canvas
				    Selection.activeTransform = FindObjectOfType<Canvas>()?.transform;
			    }

			    // Create and Assign New Canvas
			    var newCanvas = CreateCanvasExplicit();
			    Selection.activeGameObject = newCanvas;
		    }

		    var itemPrefab = spawnableItem.Value;
		    GameObject itemObject;

		    Transform parent = Selection.activeTransform?.parent?.parent;
		    if (parent == null)
		    {
			    Debug.LogError("UIWidgets: No Parent Found!");
			    return;
		    }

		    if (isInstantiatingPrefab)
		    {
			    itemObject =
					    PrefabUtility.InstantiatePrefab(itemPrefab, parent) as GameObject;
		    }
		    else
		    {
			    itemObject = Instantiate(itemPrefab, parent);
		    }

		    Undo.RegisterCreatedObjectUndo(itemObject, $"Create {itemPrefab.name}");

		    itemObject.name = itemObject.name.Replace("(Clone)", "");

		    if (autoSelectNewItems)
		    {
			    Selection.SetActiveObjectWithContext(itemObject, itemObject);
		    }
	    }
    }

    private string RemoveParenthesisSections(string key)
    {
	    int i = key.IndexOf(' ');
	    if (i >= 0)
	    {
		    key = key.Remove(i);
	    }
	    return key;
    }

    private GameObject CreateCanvasExplicit(Transform parent = null)
    {
	    if (parent == null)
	    {
		    parent = Selection.activeTransform;
	    }

	    GameObject canvasPrefab = uiHelperAsset.widgets.First(x => x.widgetName == "Canvas").widgetPrefab;

	    GameObject newCanvas;
	    if (isInstantiatingPrefab)
	    {
		    newCanvas = PrefabUtility.InstantiatePrefab(canvasPrefab, parent) as GameObject;
	    }
	    else
	    {
		    newCanvas = Instantiate(canvasPrefab, parent);
	    }
	    Undo.RegisterCreatedObjectUndo(newCanvas, $"Create new Canvas");
	    return newCanvas;
    }
}