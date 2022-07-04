#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ScriptingSymbols
{
    /// <summary>
        /// Adds a given scripting define symbol to all build target groups
        /// You can see all scripting define symbols ( not the internal ones, only the one for this project), in the PlayerSettings inspector
        /// </summary>
        /// <param name="defineSymbol">Define symbol.</param>
        public static void AddScriptingDefineSymbolToAllBuildTargetGroups(string defineSymbol)
        {
            foreach (BuildTarget target in Enum.GetValues(typeof(BuildTarget)))
            {
                BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);

                if (group == BuildTargetGroup.Unknown)
                {
                    continue;
                }

                var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';').Select(d => d.Trim()).ToList();

                if (!defineSymbols.Contains(defineSymbol))
                {
                    defineSymbols.Add(defineSymbol);

                    try
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", defineSymbols.ToArray()));
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Could not set Photon " + defineSymbol + " defines for build target: " + target + " group: " + group + " " + e);
                    }
                }
            }
        }
}

#endif
