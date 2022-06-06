using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UIHelper : EditorWindow
{
    protected static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    
    protected Vector2 scrollPosition;

    [MenuItem("Hub/UI Helper")]
    public static void Init()
    {
        UpdateList();

        Resources.UnloadUnusedAssets();
        
        var window = GetWindow<UIHelper>();
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
    }

    private void OnFocus()
    {
        UpdateList();

        Resources.UnloadUnusedAssets();
    }

    protected virtual void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginVertical();
        DrawItemList();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }
    
    protected void DrawItemList()
    {
        foreach (var item in items)
        {
            if (item.Key == "-" || item.Key == "--")
            {
                EditorGUILayout.Separator();
            }
            else if (GUILayout.Button(item.Key))
            {
                if (Selection.activeTransform == null || Selection.activeTransform.GetComponent<Canvas>() == null)
                {
                    Selection.activeTransform = FindObjectOfType<Canvas>()?.transform;
                }
                if (Selection.activeTransform == null || Selection.activeTransform.GetComponent<Canvas>() == null)
                {
                    var newCanvas = Instantiate(items["Canvas"], Selection.activeTransform);
                    Selection.activeTransform = newCanvas.transform;
                }
                var itemPrefab = item.Value;
                var itemObject = Instantiate(itemPrefab, Selection.activeTransform);
                itemObject.name = itemObject.name.Replace("(Clone)", "");
            }
        }
    }
}
