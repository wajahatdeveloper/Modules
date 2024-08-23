using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public partial class UIWidgets
{
	private static void RebuildSpawnList()
    {
        _SpawnableItems.Clear();
        var gameObjects = Resources.LoadAll<GameObject>("UIWidgets");
        foreach (GameObject o in gameObjects)
        {
            _SpawnableItems.Add(o.name, o);
        }

        _SpawnableItems.Add("-", null);
        gameObjects = Resources.LoadAll<GameObject>("UIWidgets_Scrolls");
        foreach (GameObject o in gameObjects)
        {
            _SpawnableItems.Add(o.name, o);
        }

        _SpawnableItems.Add("--", null);
        gameObjects = Resources.LoadAll<GameObject>("UIWidgets_Panels");
        foreach (GameObject o in gameObjects)
        {
            _SpawnableItems.Add(o.name, o);
        }

		_SpawnableItems.Add("---", null);
		gameObjects = Resources.LoadAll<GameObject>("UIWidgets_Elements");
		foreach (GameObject o in gameObjects)
		{
			_SpawnableItems.Add(o.name, o);
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
        foreach (var spawnableItem in _SpawnableItems)
        {
	        // Separator Item
	        if (spawnableItem.Key.StartsWith("-"))
	        {
		        EditorGUILayout.Separator();
		        continue;
	        }

	        // Load Icon for Spawnable Item
	        string sanitizedKey = RemoveParenthesisSections(spawnableItem.Key);
	        Texture svicon = Resources.Load($"IconImages/{sanitizedKey}")as Texture;

	        // Set Button Style
	        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
	        {
		        alignment = TextAnchor.MiddleLeft,
		        fontSize = 12
	        };

	        // Draw Button Element
	        var buttonContent = new GUIContent(spawnableItem.Key, svicon);
	        if (GUILayout.Button(buttonContent, buttonStyle))
            {
	            // If nothing is selected then select the canvas
                if (Selection.activeTransform == null || Selection.activeTransform.GetComponent<RectTransform>() == null)
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
					itemObject = PrefabUtility.InstantiatePrefab(itemPrefab, Selection.activeTransform) as GameObject;
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
    }

    private string RemoveParenthesisSections(string key)
    {
	    int i = key.IndexOf('(');
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

	    GameObject newCanvas;
	    if (isInstantiatingPrefab)
	    {
		    newCanvas = PrefabUtility.InstantiatePrefab(_SpawnableItems["Canvas"], parent) as GameObject;
	    }
	    else
	    {
		    newCanvas = Instantiate(_SpawnableItems["Canvas"], parent);
	    }
	    Undo.RegisterCreatedObjectUndo(newCanvas, $"Create new Canvas");
	    return newCanvas;
    }
}