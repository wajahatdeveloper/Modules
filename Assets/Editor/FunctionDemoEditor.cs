using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Reflection;
using System;
 
[CustomEditor(typeof(FunctionDemo))]
public class FunctionDemoEditor : Editor
{
    static string[] methods;
    static string[] ignoreMethods = new string[] { "Start", "Update" };
 
    static FunctionDemoEditor()
    {

        methods =
            typeof(FunctionDemo)
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) // Instance methods, both public and private/protected
            .Where(x => x.DeclaringType == typeof(FunctionDemo)) // Only list methods defined in our own class
            .Where(x => x.GetParameters().Length == 0) // Make sure we only get methods with zero argumenrts
            .Where(x => !ignoreMethods.Any(n => n == x.Name)) // Don't list methods in the ignoreMethods array (so we can exclude Unity specific methods, etc.)
            .Select(x => x.Name)
            .ToArray();
    }
 
    public override void OnInspectorGUI()
    {
        FunctionDemo obj = target as FunctionDemo;
 
        if (obj != null)
        {
            int index;
 
            try
            {
                index = methods
                    .Select((v, i) => new { Name = v, Index = i })
                    .First(x => x.Name == obj.MethodToCall)
                    .Index;
            }
            catch
            {
                index = 0;
            }
 
            obj.MethodToCall = methods[EditorGUILayout.Popup(index, methods)];
        }
    }
}
