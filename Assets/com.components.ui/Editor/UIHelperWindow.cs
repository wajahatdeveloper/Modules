using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UIHelper : EditorWindow
{
    protected Vector2 scrollPosition;

    protected static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    [MenuItem("Hub/UI Helper")]
    public static void Init()
    {
        items.Clear();
        var lst = Resources.LoadAll<GameObject>("UIWidgets");
        foreach (GameObject o in lst) { items.Add(o.name,o); }
        items.Add("-",null);
        lst = Resources.LoadAll<GameObject>("UIWidgets_Scrolls");
        foreach (GameObject o in lst) { items.Add(o.name,o); }
        items.Add("--",null);
        lst = Resources.LoadAll<GameObject>("UIWidgets_Panels");
        foreach (GameObject o in lst) { items.Add(o.name,o); }

        Resources.UnloadUnusedAssets();
        
        var window = GetWindow<UIHelper>();
        window.minSize = new Vector2(250f, 200f);
        window.Show();
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
                var itemPrefab = item.Value;
                var itemObject = Instantiate(itemPrefab, Selection.activeTransform);
                itemObject.name = itemObject.name.Replace("(Clone)", "");
            }
        }
    }
}