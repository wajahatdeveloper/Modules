#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;

public class GenerateEnum
{
    public static void Go(string enumName, string[] enumEntries)
    {
        ScriptableObject scriptableObject = Resources.Load<ScriptableObject>("MultiTaggerData");
        MonoScript targetObject = MonoScript.FromScriptableObject(scriptableObject);
        string targetPath = AssetDatabase.GetAssetPath(targetObject);
        targetPath = Path.GetDirectoryName(targetPath);
        
        string filePathAndName = targetPath + "/Resources/" + enumName + ".cs";
 
        using ( StreamWriter streamWriter = new StreamWriter( filePathAndName ) )
        {
            streamWriter.WriteLine( "public enum " + enumName );
            streamWriter.WriteLine( "{" );
            for( int i = 0; i < enumEntries.Length; i++ )
            {
                streamWriter.WriteLine( "\t" + enumEntries[i] + "," );
            }
            streamWriter.WriteLine( "}" );
        }
        AssetDatabase.Refresh();
    }
}

#endif
