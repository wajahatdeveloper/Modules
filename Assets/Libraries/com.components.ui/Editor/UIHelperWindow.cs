using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIWidgets : EditorWindow
{
    protected static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    
    protected Vector2 scrollPosition;

    private bool isInstantiatingPrefab = true;

    [MenuItem("Hub/UI Widgets", priority = 101)]
    public static void Init()
    {
        UpdateList();

        Resources.UnloadUnusedAssets();
        
        var window = GetWindow<UIWidgets>();
        window.minSize = new Vector2(250f, 200f);
        window.Show();
    }

    private static void UpdateList()
    {
        items.Clear();
        var lst = Resources.LoadAll<GameObject>("UIWidgets");
        foreach (GameObject o in lst)
        {
            items.Add(o.name, o);
        }

        items.Add("-", null);
        lst = Resources.LoadAll<GameObject>("UIWidgets_Scrolls");
        foreach (GameObject o in lst)
        {
            items.Add(o.name, o);
        }

        items.Add("--", null);
        lst = Resources.LoadAll<GameObject>("UIWidgets_Panels");
        foreach (GameObject o in lst)
        {
            items.Add(o.name, o);
        }

		items.Add("---", null);
		lst = Resources.LoadAll<GameObject>("UIWidgets_Elements");
		foreach (GameObject o in lst)
		{
			items.Add(o.name, o);
		}
	}

    private void OnFocus()
    {
        UpdateList();

        Resources.UnloadUnusedAssets();
    }

    protected virtual void OnGUI()
    {
        isInstantiatingPrefab = EditorGUILayout.Toggle("Use Prefabs", isInstantiatingPrefab);

        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        {
	        DrawSelectionTools();
        }
        EditorGUILayout.EndHorizontal();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        {
	        EditorGUILayout.BeginVertical();
	        {
		        DrawItemList();
	        }
	        EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawSelectionTools()
    {
	    if (GUILayout.Button("Select All Button Text"))
	    {
		    Button[] buttons = FindObjectsOfType<Button>(true);
		    List<GameObject> firstChildrenOfButtons = new();

		    foreach (Button button in buttons)
		    {
			    if (button.transform.childCount > 0)
			    {
				    Transform firstChild = button.transform.GetChild(0);
				    if (firstChild.GetComponent<Text>() == null)
				    {
					    continue;
				    }
				    firstChildrenOfButtons.Add(firstChild.gameObject);
			    }
		    }

		    Selection.objects = firstChildrenOfButtons.ToArray();
	    }

	    if (GUILayout.Button("Select All Button Text Mesh Pro"))
	    {
		    Button[] buttons = FindObjectsOfType<Button>(true);
		    List<GameObject> firstChildrenOfButtons = new();

		    foreach (Button button in buttons)
		    {
			    if (button.transform.childCount > 0)
			    {
				    Transform firstChild = button.transform.GetChild(0);
				    if (firstChild.GetComponent<TextMeshProUGUI>() == null)
				    {
					    continue;
				    }
				    firstChildrenOfButtons.Add(firstChild.gameObject);
			    }
		    }

		    Selection.objects = firstChildrenOfButtons.ToArray();
	    }
    }
    
    protected void DrawItemList()
    {
        foreach (var item in items)
        {
            string s = item.Key;
            int i = s.IndexOf('(');
            if (i >= 0)
            {
                s = s.Remove(i);
            }
            GUIStyle gUIStyle = new GUIStyle(GUI.skin.button);
            gUIStyle.alignment = TextAnchor.MiddleLeft;
            
            Texture svicon = Resources.Load($"IconImages/{s}")as Texture;
            if (item.Key.StartsWith("-"))
            {
                EditorGUILayout.Separator();
            }
            else if ((GUILayout.Button(new GUIContent(item.Key,svicon),gUIStyle)/*GUILayout.Button(item.Key)*/))
            {
                if (Selection.activeTransform == null)
                {
					// Find and Try Assign Existing Canvas
					Selection.activeTransform = FindObjectOfType<Canvas>()?.transform;

					// Create and Assign New Canvas
					if (Selection.activeTransform == null)
					{
						if (item.Key != "Canvas")
						{
							GameObject newCanvas;

							if (isInstantiatingPrefab)
							{
								newCanvas = PrefabUtility.InstantiatePrefab(items["Canvas"], Selection.activeTransform) as GameObject;
							}
							else
							{
								newCanvas = Instantiate(items["Canvas"], Selection.activeTransform);
							}

							Selection.activeTransform = newCanvas.transform;
						}
					}
				}
                else
                {
                    // If No UI Object is Selected
                    if (Selection.activeTransform.GetComponent<RectTransform>() == null)
                    {
                        Selection.activeTransform = null;
                    }
				}

				var itemPrefab = item.Value;
                GameObject itemObject;

                if (isInstantiatingPrefab)
				{
					itemObject = PrefabUtility.InstantiatePrefab(itemPrefab, Selection.activeTransform) as GameObject;
				}
				else
				{
					itemObject = Instantiate(itemPrefab, Selection.activeTransform);
				}

                itemObject.name = itemObject.name.Replace("(Clone)", "");
            }
        }
    }
}