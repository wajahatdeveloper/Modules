// Tags, Layers and Scene Builder - Auto Generate Tags, Layers and Scenes classes containing consts for all variables for code completion - 2012-10-01
// released under MIT License
// http://www.opensource.org/licenses/mit-license.php
//
//@author		Devin Reimer - AlmostLogical Software / Owlchemy Labs
//@website 		http://blog.almostlogical.com, http://owlchemylabs.com
/*
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;

//Note: This class uses UnityEditorInternal which is an undocumented internal feature
public class TagsLayersScenesBuilder : EditorWindow
{
    private const string FOLDER_LOCATION = "Scripts/AutoGenerated/";
    private const string TAGS_FILE_NAME = "Tags";
    private const string LAYERS_FILE_NAME = "Layers";
    private const string SORTING_LAYERS_FILE_NAME = "SortingLayers";
    private const string SCENES_FILE_NAME = "Scenes";
    private const string SCRIPT_EXTENSION = ".cs";

    [MenuItem("Hub/Editor/Generate/Rebuild Tags, Layers and Scenes Classes")]
    static void RebuildTagsAndLayersClasses()
    {
        string folderPath = Application.dataPath + "/" + FOLDER_LOCATION;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        File.WriteAllText(folderPath + TAGS_FILE_NAME + SCRIPT_EXTENSION, GetClassContent(TAGS_FILE_NAME, UnityEditorInternal.InternalEditorUtility.tags));
        File.WriteAllText(folderPath + LAYERS_FILE_NAME + SCRIPT_EXTENSION, GetLayerClassContent(LAYERS_FILE_NAME, UnityEditorInternal.InternalEditorUtility.layers));
        File.WriteAllText(folderPath + SORTING_LAYERS_FILE_NAME + SCRIPT_EXTENSION, GetLayerClassContent(SORTING_LAYERS_FILE_NAME, SortingLayer.layers.Select(x=>x.name).ToArray()));
        File.WriteAllText(folderPath + SCENES_FILE_NAME + SCRIPT_EXTENSION, GetClassContent(SCENES_FILE_NAME, EditorBuildSettingsScenesToNameStrings(EditorBuildSettings.scenes)));
        AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + TAGS_FILE_NAME + SCRIPT_EXTENSION, ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + LAYERS_FILE_NAME + SCRIPT_EXTENSION, ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + SORTING_LAYERS_FILE_NAME + SCRIPT_EXTENSION, ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + SCENES_FILE_NAME + SCRIPT_EXTENSION, ImportAssetOptions.ForceUpdate);
        Debug.Log("Rebuild Complete");
    }

    private static string[] EditorBuildSettingsScenesToNameStrings(EditorBuildSettingsScene[] scenes)
    {
        string[] sceneNames = new string[scenes.Length];
        for (int n = 0; n < sceneNames.Length; n++)
        {
            sceneNames[n] = System.IO.Path.GetFileNameWithoutExtension(scenes[n].path);
        }
        return sceneNames;
    }

    private static string GetClassContent(string className, string[] labelsArray)
    {
        string output = "";
        output += "//This class is auto-generated do not modify (TagsLayersScenesBuilder.cs) - blog.almostlogical.com\n";
        output += "public class " + className + "\n";
        output += "{\n";
        foreach (string label in labelsArray)
        {
            output += "\t"+ BuildConstVariable(label) + "\n";
        }
        output += "}";
        return output;
    }

    private static string GetLayerClassContent(string className, string[] labelsArray)
    {
        string output = "";
        output += "//This class is auto-generated do not modify (TagsLayersScenesBuilder.cs) - blog.almostlogical.com\n";
        output += "public class " + className + "\n";
        output += "{\n";
        foreach (string label in labelsArray)
        {
            output += "\t" + BuildConstVariable(label) + "\n";
        }
        output += "\n";

        foreach (string label in labelsArray)
        {
            output += "\t" + "public const int " + ToUpperCaseWithUnderscores(label) + "_INT" + " = " + LayerMask.NameToLayer(label) + ";\n";
        }

        output += "}";
        return output;
    }

    private static string BuildConstVariable(string varName)
    {
        return "public const string " + ToUpperCaseWithUnderscores(varName) + " = " + '"' + varName + '"' + ";";
    }

    private static string ToUpperCaseWithUnderscores(string input)
    {
        string output = "" + input[0];

        for (int n = 1; n < input.Length; n++)
        {
            if ((char.IsUpper(input[n]) || input[n] == ' ') && !char.IsUpper(input[n - 1]) && input[n - 1] != '_' && input[n - 1] != ' ')
            {
                output += "_";
            }

            if (input[n] != ' ' && input[n]!='_')
            {
                output += input[n];
            }
        }

        output = output.ToUpper();
        return output;
    }
}