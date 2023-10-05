using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PanelManagerWindow : EditorWindow
{
    protected static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    private bool enableSelectedPanel = true;
    private bool reselectToDisable = true;
    private Vector2 _viewScrollPosition;

    [MenuItem("Hub/PanelManager")]
    private static void ShowWindow()
    {
        var window = GetWindow<PanelManagerWindow>();
        window.titleContent = new GUIContent("Panel Manager");
        window.minSize = new Vector2(250f, 200f);
        window.Show();
    }

    private void OnFocus()
    {
        FindObjectsUnderCanvasWithKeywords();
        Resources.UnloadUnusedAssets();
    }

    private void FindObjectsUnderCanvasWithKeywords()
    {
        items.Clear();
        
        GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        var canvases = rootGameObjects.Where(x => x.GetComponent<Canvas>()).ToList();
        foreach (GameObject canvasObject in canvases)
        {
            foreach (Transform child in canvasObject.transform)
            {
                if (child.name.ToLower().Contains("panel") || child.name.ToLower().Contains("screen"))
                {
                    //Debug.Log("Found object: " + child.name, child.gameObject);
                    items.Add(child.name, child.gameObject);
                }
            }
        }
    }
    private void OnGUI()
    {
        DrawPanelList();
    }

    private void DrawPanelList()
    {
        _viewScrollPosition = EditorGUILayout.BeginScrollView(_viewScrollPosition, false, false);
        
        foreach (var item in items)
        {
            var style = new GUIStyle(GUI.skin.button)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(8, 8, 4, 4),
                margin = new RectOffset(4, 4, 4, 4),
                normal = { textColor = Color.white },
                hover = { textColor = Color.yellow },
                
            };
            
            if (GUILayout.Button(new GUIContent(item.Key) , style: style))
            {
                Selection.activeTransform = item.Value.transform;

                if (Selection.activeTransform.gameObject.activeSelf)
                {
                    if (reselectToDisable)
                    {
                        Selection.activeTransform.gameObject.SetActive(false);
                    }
                }
                else if (enableSelectedPanel)
                {
                    Selection.activeTransform.gameObject.SetActive(true);
                }
            }
        }
        
        GUILayout.EndScrollView();
    }
}