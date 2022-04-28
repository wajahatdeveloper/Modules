/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HarmonyLib;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    [InitializeOnLoad]
    public static class HierarchyToolbarInterceptor
    {
        private static Harmony harmony;
        private static GUIContent filterByType;
        private static MethodInfo postfix;

        static HierarchyToolbarInterceptor()
        {
            try
            {
                postfix = typeof(HierarchyToolbarInterceptor).GetMethod("SearchFieldGUI", Reflection.StaticLookup);

                if (Prefs.hierarchyTypeFilter) Patch();
            }
            catch
            {
                
            }
        }

        private static void Patch()
        {
            if (harmony != null) return;

            harmony = new Harmony("InfinityCode.UltimateEditorEnhancer.HierarchyToolbarInterceptor");
            harmony.Patch(SearchableEditorWindowRef.searchFieldGUIMethod, postfix: new HarmonyMethod(postfix));
        }

        private static void SearchFieldGUI(EditorWindow __instance)
        {
            if (__instance.GetType() != SceneHierarchyWindowRef.type) return;

            int mode = SceneHierarchyWindowRef.GetSearchMode(__instance);
            if (filterByType == null)
            {
                filterByType = EditorGUIUtility.IconContent("FilterByType", "Search by Type");
                filterByType.tooltip = "Filter by Type";
            }

            if (mode != 1 && GUILayoutUtils.Button(filterByType, EditorStyles.toolbarButton) == ButtonEvent.click)
            {
                Component[] components = Object.FindObjectsOfType<Component>();
                HashSet<string> types = new HashSet<string>();
                for (int i = 0; i < components.Length; i++)
                {
                    Type type = components[i].GetType();
                    string name = type.Name;
                    if (!types.Contains(name)) types.Add(name);
                }

                Rect lastRect = GUILayoutUtils.lastRect;
                GUIContent[] contents = types.OrderBy(t => t).Select(t => new GUIContent(t)).ToArray();
                FlatSelectorWindow.Show(new Rect(new Vector2(lastRect.x, lastRect.yMax), Vector2.zero), contents, -1).OnSelect += i =>
                {
                    if (i < 0 || i >= contents.Length) return;

                    if (mode == 0)
                    {
                        string search = SceneHierarchyWindowRef.GetSearchFilter(__instance);
                        search = Regex.Replace(search, @"t:\w+", "").Trim();
                        if (search.Length > 0) search += " ";
                        search += "t:" + contents[i].text;
                        SceneHierarchyWindowRef.SetSearchFilter(__instance, search, mode);
                    }
                    else SceneHierarchyWindowRef.SetSearchFilter(__instance, contents[i].text, mode);
                };
            }
        }

        public static void Refresh()
        {
            if (Prefs.hierarchyTypeFilter) Patch();
            else
            {
                harmony.Unpatch(SearchableEditorWindowRef.searchFieldGUIMethod, postfix);
                harmony = null;
            }
        }
    }
}