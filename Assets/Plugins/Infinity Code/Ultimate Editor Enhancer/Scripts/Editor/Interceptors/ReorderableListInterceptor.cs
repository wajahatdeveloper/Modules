/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using HarmonyLib;
using InfinityCode.UltimateEditorEnhancer.InspectorTools;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using InfinityCode.UltimateEditorEnhancer.Windows;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.Interceptors
{
    [InitializeOnLoad]
    public static class ReorderableListInterceptor
    {
        private static Harmony harmony;
        private static MethodInfo patch;
        private static int indentLevel;

        public static bool insideList
        {
            get { return indentLevel > 0; }
        }

        static ReorderableListInterceptor()
        {
            Patch();
        }

        private static void DoListElementsPrefix()
        {
            indentLevel++;
            if (Prefs.nestedEditorInReorderableList) return;
            NestedEditor.disallowNestedEditors = true;
        }

        private static void DoListElementsPostfix()
        {
            indentLevel--;
            if (Prefs.nestedEditorInReorderableList) return;
            NestedEditor.disallowNestedEditors = indentLevel > 0;
        }

        private static void Patch()
        {
            if (harmony != null) return;
            if (ReorderableListRef.doListElementsMethod == null) return;

            try
            {
                harmony = new Harmony("InfinityCode.UltimateEditorEnhancer.ReorderableListInterceptor");

                MethodInfo prefixMethod = AccessTools.Method(typeof(ReorderableListInterceptor), "DoListElementsPrefix");
                MethodInfo postfixMethod = AccessTools.Method(typeof(ReorderableListInterceptor), "DoListElementsPostfix");
                patch = harmony.Patch(ReorderableListRef.doListElementsMethod, new HarmonyMethod(prefixMethod), new HarmonyMethod(postfixMethod));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void Refresh()
        {
            if (!Prefs.nestedEditorInReorderableList) Patch();
            else
            {
                NestedEditor.disallowNestedEditors = false;
                harmony.Unpatch(ReorderableListRef.doListElementsMethod, patch);
                harmony = null;
            }
        }
    }
}